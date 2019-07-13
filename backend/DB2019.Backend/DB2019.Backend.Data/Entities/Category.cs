using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB2019.Backend.Data.Entities
{
    public class Category
    {
        [Key] public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
    }
}