using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Host.Mef;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entitas.Roslyn.CodeGeneration.Plugins.Utils;
using Microsoft.Build.Locator;

public static class AdhocWorkspaceHelper
{
    public static Dictionary<string, List<MetadataReference>> References = new();
    public static Dictionary<string, object> ObjectCache { get; set; }
    static readonly INamedTypeSymbol[] _types;

    public static async Task<IEnumerable<MetadataReference>> LoadProjectWithDependenciesAsync(string projectPath)
    {
        if(References.ContainsKey(projectPath))
            return References[projectPath];

        // Register MSBuild, which is required to read project files (.csproj)
        if (!MSBuildLocator.IsRegistered)
        {
            MSBuildLocator.RegisterDefaults();
        }

        // Create MSBuild workspace to read project references
        using (var msBuildWorkspace = MSBuildWorkspace.Create())
        {
            var project = await msBuildWorkspace.OpenProjectAsync(projectPath);
            var compilation = await project.GetCompilationAsync();

            if (compilation != null)
            {
                var metadataReferences = compilation.References.ToList();
                References.Add(projectPath, metadataReferences);

                return metadataReferences;
            }
        }

        return new List<MetadataReference>();
    }

    public static INamedTypeSymbol[] LoadTypes(string projectPath)
    {
        var types = _types ?? PluginUtil
            .GetCachedProjectParser(ObjectCache, projectPath)
            .GetTypes();

        return types;
    }
}
