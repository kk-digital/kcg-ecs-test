# Entitas-Standalone

Entitas-Standalone is a pure C# console app using [Entitas](https://github.com/sschmid/Entitas).

There are two project in the solution:

## src/MyProject.csproj
This is a minimal sample project using one generated component.

## jenny/MyProject.Jenny.csproj
Use this project to build the code generator called Jenny.

To start the server please run:

```
dotnet run -c Release --project jenny/RunCodeGenerator.csproj server
```

To generate code using the server please run

```
dotnet jenny/bin/Release/net7.0/RunCodeGenerator.dll client gen
```

To generate without the server

```
dotnet jenny/bin/Release/net7.0/RunCodeGenerator.dll gen
```
or
```
dotnet run -c Release --project jenny/RunCodeGenerator.csproj gen
```


# Goals

[] Create custom version of entitas removing functionality that we don't need.
[] Generator code should be inside a specified namespace one for each component type.

