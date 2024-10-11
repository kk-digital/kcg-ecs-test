using DesperateDevs.Serialization;

namespace MyProject.CodeGenerator
{
    public static class GeneratorHelpers
    {
        public static void SetTargetDirectory(this Preferences preferences, string targetDirectory)
        {
            preferences["Jenny.Plugins.TargetDirectory"] = targetDirectory;
        }

        public static void SetContexts(this Preferences preferences, string contexts)
        {
            preferences["Entitas.CodeGeneration.Plugins.Contexts"] = contexts;
        }

        public static void SetProjectPath(this Preferences preferences, string projectPath)
        {
            preferences["Jenny.Plugins.ProjectPath"] = projectPath;
        }
    }
}
