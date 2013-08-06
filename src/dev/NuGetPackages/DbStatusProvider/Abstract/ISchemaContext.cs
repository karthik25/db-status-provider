using System;
using System.Collections.Generic;
using DbStatusProvider.Objects;

namespace DbStatusProvider.Abstract
{
    public interface ISchemaContext : IDisposable
    {
        IList<ISchemaVersion> GetScriptsInstalled();
        void AddScript(ISchemaVersion schemaVersion);
    }
}