using System;
using System.Configuration;
using System.IO;
using DbStatusProvider.Configuration;

namespace DbStatusProvider.Core
{
    public static class SchemaVersionParser
    {
        private static readonly DbStatusUpdaterConfiguration Configuration =
            ConfigurationManager.GetSection("dbStatusUpdater") as DbStatusUpdaterConfiguration;

        public static SchemaVersion Parse(this string schemaVersionFile)
        {
            var schemaVersion = Path.GetFileName(schemaVersionFile);

            if (schemaVersion == null || !schemaVersion.StartsWith(Configuration.ScriptsPrefix))
                throw new Exception("Unable to identify the file name");

            return new SchemaVersion
            {
                MajorVersion = short.Parse(schemaVersion.Substring(2, 2)),
                MinorVersion = short.Parse(schemaVersion.Substring(4, 2)),
                ScriptVersion = short.Parse(schemaVersion.Substring(6, 2)),
                ScriptPath = schemaVersionFile
            };
        }
    }
}