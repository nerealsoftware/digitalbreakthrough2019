using System;
using System.Linq;
using System.Web.Http;
using DB2019.Backend.Data;
using DB2019.Backend.Data.Entities;

namespace DB2019.Backend.Api.Controllers
{
    /// <summary>
    ///     Авторизация пользователей
    /// </summary>
    [Route("api/auth")]
    public class AuthController : ApiController
    {
        /// <summary>
        ///     Получить сессию пользователя по идентификатору устройства
        /// </summary>
        /// <param name="deviceId">Идентификатор устройства</param>
        /// <returns>Сессия пользователя</returns>
        public Guid Get(string deviceId)
        {
            using (var db = new Db2019DbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.DeviceId == deviceId);
                if (user == null)
                {
                    user = new User
                    {
                        DeviceId = deviceId
                    };
                    db.Users.Add(user);
                }

                if (user.SessionId == null) user.SessionId = Guid.NewGuid();

                user.LastLoginTime = DateTime.Now;

                db.SaveChanges();

                return user.SessionId.GetValueOrDefault();
            }
        }
    }
}