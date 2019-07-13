namespace DB2019.Backend.Api.Models
{
    /// <summary>
    ///     Метка
    /// </summary>
    public class TagData
    {
        /// <summary>
        ///     Идентификатор метки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Идентификатор категории
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        ///     Название метки
        /// </summary>
        public string Name { get; set; }
    }
}