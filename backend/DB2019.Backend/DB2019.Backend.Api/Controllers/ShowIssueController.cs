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

        public ActionResult Category(int? id)
        {
            var categoryList = LayoutHelper.GetCategories();

            var category = id.HasValue ? categoryList.FirstOrDefault(cat => cat.Id == id.Value) : null;
            if (category == null) return RedirectToAction("Index");

            var issues = IssueController.InternalGetIssues(0, -1, category.Id);
            return View(new CategryIssues(issues.Data) { Category = category, Total = issues.TotalCount });
        }

        public ActionResult ById(int? id)
        {
            var issue = id.HasValue ? IssueController.GetById(id.Value) : null;
            if (issue == null) return RedirectToAction("Index");

            return View(issue);
        }
    }
}