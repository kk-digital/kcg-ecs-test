using System.Linq;
using DesperateDevs.Extensions;
using DesperateDevs.Roslyn;
using Entitas.CodeGeneration.Plugins;
using Microsoft.CodeAnalysis;

namespace Entitas.Roslyn.CodeGeneration.Plugins
{
    public class ShouldGenerateComponentComponentDataProvider : IComponentDataProvider
    {
        public void Provide(INamedTypeSymbol type, ComponentData data)
        {
            var componentInterface = "IComponent";

            var shouldGenerateComponent = type.BaseType.ToCompilableString() != componentInterface;
            data.ShouldGenerateComponent(shouldGenerateComponent);
            if (shouldGenerateComponent)
                data.SetObjectTypeName(type.ToCompilableString());
        }
    }
}
