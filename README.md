There are Three project in the solution:

## src/kcg-ecs-test.csproj
This is a minimal sample project using to test our entitas verison

## jenny/RunCodeGenerator.Jenny.csproj
Use this project to build the code generator called Jenny.

## customEntitas/ Jenny Server

This is a fork of entitas ECS.

- Custom Entitas needs to be modified to removing functionality that we don't need. 
- We need to determine, for the example, which parts of Custom Entias, are not used and can be commented out or removed
- The generated also, also need to be generated to a specific folder and inside of a namespace
- - current all code output is going into folder /generated/ and the outputted code has no namespace

- we need to have a configuration file with
- - assembly/namespace, input folder, output folder, output namespace
- - then run the code generator, once for each input folder

The current configuration is in Jenny.properties
- open Jenny.properties to see the current input

## Running the Entias Jenny Code generation Server

To start the server please run:

```
dotnet run -c Release --project jenny/RunCodeGenerator.csproj server
```

To generate code using the server please run

```
dotnet jenny/bin/Release/net7.0/RunCodeGenerator.dll client gen
```

## Running Code Generator Without hte Jenny Server

To generate without the server

```
dotnet jenny/bin/Release/net7.0/RunCodeGenerator.dll gen
```
or
```
dotnet run -c Release --project jenny/RunCodeGenerator.csproj gen
```

## Jenny.properties

There is only one output folder
- we need (input folder, output folder, namespace) and need to run code generator for each input folder, and the result to go to the correct output folder

```
Jenny.Plugins.TargetDirectory = src/generated
```

We need to determine which generators we can disable and which are not used.

```
Jenny.SearchPaths = jenny/bin/Release/net7.0
Jenny.Plugins = Entitas.CodeGeneration.Plugins, \
                Entitas.Roslyn.CodeGeneration.Plugins, \
                Entitas.VisualDebugging.CodeGeneration.Plugins, \
                Jenny.Plugins, \
                Jenny.Plugins.Unity
Jenny.PreProcessors = Jenny.Plugins.ValidateProjectPathPreProcessor
Jenny.DataProviders = Entitas.CodeGeneration.Plugins.ContextDataProvider, \
                      Entitas.Roslyn.CodeGeneration.Plugins.CleanupDataProvider, \
                      Entitas.Roslyn.CodeGeneration.Plugins.ComponentDataProvider, \
                      Entitas.Roslyn.CodeGeneration.Plugins.EntityIndexDataProvider
Jenny.CodeGenerators = Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator, \
                       Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator, \
                       Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator, \
                       Entitas.CodeGeneration.Plugins.ComponentGenerator, \
                       Entitas.CodeGeneration.Plugins.ComponentLookupGenerator, \
                       Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator, \
                       Entitas.CodeGeneration.Plugins.ContextAttributeGenerator, \
                       Entitas.CodeGeneration.Plugins.ContextGenerator, \
                       Entitas.CodeGeneration.Plugins.ContextMatcherGenerator, \
                       Entitas.CodeGeneration.Plugins.ContextsGenerator, \
                       Entitas.CodeGeneration.Plugins.EntityGenerator, \
                       Entitas.CodeGeneration.Plugins.EntityIndexGenerator, \
                       Entitas.CodeGeneration.Plugins.EventEntityApiGenerator, \
                       Entitas.CodeGeneration.Plugins.EventListenerComponentGenerator, \
                       Entitas.CodeGeneration.Plugins.EventListenerInterfaceGenerator, \
                       Entitas.CodeGeneration.Plugins.EventSystemGenerator, \
                       Entitas.CodeGeneration.Plugins.EventSystemsGenerator, \
                       Entitas.Roslyn.CodeGeneration.Plugins.CleanupSystemGenerator, \
                       Entitas.Roslyn.CodeGeneration.Plugins.CleanupSystemsGenerator, \
                       Entitas.VisualDebugging.CodeGeneration.Plugins.ContextObserverGenerator, \
                       Entitas.VisualDebugging.CodeGeneration.Plugins.FeatureClassGenerator
Jenny.PostProcessors = Jenny.Plugins.AddFileHeaderPostProcessor, \
                       Jenny.Plugins.CleanTargetDirectoryPostProcessor, \
                       Jenny.Plugins.MergeFilesPostProcessor, \
                       Jenny.Plugins.WriteToDiskPostProcessor, \
                       Jenny.Plugins.ConsoleWriteLinePostProcessor
Jenny.Server.Port = 3333
Jenny.Client.Host = localhost
Jenny.Plugins.ProjectPath = src/kcg-ecs-test.csproj
Entitas.CodeGeneration.Plugins.Contexts = Game,\
                                          Particle,\
                                          Vehicle
Entitas.CodeGeneration.Plugins.IgnoreNamespaces = false
Jenny.Plugins.TargetDirectory = src/generated
```

Example.
- Events are definately not used.
- visual debugging is definately not used

We need to determine which modules are emitting code, for our inputs
- maybe by outputting a log file or debug prints
