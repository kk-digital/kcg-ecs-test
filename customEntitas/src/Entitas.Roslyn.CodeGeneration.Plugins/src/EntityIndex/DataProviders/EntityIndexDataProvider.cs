using System;
using System.Collections.Generic;
using System.Linq;
using Jenny;
using Jenny.Plugins;
using DesperateDevs.Extensions;
using DesperateDevs.Roslyn;
using DesperateDevs.Serialization;
using Entitas.CodeGeneration.Attributes;
using Entitas.CodeGeneration.Plugins;
using Microsoft.CodeAnalysis;
using Entitas.Roslyn.CodeGeneration.Plugins.Utils;

namespace Entitas.Roslyn.CodeGeneration.Plugins
{
    public class EntityIndexDataProvider : IDataProvider, IConfigurable, ICachable
    {
        public string Name => "Entity Index (Roslyn)";
        public int Order => 0;
        public bool RunInDryMode => true;

        public Dictionary<string, string> DefaultProperties =>
            _ignoreNamespacesConfig.DefaultProperties.Merge(new[]
            {
                _projectPathConfig.DefaultProperties,
                _contextsComponentDataProvider.DefaultProperties
            });

        public Dictionary<string, object> ObjectCache { get; set; }

        readonly IgnoreNamespacesConfig _ignoreNamespacesConfig = new IgnoreNamespacesConfig();
        readonly ProjectPathConfig _projectPathConfig = new ProjectPathConfig();
        readonly ContextsComponentDataProvider _contextsComponentDataProvider = new ContextsComponentDataProvider();

        readonly INamedTypeSymbol[] _types;

        public EntityIndexDataProvider() : this(null) { }

        public EntityIndexDataProvider(INamedTypeSymbol[] types)
        {
            _types = types;
        }

        public void Configure(Preferences preferences)
        {
            _ignoreNamespacesConfig.Configure(preferences);
            _projectPathConfig.Configure(preferences);
            _contextsComponentDataProvider.Configure(preferences);
        }

        public CodeGeneratorData[] GetData()
        {
            var types = _types ?? new FileParser(_projectPathConfig.ProjectPath, _projectPathConfig.ExcludedDirs).GetTypesFromDirectoryAsync()
                .Result;

            var componentInterface = "IComponent";

            var entityIndexData = types
                .Where(type => type.BaseType.ToCompilableString() == componentInterface)
                .Where(type => !type.IsAbstract)
                .ToDictionary(
                    type => type,
                    type => type.GetPublicMembers(true))
                .Where(kv => kv.Value.Any(symbol => symbol.GetAttribute<PrimaryEntityIndexAttribute>() != null))
                .SelectMany(kv => createEntityIndexData(kv.Key, kv.Value));

            //var entityIndexData = GetEntityIndexData(types, componentInterface);

            var customEntityIndexData = types
                .Where(type => !type.IsAbstract)
                .Where(type => type.GetAttribute<CustomEntityIndexAttribute>() != null)
                .Select(createCustomEntityIndexData);

            return entityIndexData
                .Concat(customEntityIndexData)
                .ToArray();
        }

