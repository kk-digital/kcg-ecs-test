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
    static readonly Logger _logger = Logger.GetLogger(typeof(CustomGenerationApp));

    public static void Main(string[] args)
    {
        var watch = new Stopwatch();
        watch.Start();

        SetupLogger();

        var preferences = InitializePreferences();
        preferences.SetTargetDirectory("../../../../src/generated");
        preferences.SetContexts("Game,Particle,Vehicle");
        preferences.SetProjectPath("../../../../src");
        preferences.SetExcludedDirs("generated");

        var codeGenerator = CodeGeneratorUtil.CodeGeneratorFromPreferences(preferences);

        codeGenerator.OnProgress += (title, info, progress) =>
        {
            var p = (int)(progress * 100);

            _logger.Info($"{p}%: {title}. {info}.");
        };

        var generatedFiles = codeGenerator.Generate();
        watch.Stop();

        _logger.Info($"[{DateTime.Now.ToLongTimeString()}] Generated {generatedFiles.Length} files in {(watch.ElapsedMilliseconds / 1000f):0.0} seconds");
    }

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
                "Jenny.Plugins.AddFileHeaderPostProcessor, Jenny.Plugins.CleanTargetDirectoryPostProcessor, Jenny.Plugins.MergeFilesPostProcessor, Jenny.Plugins.WriteToDiskPostProcessor, Jenny.Plugins.ConsoleWriteLinePostProcessor"
            },
            { "Entitas.CodeGeneration.Plugins.IgnoreNamespaces", "false" }
        };

        var preferencesPath = Path.GetTempFileName();
        using (var writer = new StreamWriter(preferencesPath))
        {
            foreach (var kvp in preferencesDic)
            {
                writer.WriteLine($"{kvp.Key} = {kvp.Value.Replace(",", ", \\\r                ")}");
            }
        }

        _logger.Debug($"Created temp preferences file: {preferencesPath}");

        return new Preferences(preferencesPath, null);
    }

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

        Logger.AddAppender(consoleAppender.WriteLine);

        var fileAppender = new FileWriterAppender("generation.log");
        Logger.AddAppender(fileAppender.WriteLine);
    }
}
