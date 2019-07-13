using System.ComponentModel.DataAnnotations;

namespace DB2019.Backend.Api.Models
{
    /// <summary>
    ///     Параметры новой заявки
    /// </summary>
    public class NewIssueData
    {
        /// <summary>
        ///     Категория заявки
        /// </summary>
        [Required]
        public string Category { get; set; }

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
        ///     Гео-позиция пользователя
        /// </summary>
        [Required]
        public GeoCoordinates Position { get; set; }
    }
}