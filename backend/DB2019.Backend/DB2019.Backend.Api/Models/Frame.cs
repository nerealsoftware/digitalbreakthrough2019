using System.Collections.Generic;

namespace DB2019.Backend.Api.Models
{
    /// <summary>
    /// Страница данных
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public class Frame<T>
    {
        /// <summary>
        /// Данные
        /// </summary>
        public List<T> Data { get; set; }
        /// <summary>
        /// Общее количество
        /// </summary>
        public int TotalCount { get; set; }
    }
}