        // Method to process entity index data without using LINQ
        /*public IEnumerable<EntityIndexData> GetEntityIndexData(IEnumerable<INamedTypeSymbol> types, string componentInterface)
        {
            var result = new List<EntityIndexData>();

            // First filter: Where type.BaseType.ToCompilableString() == componentInterface
            var filteredTypes = new List<INamedTypeSymbol>();
            foreach (var type in types)
            {
                if (type.BaseType != null && type.BaseType.ToCompilableString() == componentInterface)
                {
                    filteredTypes.Add(type);
                }
            }

            // Second filter: Where type is not abstract
            var nonAbstractTypes = new List<INamedTypeSymbol>();
            foreach (var type in filteredTypes)
            {
                if (!type.IsAbstract)
                {
                    nonAbstractTypes.Add(type);
                }
            }

            // Create dictionary: ToDictionary(type => type, type => type.GetPublicMembers(true))
            var typeMemberDictionary = new Dictionary<INamedTypeSymbol, IEnumerable<ISymbol>>();
            foreach (var type in nonAbstractTypes)
            {
                var publicMembers = type.GetPublicMembers(true);
                typeMemberDictionary.Add(type, publicMembers);
            }

            // Filter dictionary: Where kv.Value contains PrimaryEntityIndexAttribute
            var filteredDictionary = new Dictionary<INamedTypeSymbol, IEnumerable<ISymbol>>();
            bool containsPrimaryEntityIndexAttribute = false;
            foreach (var kv in typeMemberDictionary)
            {
                foreach (var symbol in kv.Value)
                {
                    if (symbol.GetAttribute<PrimaryEntityIndexAttribute>() != null)
                    {
                        containsPrimaryEntityIndexAttribute = true;
                        break;
                    }
                }

                if (containsPrimaryEntityIndexAttribute)
                {
                    filteredDictionary.Add(kv.Key, kv.Value);
                }
            }

            // SelectMany: Create entity index data from each dictionary entry
            foreach (var kv in filteredDictionary)
            {
                var entityIndexData = createEntityIndexData(kv.Key, kv.Value.ToArray());
                result.AddRange(entityIndexData);
            }

            return result;
        }*/

        EntityIndexData[] createEntityIndexData(INamedTypeSymbol type, ISymbol[] members)
        {
            var hasMultiple = members.Count(member => member.GetAttribute<PrimaryEntityIndexAttribute>() != null) > 1;
            return members
                .Where(member => member.GetAttribute<PrimaryEntityIndexAttribute>() != null)
                .Select(member =>
                {
                    var data = new EntityIndexData();
                    var attribute = member.GetAttribute<PrimaryEntityIndexAttribute>();

                    data.SetEntityIndexType(getEntityIndexType(attribute));
                    data.IsCustom(false);
                    data.SetEntityIndexName(type.ToCompilableString().ToComponentName(_ignoreNamespacesConfig.ignoreNamespaces));
                    data.SetHasMultiple(hasMultiple);
                    data.SetKeyType(member.PublicMemberType().ToCompilableString());
                    data.SetComponentType(type.ToCompilableString());
                    data.SetMemberName(member.Name);
                    data.SetContextNames(_contextsComponentDataProvider.GetContextNamesOrDefault(type));

                    return data;
                }).ToArray();
        }

        EntityIndexData createCustomEntityIndexData(INamedTypeSymbol type)
        {
            var data = new EntityIndexData();
            var attribute = type.GetAttribute<CustomEntityIndexAttribute>();
            data.SetEntityIndexType(type.ToCompilableString());
            data.IsCustom(true);
            data.SetEntityIndexName(type.ToCompilableString().RemoveDots());
            data.SetHasMultiple(false);
            data.SetContextNames(new[]
            {
                ((INamedTypeSymbol)attribute.ConstructorArguments.First().Value)
                .ToCompilableString()
                .TypeName()
                .RemoveContextSuffix()
            });

            var getMethods = type
                .GetMembers()
                .OfType<IMethodSymbol>()
                .Where(method => method.DeclaredAccessibility == Accessibility.Public)
                .Where(method => !method.IsStatic)
                .Where(method => method.GetAttribute<EntityIndexGetMethodAttribute>() != null)
                .Select(method => new MethodData(
                    method.ReturnType.ToCompilableString(),
                    method.Name,
                    method.Parameters
                        .Select(p => new MemberData(p.ToCompilableString(), p.Name))
                        .ToArray()
                ))
                .ToArray();

            data.SetCustomMethods(getMethods);

            return data;
        }

        string getEntityIndexType(AttributeData attribute)
        {
            var entityIndexType = attribute.ToString();
            switch (entityIndexType)
            {
                case "EntityIndex":
                    return "Entitas.EntityIndex";
                case "PrimaryEntityIndex":
                    return "Entitas.PrimaryEntityIndex";
                default:
                    throw new Exception($"Unhandled EntityIndexType: {entityIndexType}");
            }
        }
    }
}
