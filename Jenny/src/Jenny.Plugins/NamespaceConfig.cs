using System;
using System.Collections.Generic;
using DesperateDevs.Serialization;

namespace Jenny.Plugins
{
    public class NamespaceConfig : AbstractConfigurableConfig
    {
        readonly string _namespaceKey = "Jenny.Plugins.Namespace";

        public override Dictionary<string, string> DefaultProperties => new Dictionary<string, string>
        { 
            {_namespaceKey, "Generated.Entitas"}
        };

        public string Namespace => _preferences[_namespaceKey];
    }
}
