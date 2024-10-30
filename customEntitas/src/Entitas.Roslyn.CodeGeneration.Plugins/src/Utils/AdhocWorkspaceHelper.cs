using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Host.Mef;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Build.Locator;
using System;

public static class AdhocWorkspaceHelper
{
    public static async Task<IEnumerable<MetadataReference>> LoadProjectWithDependenciesAsync(string projectPath)
    {
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
                var metadataReferences = compilation.References;

                return metadataReferences;
            }
        }

        return new List<MetadataReference>();
    }
}
