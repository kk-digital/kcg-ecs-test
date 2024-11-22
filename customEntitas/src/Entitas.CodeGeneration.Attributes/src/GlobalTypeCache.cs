using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Entitas.CodeGeneration.Attributes
{
    public static class GlobalTypeCache
    {
        public static Dictionary<IFieldSymbol, (INamedTypeSymbol MatchingType, INamedTypeSymbol TypeSymbol)> TypeMappings { get; } = new();
        public static Dictionary<string, INamedTypeSymbol> NameToTypeMap { get; } = new();
        public static Dictionary<string, INamedTypeSymbol[]> SearchPathTypeCache { get; } = new();
    }
}
