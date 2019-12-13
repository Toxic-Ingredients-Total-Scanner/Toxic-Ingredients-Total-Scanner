using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TITS_API.Models.Interfaces;

namespace TITS_API.Models.Models
{
    public class TrackedEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string Checksum { get; set; }
    }
}
