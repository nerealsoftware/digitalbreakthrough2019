using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB2019.Backend.Data.Entities
{
    public class Issue
    {
        public Issue()
        {
            Tags = new HashSet<Tag>();
        }

        [Key] public int Id { get; set; }

        [Index] public int UserId { get; set; }

        public virtual User User { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public byte[] Photo { get; set; }
        public string Comment { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}