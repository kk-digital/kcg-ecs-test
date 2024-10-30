using System.Collections.Generic;
using System.Linq;
using Jenny;
using Jenny.Plugins;
using DesperateDevs.Extensions;
using DesperateDevs.Serialization;
using Entitas.CodeGeneration.Attributes;
using Entitas.CodeGeneration.Plugins;
using Microsoft.CodeAnalysis;
using Entitas.Roslyn.CodeGeneration.Plugins.Utils;

namespace Entitas.Roslyn.CodeGeneration.Plugins
{
    public class CleanupDataProvider : IDataProvider, IConfigurable, ICachable
    {
        public string Name => "Cleanup";
        public int Order => 0;
        public bool RunInDryMode => true;

        public Dictionary<string, string> DefaultProperties => _projectPathConfig.DefaultProperties;

        public Dictionary<string, object> ObjectCache { get; set; }

        readonly ProjectPathConfig _projectPathConfig = new ProjectPathConfig();
        readonly INamedTypeSymbol[] _types;

        Preferences _preferences;
        ComponentDataProvider _componentDataProvider;

        public CleanupDataProvider() : this(null) { }

        public CleanupDataProvider(INamedTypeSymbol[] types)
        {
            _types = types;
        }

        public void Configure(Preferences preferences)
        {
            _preferences = preferences;
            _projectPathConfig.Configure(preferences);
        }

        public CodeGeneratorData[] GetData(IEnumerable<MetadataReference> projReferences)
        {
            var types = _types ?? new FileParser(_projectPathConfig, projReferences).GetTypesFromDirectoryAsync()
                .Result;

            var componentInterface = typeof(IComponent).ToCompilableString();

            var cleanupTypes = types
                .Where(type => type.AllInterfaces.Any(i => i.ToCompilableString() == componentInterface))
                .Where(type => !type.IsAbstract)
                .Where(type => type.GetAttribute<CleanupAttribute>() != null)
                .ToArray();

            var cleanupLookup = cleanupTypes.ToDictionary(
                type => type.ToCompilableString(),
                type => (CleanupMode)type.GetAttribute<CleanupAttribute>().ConstructorArguments[0].Value);

            _componentDataProvider = new ComponentDataProvider(cleanupTypes);
            _componentDataProvider.Configure(_preferences);

            return _componentDataProvider
                .GetData(projReferences)
                .Where(data => !((ComponentData)data).GetTypeName().RemoveComponentSuffix().HasListenerSuffix())
                .Select(data => new CleanupData(data) {cleanupMode = cleanupLookup[((ComponentData)data).GetTypeName()]})
                .ToArray();
        }
    }
}
