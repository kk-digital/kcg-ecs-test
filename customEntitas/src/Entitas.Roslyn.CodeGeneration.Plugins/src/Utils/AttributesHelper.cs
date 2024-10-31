using DesperateDevs.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        public static ISymbol[] GetPublicMembersFix(this ITypeSymbol type, bool includeBaseTypeMembers)
        {
            var array2 = type.GetMembers().Where<ISymbol>(new Func<ISymbol, bool>(IsPublicMember)).Select(member => member.ContainingType.ToDisplayString() + "." + member.Name).ToArray();
            ISymbol[] array = type.GetMembers().Where<ISymbol>(new Func<ISymbol, bool>(IsPublicMember)).ToArray<ISymbol>();
            if (includeBaseTypeMembers && type.BaseType != null && type.BaseType.ToDisplayString() != "object")
                array = ((IEnumerable<ISymbol>)array).Concat<ISymbol>((IEnumerable<ISymbol>)type.BaseType.GetPublicMembers(true)).ToArray<ISymbol>();
            return array;
        }

        private static bool IsPublicMember(ISymbol symbol)
        {
            return (symbol is IFieldSymbol || IsAutoProperty(symbol)) && !symbol.IsStatic && symbol.DeclaredAccessibility == Accessibility.Public && symbol.CanBeReferencedByName;
        }

        private static bool IsAutoProperty(ISymbol symbol)
        {
            return symbol is IPropertySymbol propertySymbol && propertySymbol.SetMethod != null &&
                   propertySymbol.GetMethod != null &&
                   !propertySymbol.GetMethod.DeclaringSyntaxReferences.First<SyntaxReference>().GetSyntax()
                       .DescendantNodes()
                       .Any<SyntaxNode>((Func<SyntaxNode, bool>)(node => node is MethodDeclarationSyntax)) &&
                   !propertySymbol.SetMethod.DeclaringSyntaxReferences.First<SyntaxReference>().GetSyntax()
                       .DescendantNodes()
                       .Any<SyntaxNode>((Func<SyntaxNode, bool>)(node => node is MethodDeclarationSyntax));
        }

        public static ITypeSymbol PublicMemberTypeFix(this ISymbol member)
        {
            return !(member is IFieldSymbol) ? ((IPropertySymbol)member).Type : ((IFieldSymbol)member).Type;
        }
    }
}
