﻿using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Entitas.Roslyn.CodeGeneration.Plugins.Utils
{
    public class FileParser
    {
        private readonly string _directoryPath;
        private readonly string[] _excludedDirs;
        private INamedTypeSymbol[]? _types; // Nullable to support the nullability feature

        public FileParser(string directoryPath, string excludedDirs)
        {
            _directoryPath = directoryPath;
            _excludedDirs = excludedDirs.Split(",");
        }

        // Method to get types from the provided .cs files
        public async Task<INamedTypeSymbol[]> GetTypesFromDirectoryAsync()
        {
            // If types are not cached, proceed to analyze files
            if (_types == null)
            {
                try
                {
                    var workspace = new AdhocWorkspace(MefHostServices.DefaultHost);  // Create an Adhoc workspace
                    _types = await GetCompilationFromDirectoryAsync(workspace);

                }
                catch (Exception ex)
                {
                    // Handle errors during compilation
                    Console.WriteLine($"Failed to get types: {ex.Message}");
                    throw;
                }
            }

            return _types;
        }

        // Helper method to create a Compilation from all .cs files in the directory
        private async Task<INamedTypeSymbol[]> GetCompilationFromDirectoryAsync(AdhocWorkspace workspace)
        {
            var namedTypeSymbols = new List<INamedTypeSymbol>();

            var projectInfo = ProjectInfo.Create(
                ProjectId.CreateNewId(),
                VersionStamp.Create(),
                "AdhocProject",
                "AdhocAssembly",
                LanguageNames.CSharp);

            var project = workspace.AddProject(projectInfo);

            // Add assemblies as metadata references
            var references = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && File.Exists(a.Location))
                .Select(a => MetadataReference.CreateFromFile(a.Location))
                .Cast<MetadataReference>()
                .ToList();

            project = project.AddMetadataReferences(references);

            // Get all .cs files from the directory
            var csFiles = GetFilesWithExcludedDirectories(_directoryPath, _excludedDirs);

            foreach (var filePath in csFiles)
            {
                var fileContent = await File.ReadAllTextAsync(filePath);  // Read file content
                var sourceText = SourceText.From(fileContent);

                project = project.AddDocument(filePath, sourceText).Project;
            }

            var compilation = await project.GetCompilationAsync();
            if (compilation == null)
            {
                Console.WriteLine("Failed to get compilation.");
                return namedTypeSymbols.ToArray();
            }

            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                var root = await syntaxTree.GetRootAsync();

                var typeDeclarations = root.DescendantNodes()
                    .OfType<Microsoft.CodeAnalysis.CSharp.Syntax.TypeDeclarationSyntax>();

                foreach (var typeDeclaration in typeDeclarations)
                {
                    var typeSymbol = semanticModel.GetDeclaredSymbol(typeDeclaration) as INamedTypeSymbol;
                    if (typeSymbol != null)
                        namedTypeSymbols.Add(typeSymbol);
                }
            }

            return namedTypeSymbols.ToArray();
        }

        // Method to get files from a directory while excluding certain directories
        static IEnumerable<string> GetFilesWithExcludedDirectories(string directoryPath, string[] excludedDirectories)
        {
            // Enumerate files in the current directory (top-level only)
            foreach (var file in Directory.EnumerateFiles(directoryPath, "*.cs", SearchOption.TopDirectoryOnly))
            {
                yield return file;  // Return the file
            }

            // Enumerate subdirectories in the current directory
            foreach (var directory in Directory.EnumerateDirectories(directoryPath))
            {
                var directoryName = Path.GetFileName(directory);  // Get the name of the directory

                // Check if the current directory is in the excluded list
                if (!excludedDirectories.Contains(directoryName, StringComparer.OrdinalIgnoreCase))
                {
                    // Recursively get files from non-excluded subdirectories
                    foreach (var file in GetFilesWithExcludedDirectories(directory, excludedDirectories))
                    {
                        yield return file;
                    }
                }
            }
        }
    }
}
