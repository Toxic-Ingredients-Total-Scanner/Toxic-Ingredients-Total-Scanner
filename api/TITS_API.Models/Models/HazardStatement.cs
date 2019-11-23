using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TITS_API.Models.Interfaces;

namespace TITS_API.Models.Models
{
    [Table("HazardStatements")]
    public class HazardStatement : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string DescriptionPolish { get; set; }
        public string DescriptionEnglish { get; set; }
    }
}
