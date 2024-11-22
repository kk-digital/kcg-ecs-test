using System.Linq;
using DesperateDevs.Roslyn;
using Entitas.CodeGeneration.Attributes;
using Entitas.CodeGeneration.Plugins;
using Microsoft.CodeAnalysis;

namespace Entitas.Roslyn.CodeGeneration.Plugins
{
    public class ShouldGenerateComponentIndexComponentDataProvider : IComponentDataProvider
    {
        public void Provide(INamedTypeSymbol type, ComponentData data)
        {
            data.ShouldGenerateIndex(getGenerateIndex(type));
        }

        bool getGenerateIndex(INamedTypeSymbol type)
        {
            var attr = type.GetAttribute<DontGenerateAttribute>();
            return attr == null || (attr.ConstructorArguments.Length > 0 && (bool)(attr.ConstructorArguments.First().Value));
        }
    }
}
