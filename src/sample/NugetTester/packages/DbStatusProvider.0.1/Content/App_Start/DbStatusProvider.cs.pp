using System.Reflection;
using System.Web;
using DbStatusProvider.Core;
using DbStatusProvider.Objects;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof($rootnamespace$.App_Start.DbStatusProvider), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof($rootnamespace$.App_Start.DbStatusProvider), "Stop")]

namespace $rootnamespace$.App_Start
{
    public static class DbStatusProvider
    {
         public static void Start()
         {
             var setupStatusGenerator = new SetupStatusGenerator(Assembly.GetExecutingAssembly().Location);
             HttpContext.Current.Application[InstallationStatus] = setupStatusGenerator.GetSetupStatus();
         }

         public static void Stop()
         {
             HttpContext.Current.Application[InstallationStatus] = null;
         }

         public static SetupStatus GetStatus()
         {
             return (SetupStatus)HttpContext.Current.Application[InstallationStatus];
         }

         public static void SetStatus()
         {
             HttpContext.Current.Application[InstallationStatus] = null;
         }

         private const string InstallationStatus = "Installation_Status";
    }
}