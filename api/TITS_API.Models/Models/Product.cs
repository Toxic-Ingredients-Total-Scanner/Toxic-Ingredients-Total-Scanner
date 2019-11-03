using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TITS_API.Models.Interfaces;

namespace TITS_API.Models.Models
{
    [Table("Products")]
    public class Product : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Gtin { get; set; }
        public string ItemName { get; set; }
        public string BrandName { get; set; }
        public string BrandOwner { get; set; }
        public string ManufacturerName { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string Url { get; set; }
    }
}
