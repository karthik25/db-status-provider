namespace DbStatusProvider.Objects
{
    public interface ISchemaVersion
    {
        short MajorVersion { get; set; }
        short MinorVersion { get; set; }
        short ScriptVersion { get; set; }
        string ScriptPath { get; set; }
    }
}