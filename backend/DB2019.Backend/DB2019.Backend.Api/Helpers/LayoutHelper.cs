using System.Collections.Generic;
using DB2019.Backend.Api.Controllers;
using DB2019.Backend.Api.Models;

namespace DB2019.Backend.Api.Helpers
{
    public class LayoutHelper
    {
        public static List<CategoryData> GetCategories()
        {
            return InMemoryCache.GetOrSet("categoryList", () =>
            {
                var categoryList = CategoryController.InternalCategoryList();
                categoryList.Sort((a, b) => a.Name.CompareTo(b.Name));
                return categoryList;
            }, 60);
        }
    }
}