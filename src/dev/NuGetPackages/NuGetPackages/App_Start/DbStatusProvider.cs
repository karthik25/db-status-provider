using System.Reflection;
using System.Web;
using DbStatusProvider.Core;
using DbStatusProvider.Objects;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(NuGetPackages.App_Start.DbStatusProvider), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NuGetPackages.App_Start.DbStatusProvider), "Stop")]

namespace NuGetPackages.App_Start
{
    public static class DbStatusProvider
    {
         public static void Start()
         {
             SetStatus();
         }

         public static void Stop()
         {
             ClearStatus();
         }

         public static SetupStatus GetStatus()
         {
             return (SetupStatus)HttpContext.Current.Application[InstallationStatus];
         }

         public static SetupStatus ResetStatus()
         {
             SetStatus();
             return GetStatus();
         }

         public static void ClearStatus()
         {
             HttpContext.Current.Application[InstallationStatus] = null;
         }

         private static void SetStatus()
         {
             var setupStatusGenerator = new SetupStatusGenerator(Assembly.GetExecutingAssembly().Location);
             HttpContext.Current.Application[InstallationStatus] = setupStatusGenerator.GetSetupStatus();
         }

         private const string InstallationStatus = "Installation_Status";
    }
}