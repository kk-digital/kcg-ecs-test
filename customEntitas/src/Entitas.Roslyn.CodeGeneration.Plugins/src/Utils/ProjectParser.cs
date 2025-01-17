using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace Entitas.Roslyn.CodeGeneration.Plugins.Utils
{
    public static class PluginUtil
    {
        public static readonly string ProjectParserKey = typeof(PluginUtil).Namespace + ".ProjectParser";

        public static ProjectParser GetCachedProjectParser(
            Dictionary<string, object> objectCache,
            string projectPath)
        {
            object cachedProjectParser;
            if (!objectCache.TryGetValue(PluginUtil.ProjectParserKey, out cachedProjectParser))
            {
                cachedProjectParser = (object)new ProjectParser(projectPath);
                objectCache.Add(PluginUtil.ProjectParserKey, cachedProjectParser);
            }
            return (ProjectParser)cachedProjectParser;
        }
    }

    public class ProjectParser
    {
        private readonly
#nullable disable
            Project _project;
        private INamedTypeSymbol[] _types;

        public ProjectParser(string projectPath)
        {
            if (!MSBuildLocator.IsRegistered)
                MSBuildLocator.RegisterDefaults();
            using (MSBuildWorkspace msBuildWorkspace = MSBuildWorkspace.Create())
                this._project = msBuildWorkspace.OpenProjectAsync(projectPath).Result;
        }

        public INamedTypeSymbol[] GetTypes()
        {
            if (this._types == null)
            {
                var compilation = this._project.GetCompilationAsync().Result;
                var types = new List<INamedTypeSymbol>();

                // Get types from the current project
                types.AddRange(compilation.GetSymbolsWithName((Func<string, bool>)(name => true), SymbolFilter.Type)
                    .OfType<INamedTypeSymbol>());

                // Get types from referenced assemblies
                foreach (var reference in compilation.References)
                {
                    var assemblySymbol = compilation.GetAssemblyOrModuleSymbol(reference) as IAssemblySymbol;
                    if (assemblySymbol != null)
                    {
                        var assemblyTypes = assemblySymbol.GlobalNamespace
                            .GetNamespaceMembers()
                            .SelectMany(ns => ns.GetTypeMembers())
                            .Where(t => !t.IsAbstract && !t.IsGenericType);
                        types.AddRange(assemblyTypes);
                    }
                }

                this._types = types.ToArray();
            }
            return this._types;
        }
    }
}
