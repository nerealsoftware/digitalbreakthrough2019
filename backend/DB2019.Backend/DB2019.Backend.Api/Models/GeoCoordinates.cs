using System.ComponentModel.DataAnnotations;

namespace DB2019.Backend.Api.Models
{
    /// <summary>
    ///     Гео-координаты
    /// </summary>
    public class GeoCoordinates
    {
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
    }
}