using System;
using System.Collections.Generic;

using DB2019.Backend.Data.Entities;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DB2019.Backend.Api.Models
{
    /// <summary>
    ///     Данные заявки
    /// </summary>
    public class IssueData
    {
        /// <summary>
        ///     Идентификатор заявки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Идентификатор категории заявки
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        ///     Гео-позиция пользователя
        /// </summary>
        public GeoCoordinates Position { get; set; }

        /// <summary>
        ///     Фотография к заявке, в base64 формате
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        ///     Необязательный комментарий к заявке
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        ///     Список идентификаторов меток
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<int> Tags { get; set; }

        /// <summary>
        ///     Статус заявки
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public IssueStatus Status { get; set; }
        /// <summary>
        ///     Рейтинг
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        ///     Время создания заявки
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}