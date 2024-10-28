using DesperateDevs.Roslyn;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitas.Roslyn.CodeGeneration.Plugins.Utils
{
    public static class AttributesHelper
    {
        public static AttributeData GetAttributeFix<T>(this ISymbol type, bool inherit = false)
        {
            return ((IEnumerable<AttributeData>)type.GetAttributes<T>(inherit)).SingleOrDefault<AttributeData>();
        }

        public static AttributeData[] GetAttributes<T>(this ISymbol type, bool inherit = false)
        {
            return type.GetAttributes().Where<AttributeData>((Func<AttributeData, bool>)(attr => IsAttributeType<T>(attr, inherit))).ToArray<AttributeData>();
        }

        private static bool IsAttributeType<T>(AttributeData attr, bool inherit = false)
        {
            switch (typeof(T).Name)
            {
                case "PrimaryEntityIndexAttribute":
                case "EntityIndexAttribute":
                    return attr.AttributeClass.ToCompilableString() == "PrimaryEntityIndex" || attr.AttributeClass.ToCompilableString() == "EntityIndex";

                case "CustomEntityIndexAttribute":
                    return attr.AttributeClass.ToCompilableString() == "CustomEntityIndex";

                default:
                    return false;
            }
        }

    }
}
