using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DB2019.Backend.Api.Helpers;
using DB2019.Backend.Api.Models;

namespace DB2019.Backend.Api.Controllers
{
    public class CategryIssues : List<IssueData>
    {
        public CategoryData Category { get; set; }
        public int Total { get; set; }

        public CategryIssues(IEnumerable<IssueData> source)
        {
            AddRange(source);
        }
    }

    public class ShowIssueController : Controller
    {

        public ActionResult Index()
        {
            var categoryList = LayoutHelper.GetCategories();
            return View(categoryList);
        }

        public ActionResult Category(int? categoryId)
        {
            var categoryList = LayoutHelper.GetCategories();

            var category = categoryId.HasValue ? categoryList.FirstOrDefault(cat => cat.Id == categoryId.Value) : null;
            if (category == null) return RedirectToAction("Index");

            var issues = IssueController.InternalGetIssues(0, 999, category.Id);
            return View(new CategryIssues(issues.Data) { Category = category, Total = issues.TotalCount });
        }
    }
}