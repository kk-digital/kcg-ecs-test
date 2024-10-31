using DesperateDevs.Serialization;

namespace MyProject.CodeGenerator
{
    public class GenerationConfig
    {
        // Target directory where generated files should be placed
        public string TargetDirectory { get; set; }

        // Contexts to be used in generation (e.g., "Game,Particle,Vehicle")
        public string Contexts { get; set; }

        // Entitas directory for the generation process
        public string EntitasDir { get; set; }

        // Directories to exclude during the generation process
        public string ExcludedDirs { get; set; }

        // Namespace to be used for the generated code
        public string Namespace { get; set; }

        // Project file path
        public string ProjectFilePath { get; set; }

        // Method to apply preferences (if needed)
        public void ApplyPreferences(Preferences preferences)
        {
            preferences.SetTargetDirectory(TargetDirectory);
            preferences.SetContexts(Contexts);
            preferences.SetProjectPath(EntitasDir);
            preferences.SetExcludedDirs(ExcludedDirs);
            preferences.SetNamespace(Namespace);
            preferences.SetProjectFilePath(ProjectFilePath);
        }
    }
}
