using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using DB2019.Backend.Api.Models;
using DB2019.Backend.Data;
using DB2019.Backend.Data.Entities;

namespace DB2019.Backend.Api.Controllers
{
    public class MapController : Controller
    {
        public static Point centerPoint = new Point(58.604, 49.640);

        public ActionResult Map()
        {
            return View();
        }

        [HttpPost]
        public string GetData(string filter)
        {
            List<Appeal> appeals = new List<Appeal>();

            using (var db = new Db2019DbContext())
            {
                var issues = db.Issues.ToList();
                for (var index = 1; index <= issues.Count; index++)
                {
                    var item = issues[index - 1];
                    appeals.Add(new Appeal()
                    {
                        Coordinate = new Point(item.Latitude, item.Longitude),
                        Hint = item.Category.Name,
                        Description = string.Format("<p><a href=\"{0}\">№ {1} От: {2}</a><br>{3}<br>{4}<br></p>", Url.Action("ById","ShowIssue",new { id = item.Id }),item.Id,
                            item.CreatedTime.ToString("yyyy.MM.dd"), item.Category.Name, item.Comment),
                        StateCode = index % 3 == 0 ? "PROC" : index % 3 == 1 ? "NEW" : "PROCESS"
                    });
                }
            }

            var result = appeals;
            if ((!string.IsNullOrEmpty(filter)) && (filter != "ALL"))
            {
                result = result.Where(x => x.StateCode == filter).ToList();
            }

            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public string GetData2(string filter)
        {
            List<Appeal> appeals = new List<Appeal>();
            for (var i = 0; i < 10; i++)
            {
                appeals.Add(new Appeal()
                {
                    Coordinate = new Point(centerPoint.Latitude + GetRandomValue(), centerPoint.Lontitude + GetRandomValue()),
                    Hint = string.Format("Моя точка №{0} Подсказка", i),
                    Description = string.Format("<p><a href=\"\">Моя точка №{0} Описание</a></p>", i),
                    StateCode = i % 3 == 0 ? "PROC" : i % 3 == 1 ? "NEW" : "PROCESS"
                });
            }
            var result = appeals;
            if ((!string.IsNullOrEmpty(filter)) && (filter != "ALL"))
            {
                result = result.Where(x => x.StateCode == filter).ToList();
            }

            return JsonConvert.SerializeObject(result);
        }

        public static Random random = new Random();
        public static double GetRandomValue()
        {
            return (random.NextDouble() - 0.5) / 10;
        }
    }

    public class Appeal
    {
        public Point Coordinate { get; set; }
        public string Hint { get; set; }
        public string Description { get; set; }
        public string StateCode { get; set; }
    }

    public class Point
    {
        public double Latitude { get; set; }
        public double Lontitude { get; set; }

        public Point(double lat, double lon)
        {
            Latitude = lat;
            Lontitude = lon;
        }
    }
}