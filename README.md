There are Three project in the solution:

## src/kcg-ecs-test.csproj
This is a minimal sample project using to test our entitas verison

## jenny/RunCodeGenerator.Jenny.csproj
Use this project to build the code generator called Jenny.

## customEntitas/
This is the custom version of entitas that we are using. It is a fork of the original entitas project.
Custom Entitas is going to be modified removing functionality that we don't need. Making it more lightweight
custom Entitas should make generator code be para of same namespace of the component.

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

