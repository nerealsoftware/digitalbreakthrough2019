using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DB2019.Backend.Api.Models;
using DB2019.Backend.Data;
using DB2019.Backend.Data.Entities;

namespace DB2019.Backend.Api.Controllers
{
    /// <summary>
    ///     Метки
    /// </summary>
    [Route("api/tag")]
    public class TagController : ApiController
    {
        /// <summary>
        ///     Получить список всех меток
        /// </summary>
        /// <returns></returns>
        public List<TagData> Get()
        {
            using (var db = new Db2019DbContext())
            {
                var tags = db.Tags.OrderBy(t => t.CategoryId).ThenBy(t => t.Id).ToList();
                return tags.Select(Convert).ToList();
            }
        }

        /// <summary>
        ///     Получить список меток по категории
        /// </summary>
        /// <param name="categiryId">Идентификатор категории</param>
        /// <returns></returns>
        public List<TagData> Get(int categiryId)
        {
            using (var db = new Db2019DbContext())
            {
                var tags = db.Tags.Where(t => t.CategoryId == categiryId).OrderBy(t => t.Id).ToList();
                return tags.Select(Convert).ToList();
            }
        }

        private static TagData Convert(Tag t)
        {
            return new TagData {Id = t.Id, CategoryId = t.CategoryId, Name = t.Name};
        }
    }
}