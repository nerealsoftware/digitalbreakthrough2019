using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DB2019.Backend.Api.Models;
using DB2019.Backend.Data;
using DB2019.Backend.Data.Entities;

namespace DB2019.Backend.Api.Controllers
{
    /// <summary>
    ///     Категории
    /// </summary>
    [Route("api/category")]
    public class CategoryController : ApiController
    {
        /// <summary>
        ///     Получить список всех категорий
        /// </summary>
        /// <returns></returns>
        public List<CategoryData> Get()
        {
            return InternalCategoryList();
        }

        public static List<CategoryData> InternalCategoryList()
        {
            using (var db = new Db2019DbContext())
            {
                var categories = db.Categories.OrderBy(c => c.Id).ToList();
                return categories.Select(Convert).ToList();
            }
        }

        private static CategoryData Convert(Category c)
        {
            return new CategoryData {Id = c.Id, Name = c.Name};
        }
    }
}