﻿using System.Collections.Generic;
using DesperateDevs.Serialization;

namespace Entitas.CodeGeneration.Plugins
{
    public class IgnoreNamespacesConfig : AbstractConfigurableConfig
    {
        const string IGNORE_NAMESPACES_KEY = "Entitas.CodeGeneration.Plugins.IgnoreNamespaces";

        public override Dictionary<string, string> DefaultProperties => new Dictionary<string, string>
        {
            {IGNORE_NAMESPACES_KEY, "false"}
        };

        public bool ignoreNamespaces
        {
            get => _preferences[IGNORE_NAMESPACES_KEY] == "true";
            set => _preferences[IGNORE_NAMESPACES_KEY] = value.ToString();
        }
    }
}
