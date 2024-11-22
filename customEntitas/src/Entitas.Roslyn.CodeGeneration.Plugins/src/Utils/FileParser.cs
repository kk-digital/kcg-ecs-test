using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading;
using Entitas.CodeGeneration.Attributes;
using Jenny.Plugins;

namespace Entitas.Roslyn.CodeGeneration.Plugins.Utils
{
    public class FileParser(ProjectPathConfig projectPathConfig)
    {
        private readonly string _directoryPath = projectPathConfig.ProjectPath;
        private readonly string _searchPaths = projectPathConfig.SearchPaths;
        private readonly string[] _excludedDirs = projectPathConfig.ExcludedDirs.Split(",");
        private INamedTypeSymbol[]? _types; // Nullable to support the nullability feature
        public Dictionary<string, object> ObjectCache { get; set; } = new Dictionary<string, object>();
        // Using global type mappings from GlobalTypeCache

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
                .Select(a => a.Location)
                .Distinct()
                .Select(location => MetadataReference.CreateFromFile(location))
                .ToList();

            project = project.AddMetadataReferences(references);

            INamedTypeSymbol[] types;
            if (!GlobalTypeCache.SearchPathTypeCache.TryGetValue(_searchPaths, out types))
            {
                types = PluginUtil.GetCachedProjectParser(ObjectCache, _searchPaths).GetTypes();
                GlobalTypeCache.SearchPathTypeCache[_searchPaths] = types;
            }

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
                    {
                        // Process all members to ensure proper namespace resolution
                        foreach (var member in typeSymbol.GetMembers())
                        {
                            if (member is IFieldSymbol fieldSymbol)
                            {
                                var fieldType = fieldSymbol.Type;
                                // Ensure proper namespace resolution for field types
                                var containingNamespace = fieldType.ContainingNamespace;
                                if (containingNamespace == null || containingNamespace.IsGlobalNamespace)
                                {
                                    // Search for the type in loaded types collection
                                    var matchingType = types.FirstOrDefault(t =>
                                        t.Name.Equals(fieldType.Name, StringComparison.OrdinalIgnoreCase) &&
                                        !t.ContainingNamespace.IsGlobalNamespace);

                                    if (matchingType != null)
                                    {
                                        GlobalTypeCache.TypeMappings[fieldSymbol] = (matchingType, typeSymbol);
                                        GlobalTypeCache.NameToTypeMap[matchingType.Name] = matchingType;
                                    }
                                }
                            }
                        }

                        if(!namedTypeSymbols.Contains(typeSymbol))
                            namedTypeSymbols.Add(typeSymbol);
                    }
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
