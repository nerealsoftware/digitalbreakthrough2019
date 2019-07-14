using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DB2019.Backend.Api.Models
{
    /// <summary>
    ///     Параметры новой заявки
    /// </summary>
    public class NewIssueData
    {
        /// <summary>
        ///     Идентификатор категории заявки
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        ///     Широта
        /// </summary>
        [Required]
        public double Latitude { get; set; }

        /// <summary>
        ///     Долгота
        /// </summary>
        [Required]
        public double Longitude { get; set; }

        /// <summary>
        ///     Фотография к заявке, в base64 формате
        /// </summary>
        [Required]
        public string Photo { get; set; }

        /// <summary>
        ///     Необязательный комментарий к заявке
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        ///     Список идентификаторов меток
        /// </summary>
        public List<int> Tags { get; set; }
    }
}