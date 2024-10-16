using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DesperateDevs.Serialization;
using Jenny.Generator;
using Sherlog;
using Sherlog.Appenders;

namespace MyProject.CodeGenerator;

public static class CustomGenerationApp
{
    // Logger instance for logging information during the generation process
    static readonly Logger _logger = Logger.GetLogger(typeof(CustomGenerationApp));

    public static void Main(string[] args)
    {
        var watch = new Stopwatch();
        watch.Start();

        SetupLogger();  // Set up logging to console and file

        Preferences preferences = InitializePreferences();  // Initialize preferences for code generation
        
        var genConfig1 = new GenerationConfig
        {
            TargetDirectory = "../../../../src/generatedEcs",
            Contexts        = "Game",
            EntitasDir      = "../../../../src",
            ExcludedDirs    = "particles, vehicle",
            Namespace       = "GeneratedEcs"
        };

        GenerationConfig genConfigParticle = new GenerationConfig
        {
            TargetDirectory = "../../../../src/particles/generated",
            Contexts = "Particle",
            EntitasDir = "../../../../src/particles",
            ExcludedDirs = "generated",
            Namespace = "Particle"
        };

        GenerationConfig genConfigVehicle = new GenerationConfig
        {
            TargetDirectory = "../../../../src/vehicle/generated",
            Contexts = "Vehicle",
            EntitasDir = "../../../../src/vehicle",
            ExcludedDirs = "",
            Namespace = "Vehicle" };

        // List of generation configurations to iterate over
        var configs = new[] { genConfigParticle, genConfigVehicle };

        foreach (var generationConfig in configs)
        {
            generationConfig.ApplyPreferences(preferences);  // Apply preferences based on the current generation configuration

            var codeGenerator = CodeGeneratorUtil.CodeGeneratorFromPreferences(preferences);  // Create code generator from preferences

            // Subscribe to progress events during code generation
            codeGenerator.OnProgress += (title, info, progress) =>
            {
                var p = (int)(progress * 100);
                _logger.Info($"{p}%: {title}. {info}.");
            };

            // Generate the code and measure the time taken
            var generatedFiles = codeGenerator.Generate();
            watch.Restart();

            _logger.Info($"[{DateTime.Now.ToLongTimeString()}] Generated {generatedFiles.Length} files in {(watch.ElapsedMilliseconds / 1000f):0.0} seconds");
        }

        watch.Stop();
    }

    // Method to initialize preferences for code generation
    private static Preferences InitializePreferences()
    {
        var preferencesDic = new Dictionary<string, string>
        {
            { "Jenny.SearchPaths", "." },
            { "Jenny.Plugins", "Entitas.CodeGeneration.Plugins, Jenny.Plugins, Entitas.Roslyn.CodeGeneration.Plugins" },
            { "Jenny.PreProcessors", "" },
            {
                "Jenny.DataProviders",
                "Entitas.CodeGeneration.Plugins.ContextDataProvider, Entitas.Roslyn.CodeGeneration.Plugins.CleanupDataProvider, Entitas.Roslyn.CodeGeneration.Plugins.ComponentDataProvider, Entitas.Roslyn.CodeGeneration.Plugins.EntityIndexDataProvider"
            },
            {
                "Jenny.CodeGenerators",
                "Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator, Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator, Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator, Entitas.CodeGeneration.Plugins.ComponentGenerator, Entitas.CodeGeneration.Plugins.ComponentLookupGenerator, Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator, Entitas.CodeGeneration.Plugins.ContextAttributeGenerator, Entitas.CodeGeneration.Plugins.ContextGenerator, Entitas.CodeGeneration.Plugins.ContextMatcherGenerator, Entitas.CodeGeneration.Plugins.ContextsGenerator, Entitas.CodeGeneration.Plugins.EntityGenerator, Entitas.CodeGeneration.Plugins.EntityIndexGenerator, Entitas.CodeGeneration.Plugins.EventEntityApiGenerator, Entitas.CodeGeneration.Plugins.EventListenerComponentGenerator, Entitas.CodeGeneration.Plugins.EventListenerInterfaceGenerator, Entitas.CodeGeneration.Plugins.EventSystemGenerator, Entitas.CodeGeneration.Plugins.EventSystemsGenerator, Entitas.Roslyn.CodeGeneration.Plugins.CleanupSystemGenerator, Entitas.Roslyn.CodeGeneration.Plugins.CleanupSystemsGenerator, Entitas.VisualDebugging.CodeGeneration.Plugins.ContextObserverGenerator, Entitas.VisualDebugging.CodeGeneration.Plugins.FeatureClassGenerator"
            },
            {
                "Jenny.PostProcessors",
                "Jenny.Plugins.AddFileHeaderPostProcessor, Jenny.Plugins.CleanTargetDirectoryPostProcessor, Jenny.Plugins.MergeFilesPostProcessor, Jenny.Plugins.AddNamespacePostProcessor, Jenny.Plugins.WriteToDiskPostProcessor, Jenny.Plugins.ConsoleWriteLinePostProcessor"
            },
            { "Entitas.CodeGeneration.Plugins.IgnoreNamespaces", "false" }
        };

        var preferencesPath = Path.GetTempFileName();  // Create a temporary file for preferences
        using (var writer = new StreamWriter(preferencesPath))
        {
            foreach (var kvp in preferencesDic)
            {
                // Write each preference key-value pair to the temporary file
                writer.WriteLine($"{kvp.Key} = {kvp.Value.Replace(",", ", \\\r                ")}");
            }
        }

        _logger.Debug($"Created temp preferences file: {preferencesPath}");

        return new Preferences(preferencesPath, null);  // Return a Preferences instance initialized with the temporary file
    }

    // Method to set up the logger for console and file output
    private static void SetupLogger()
    {
        // Add colorful console messages
        var consoleAppender = new ConsoleAppender(new Dictionary<LogLevel, ConsoleColor>
        {
            { LogLevel.Trace, ConsoleColor.Cyan },
            { LogLevel.Debug, ConsoleColor.Blue },
            { LogLevel.Info, ConsoleColor.White },
            { LogLevel.Warn, ConsoleColor.Yellow },
            { LogLevel.Error, ConsoleColor.Red },
            { LogLevel.Fatal, ConsoleColor.Magenta },
        });

        Logger.AddAppender(consoleAppender.WriteLine);  // Add console appender to the logger

        var fileAppender = new FileWriterAppender("generation.log");  // Create file appender for logging to a file
        Logger.AddAppender(fileAppender.WriteLine);  // Add file appender to the logger
    }
}