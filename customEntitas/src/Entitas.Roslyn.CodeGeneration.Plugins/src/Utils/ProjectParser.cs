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
                this._types = this._project.GetCompilationAsync().Result.GetSymbolsWithName((Func<string, bool>)(name => true), SymbolFilter.Type).OfType<INamedTypeSymbol>().ToArray<INamedTypeSymbol>();
            return this._types;
        }
    }
}
