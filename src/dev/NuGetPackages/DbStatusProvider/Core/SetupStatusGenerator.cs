using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using DbStatusProvider.Abstract;
using DbStatusProvider.Configuration;
using DbStatusProvider.Enumerations;
using DbStatusProvider.Objects;

namespace DbStatusProvider.Core
{
    public class SetupStatusGenerator
    {
        private readonly string _executingAssembly;

        private static readonly DbStatusUpdaterConfiguration Configuration =
            ConfigurationManager.GetSection("dbStatusUpdater") as DbStatusUpdaterConfiguration;

        public SetupStatusGenerator(string executingAssembly)
        {
            _executingAssembly = executingAssembly;
        }

        public SetupStatus GetSetupStatus()
        {
            var setupStatus = new SetupStatus();

            try
            {
                if (string.IsNullOrEmpty(Configuration.ContextType))
                    return null;

                if (string.IsNullOrEmpty(Configuration.ScriptsBase))
                    return null;

                var schemaContext = (ISchemaContext)Activator.CreateInstanceFrom(_executingAssembly, Configuration.ContextType).Unwrap();

                if (schemaContext == null)
                    return null;
                
                var abstractList = schemaContext.GetScriptsInstalled();
                var allEntries = abstractList
                    .Select(a => new SchemaVersion
                    {
                        ScriptPath = a.ScriptPath, 
                        MajorVersion = a.MajorVersion, 
                        MinorVersion = a.MinorVersion, 
                        ScriptVersion = a.ScriptVersion
                    })
                    .ToList();
                allEntries.Sort(new SchemaVersionComparer());

                var schemaInstance = allEntries.LastOrDefault();

                if (schemaInstance == null)
                {
                    setupStatus.StatusCode = SetupStatusCode.NotInstalled;
                    setupStatus.Message = "None of the scripts have been run";

                    return setupStatus;
                }

                var sortedList = new SortedSet<SchemaVersion>();
                var filePath = HostingEnvironment.MapPath(Configuration.ScriptsBase);
                if (filePath != null)
                {
                    var files = Directory.GetFiles(filePath).ToList();
                    files.ForEach(f =>
                    {
                        var schemaItem = f.Parse();
                        sortedList.Add(schemaItem);
                    });
                }

                var lastInstance = sortedList.LastOrDefault();

                if (lastInstance != null && lastInstance.Equals(schemaInstance))
                {
                    setupStatus.StatusCode = SetupStatusCode.NoUpdates;
                    setupStatus.Message = "Your instance is up to date!";

                    return setupStatus;
                }

                var scriptsToBeRan = sortedList.Except(allEntries, new SchemaVersionEqualityComparer()).ToList();

                setupStatus.StatusCode = SetupStatusCode.HasUpdates;
                setupStatus.Message = "Your instance has some updates";
                setupStatus.FullPathsOfScripts = scriptsToBeRan;
                setupStatus.ScriptsPending = scriptsToBeRan.Select(s => Path.GetFileName(s.ScriptPath)).ToList();

                return setupStatus;
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("Invalid object name"))
                {
                    setupStatus.StatusCode = SetupStatusCode.DatabaseNotSetup;
                    setupStatus.Message = "Database has not been setup";
                }
                else
                {
                    setupStatus.StatusCode = SetupStatusCode.DatabaseError;
                    setupStatus.Message = exception.Message;
                }
            }

            return setupStatus;
        }
    }
}