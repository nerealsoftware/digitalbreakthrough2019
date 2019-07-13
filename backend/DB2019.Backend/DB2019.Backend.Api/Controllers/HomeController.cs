using System.Web.Mvc;

namespace DB2019.Backend.Api.Controllers
{
    public class ShowIssue : Controller
    {
        public ActionResult Index()
        {
            var categoryList = CategoryController.InternalCategoryList();
            return View();
        }
    }


    public class HomeController : Controller
    {
        public ActionResult Sample()
        {
            return View();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Help");
        }
    }
}