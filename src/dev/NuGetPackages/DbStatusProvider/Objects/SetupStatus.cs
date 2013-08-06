using System.Collections.Generic;
using DbStatusProvider.Enumerations;

namespace DbStatusProvider.Objects
{
    public class SetupStatus
    {
        public SetupStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public IList<string> ScriptsPending { get; set; }
    }
}