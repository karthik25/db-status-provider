using System.Collections.Generic;
using DbStatusProvider.Abstract;
using DbStatusProvider.Core;
using DbStatusProvider.Objects;

namespace NugetTester.Contexts
{
    public class SchemaContext : ISchemaContext
    {
        public IList<ISchemaVersion> GetScriptsInstalled()
        {
            return new List<ISchemaVersion>
                {
                    new SchemaVersion { ScriptPath = "sc010001.sql", MajorVersion = 1, MinorVersion = 0, ScriptVersion = 1 },
                    new SchemaVersion { ScriptPath = "sc020001.sql", MajorVersion = 2, MinorVersion = 0, ScriptVersion = 1 }
                };
        }

        public void AddScript(ISchemaVersion schemaVersion)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}