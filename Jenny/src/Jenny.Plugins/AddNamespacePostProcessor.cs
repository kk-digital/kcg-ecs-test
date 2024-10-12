using DesperateDevs.Serialization;
using System;
using System.Collections.Generic;

namespace Jenny.Plugins
{
    public class AddNamespacePostProcessor : IPostProcessor, IConfigurable
    {
        public string Name => "Add namespace";
        public int Order => 0;
        public bool RunInDryMode => true;

        public Dictionary<string, string> DefaultProperties => _namespaceConfig.DefaultProperties;

        readonly NamespaceConfig _namespaceConfig = new();

        public const string NamespaceFormat =
            @"namespace {0}
{{
";

        public void Configure(Preferences preferences)
        {
            _namespaceConfig.Configure(preferences);
        }

        public CodeGenFile[] PostProcess(CodeGenFile[] files)
        {
            foreach (var file in files)
                file.FileContent = string.Format(NamespaceFormat, _namespaceConfig.Namespace) + file.FileContent + $"{Environment.NewLine}}}";

            return files;
        }
    }
}
