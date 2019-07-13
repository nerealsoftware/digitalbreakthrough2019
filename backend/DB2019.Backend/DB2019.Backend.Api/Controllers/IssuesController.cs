using System.Web.Http;
using DB2019.Backend.Api.Models;

namespace DB2019.Backend.Api.Controllers
{
    /// <summary>
    /// Заявки
    /// </summary>
    public class IssuesController : ApiController
    {
        /// <summary>
        ///     Добавить новую заявку
        /// </summary>
        /// <param name="sessionId">Сессия пользователя</param>
        /// <param name="data">Параметры новой заявки</param>
        public void Post(
            string sessionId,
            [FromBody] NewIssueData data)
        {
        }
    }
}