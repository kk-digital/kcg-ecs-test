using System.Collections.Generic;
using DesperateDevs.Serialization;

namespace Jenny.Plugins
{
    public class ProjectPathConfig : AbstractConfigurableConfig
    {
        readonly string _projectPathKey = $"{typeof(ProjectPathConfig).Namespace}.ProjectPath";
        readonly string _excludedDirsKey = $"{typeof(ProjectPathConfig).Namespace}.ExcludedDirs";

        public override Dictionary<string, string> DefaultProperties => new Dictionary<string, string>
        {
            {_projectPathKey, "Assembly-CSharp.csproj"},
            {_excludedDirsKey, ""}
        };

        public string ProjectPath => _preferences[_projectPathKey];
        public string ExcludedDirs => _preferences[_excludedDirsKey];
    }
}
