using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB2019.Backend.Data.Entities
{
    public class User
    {
        [Key] public int Id { get; set; }

        [MaxLength(256)] [Index] public string DeviceId { get; set; }

        [Index] public Guid? SessionId { get; set; }

        public DateTime? LastLoginTime { get; set; }
    }
}