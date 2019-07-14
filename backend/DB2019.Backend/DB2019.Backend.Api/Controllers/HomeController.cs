using System.Web.Mvc;

namespace DB2019.Backend.Api.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Sample()
        {
            return View();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Map", "Map");
        }
    }
}