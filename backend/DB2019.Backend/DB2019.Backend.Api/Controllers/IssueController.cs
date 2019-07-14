using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using DB2019.Backend.Api.Models;
using DB2019.Backend.Data;
using DB2019.Backend.Data.Entities;

using Newtonsoft.Json;

using NLog;

namespace DB2019.Backend.Api.Controllers
{
    /// <summary>
    ///     Заявки
    /// </summary>
    [System.Web.Http.Route("api/issue")]
    public class IssueController : ApiController
    {
        private static readonly Logger traceLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Добавить новую заявку
        /// </summary>
        /// <param name="sessionId">Сессия пользователя</param>
        /// <param name="data">Параметры новой заявки</param>
        public IHttpActionResult Post(
            string sessionId,
            [FromBody] NewIssueData data)
        {
            traceLogger.Debug(
                "Post new issue: sessionId = {0}, body = {1}",
                sessionId,
                JsonConvert.SerializeObject( data, Formatting.None ) );
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
                    photo = System.Convert.FromBase64String(data.Photo);
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

                return Ok();
            }
        }

        /// <summary>
        /// Получить список заявок
        /// </summary>
        /// <param name="categoryId">Идентификатор категории</param>
        /// <param name="framePosition">Номер первого элемента</param>
        /// <param name="frameSize">Количество запрашиваемых элементов</param>
        /// <returns></returns>
        public Frame<IssueData> Get(int framePosition, int frameSize, int? categoryId = null)
        {
            return InternalGetIssues(framePosition, frameSize, categoryId);
        }

        /// <summary>
        /// Получить заявку
        /// </summary>
        /// <param name="issueId">Идентификатор заявки</param>
        /// <returns></returns>
        public IssueData Get(int issueId)
        {
            var issue = GetById( issueId );
            if (issue == null) throw new HttpException((int)HttpStatusCode.NotFound, "Issue not found");
            return issue;
        }

        /// <summary>
        /// Проголосовать за заявку
        /// </summary>
        /// <param name="issueId">Идентификатор заявки</param>
        /// <param name="sessionId">Сессия пользователя</param>
        /// <returns></returns>
        [Route("api/issue/vote")]
        public IHttpActionResult Vote(string sessionId, int issueId)
        {
            using (var db = new Db2019DbContext())
            {
                if (Guid.TryParse(sessionId, out var session) == false)
                    return BadRequest("Invalid session id");
                var user = db.Users.FirstOrDefault(u => u.SessionId == session);
                if (user == null) return Unauthorized();
                var issue = db.Issues.Include(i => i.Tags).FirstOrDefault(i => i.Id == issueId);
                if (issue == null) throw new HttpException((int)HttpStatusCode.NotFound, "Issue not found");

                issue.Rating += 1;
                db.SaveChanges();
                return Ok();
            }
        }

        public Frame<IssueData> GetByPosition(
            int framePosition,
            int frameSize,
            double latitude,
            double longitude,
            double radios )
        {
            return null;
        }

        internal static IssueData GetById(int issueId)
        {
            using (var db = new Db2019DbContext())
            {
                var issue = db.Issues.Include(i => i.Tags).FirstOrDefault(i => i.Id == issueId);
                return Convert(issue);
            }
        }

        internal static Frame<IssueData> InternalGetIssues(int framePosition, int frameSize, int? categoryId)
        {
            using (var db = new Db2019DbContext())
            {
                var query = db.Issues.Include(i => i.Tags);
                if (categoryId.HasValue) query = query.Where(i => i.CategoryId == categoryId.Value);

                if (frameSize >= 0) query = query.Skip(framePosition).Take(frameSize);

                var issues = query.ToList();
                return new Frame<IssueData>
                {
                    Data = issues.Select(Convert).ToList(),
                    TotalCount = query.Count()
                };
            }
        }

        private static IssueData Convert(Issue issue)
        {
            if( issue == null ) return null;

            return new IssueData
            {
                Id = issue.Id,
                UserId = issue.UserId,
                CategoryId = issue.CategoryId,
                Position = new GeoCoordinates {Latitude = issue.Latitude, Longitude = issue.Longitude},
                Photo = issue.Photo == null || issue.Photo.Length == 0 ? "" : System.Convert.ToBase64String(issue.Photo),
                Comment = issue.Comment,
                Tags = issue.Tags.Count > 0 ? issue.Tags.Select(t => t.Id).ToList() : null,
                Status = issue.Status,
                Rating = issue.Rating,
                CreatedTime = issue.CreatedTime
            };
        }
    }
}