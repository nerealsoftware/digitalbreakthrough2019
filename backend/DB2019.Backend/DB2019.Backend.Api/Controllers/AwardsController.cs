using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB2019.Backend.Api.Controllers
{
    public class AwardsController : Controller
    {
        // GET: Awards
        public ActionResult Index()
        {
            return View();
        }
    }
}