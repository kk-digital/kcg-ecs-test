using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entitas.Roslyn.CodeGeneration.Plugins.Utils
{
    public static class SymbolExtensionFix
    {
        public static AttributeData GetAttribute<T>(this ISymbol type, bool inherit = false)
        {
            return ((IEnumerable<AttributeData>)type.GetAttributes<T>(inherit)).SingleOrDefault<AttributeData>();
        }

        public static AttributeData[] GetAttributes<T>(this ISymbol type, bool inherit = false)
        {
            return type.GetAttributes().Where<AttributeData>((Func<AttributeData, bool>)(attr => IsAttributeType<T>(attr, inherit))).ToArray<AttributeData>();
        }

        public static ISymbol[] GetPublicMembers(this ITypeSymbol type, bool includeBaseTypeMembers)
        {
            ISymbol[] array = type.GetMembers().Where<ISymbol>(new Func<ISymbol, bool>(IsPublicMember)).ToArray<ISymbol>();
            if (includeBaseTypeMembers && type.BaseType != null && type.BaseType.ToDisplayString() != "object")
                array = ((IEnumerable<ISymbol>)array).Concat<ISymbol>((IEnumerable<ISymbol>)type.BaseType.GetPublicMembers(true)).ToArray<ISymbol>();
            return array;
        }

        public static ITypeSymbol PublicMemberType(this ISymbol member)
        {
            return !(member is IFieldSymbol) ? ((IPropertySymbol)member).Type : ((IFieldSymbol)member).Type;
        }

        public static string ToCompilableString(this ISymbol symbol)
        {
            return symbol.ToDisplayString().Replace("*", string.Empty);
        }

        private static bool IsAttributeType<T>(AttributeData attr, bool inherit)
        {
            return !inherit || attr.AttributeClass == null
                ? attr.AttributeClass.ToCompilableString() == typeof(T).FullName
                : attr.AttributeClass.BaseType.ToCompilableString() == typeof(T).FullName;
        }

        private static bool IsPublicMember(ISymbol symbol)
        {
            return (symbol is IFieldSymbol || IsAutoProperty(symbol)) && !symbol.IsStatic && symbol.DeclaredAccessibility == Accessibility.Public && symbol.CanBeReferencedByName;
        }

        private static bool IsAutoProperty(ISymbol symbol)
        {
            return symbol is IPropertySymbol propertySymbol && propertySymbol.SetMethod != null && propertySymbol.GetMethod != null && !propertySymbol.GetMethod.DeclaringSyntaxReferences.First<SyntaxReference>().GetSyntax().DescendantNodes().Any<SyntaxNode>((Func<SyntaxNode, bool>)(node => node is MethodDeclarationSyntax)) && !propertySymbol.SetMethod.DeclaringSyntaxReferences.First<SyntaxReference>().GetSyntax().DescendantNodes().Any<SyntaxNode>((Func<SyntaxNode, bool>)(node => node is MethodDeclarationSyntax));
        }
    }
}
