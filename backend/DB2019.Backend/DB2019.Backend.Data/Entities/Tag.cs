using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB2019.Backend.Data.Entities
{
    public class Tag
    {
        [Key] public int Id { get; set; }

        [Index("IX_CategoryId_Name", IsUnique = true, Order = 1)]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Index("IX_CategoryId_Name", IsUnique = true, Order = 2)]
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
    }
}