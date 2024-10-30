using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Jenny
{
    public interface IDataProvider : ICodeGenerationPlugin
    {
        CodeGeneratorData[] GetData(IEnumerable<MetadataReference> projReferences);
    }
}
