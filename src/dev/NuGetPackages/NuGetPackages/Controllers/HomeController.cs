using System.Web.Mvc;

namespace NuGetPackages.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            var setupStatus = App_Start.DbStatusProvider.GetStatus();
            return View(setupStatus);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
