using System.Linq;
using DesperateDevs.Extensions;
using DesperateDevs.Roslyn;
using Entitas.CodeGeneration.Attributes;
using Entitas.CodeGeneration.Plugins;
using Microsoft.CodeAnalysis;

namespace Entitas.Roslyn.CodeGeneration.Plugins
{
    public class MemberDataComponentDataProvider : IComponentDataProvider
    {
        readonly string _componentInterface = typeof(IComponent).ToCompilableString();

        public void Provide(INamedTypeSymbol type, ComponentData data)
        {
            var isComponent = type.AllInterfaces.Any(i => i.ToCompilableString() == _componentInterface);

            var memberData = type.GetPublicMembers(isComponent)
                .Select(info =>
                {
                    var typeName = info.PublicMemberType().ToCompilableString();
                    // Try to get the resolved type from the global cache
                    if (GlobalTypeCache.NameToTypeMap.TryGetValue(typeName, out var resolvedType))
                    {
                        typeName = resolvedType.ToDisplayString();
                    }
                    return new MemberData(typeName, info.Name);
                })
                .ToArray();

            data.SetMemberData(memberData);

            data.SetMemberData(memberData);
        }
    }
}
