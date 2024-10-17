using Microsoft.CodeAnalysis;
using System.Linq;

namespace Entitas.Roslyn.CodeGeneration.Plugins.Utils
{
    public static class AttributeHelper
    {
        public static AttributeData GetAttribute<T>(this ISymbol type)
        {
            return type.GetAttributes().FirstOrDefault(a => $"{a}Attribute" == typeof(T).Name);
        }
    }
}
