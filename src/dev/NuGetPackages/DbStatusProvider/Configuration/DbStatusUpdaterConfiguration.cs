using System.Configuration;

namespace DbStatusProvider.Configuration
{
    public class DbStatusUpdaterConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("contextType", DefaultValue = "")]
        public string ContextType
        {
            get { return (string) this["contextType"]; }
            set { this["contextType"] = value; }
        }

        [ConfigurationProperty("scriptsBase", DefaultValue = "")]
        public string ScriptsBase
        {
            get { return (string) this["scriptsBase"]; }
            set { this["scriptsBase"] = value; }
        }

        [ConfigurationProperty("scriptsPrefix", DefaultValue = "")]
        public string ScriptsPrefix
        {
            get { return (string) this["scriptsPrefix"]; }
            set { this["scriptsPrefix"] = value; }
        }
    }
}