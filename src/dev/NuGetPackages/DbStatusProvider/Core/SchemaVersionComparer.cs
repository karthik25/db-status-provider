using System.Collections.Generic;

namespace DbStatusProvider.Core
{
    public class SchemaVersionComparer : IComparer<SchemaVersion>
    {
        public int Compare(SchemaVersion x, SchemaVersion y)
        {
            var currentVersion = short.Parse(string.Format("{0}{1}{2}", x.MajorVersion, x.MinorVersion, x.ScriptVersion));
            var otherVersion = short.Parse(string.Format("{0}{1}{2}", y.MajorVersion, y.MinorVersion, y.ScriptVersion));
            return currentVersion.CompareTo(otherVersion);
        }
    }
}