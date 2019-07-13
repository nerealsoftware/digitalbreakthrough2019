using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using DB2019.Backend.Api.Models;
using DB2019.Backend.Data;
using DB2019.Backend.Data.Entities;

namespace DB2019.Backend.Api.Controllers
{
    /// <summary>
    ///     Заявки
    /// </summary>
    [Route("api/issue")]
    public class IssueController : ApiController
    {
        /// <summary>
        ///     Добавить новую заявку
        /// </summary>
        /// <param name="sessionId">Сессия пользователя</param>
        /// <param name="data">Параметры новой заявки</param>
        public IHttpActionResult Post(
            string sessionId,
            [FromBody] NewIssueData data)
        {
            if (Guid.TryParse(sessionId, out var session) == false)
                return BadRequest("Invalid session id");
            if (data == null)
                return BadRequest("Invalid issue data");
            using (var db = new Db2019DbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.SessionId == session);
                if (user == null) return Unauthorized();
                var category = db.Categories.FirstOrDefault(c => c.Id == data.CategoryId);
                if (category == null) return BadRequest("Invalid category id");
                if (data.Position == null) return BadRequest("Invalid position");
                byte[] photo = null;
                try
                {
                    photo = Convert.FromBase64String(data.Photo);
                }
                catch
                {
                    return BadRequest("Invalid photo");
                }

                var tags = data.Tags?.Count > 0
                    ? db.Tags.Where(t => t.CategoryId == category.Id && data.Tags.Contains(t.Id)).ToList()
                    : null;

                var issue = new Issue
                {
                    User = user,
                    Category = category,
                    Latitude = data.Position.Latitude,
                    Longitude = data.Position.Longitude,
                    Photo = photo,
                    Comment = string.IsNullOrWhiteSpace(data.Comment) ? null : data.Comment.Trim()
                };
                if (tags != null)
                    foreach (var tag in tags)
                        issue.Tags.Add(tag);

                issue.CreatedTime = DateTime.Now;
                db.Issues.Add(issue);
                db.SaveChanges();

                return new OkResult(this);
            }
        }
    }
}