using System.Collections.Generic;
using DbStatusProvider.Core;
using DbStatusProvider.Enumerations;

namespace DbStatusProvider.Objects
{
    public class SetupStatus
    {
        public SetupStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public List<SchemaVersion> FullPathsOfScripts { get; set; } 
        public IList<string> ScriptsPending { get; set; }
    }
}