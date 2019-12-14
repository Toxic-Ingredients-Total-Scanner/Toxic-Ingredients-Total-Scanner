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
    public class Product : TrackedEntity
    {
        public string Gtin { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string BrandOwner { get; set; }
        public string Manufacturer { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public string Base64Image { get; set; }
        public string Url { get; set; }
        public bool? IsLegal { get; set; }


        [NotMapped]
        public List<Ingredient> Ingredients { get; set; }


        public override string ToString()
        {
            return Gtin + ", " + ProductName + ", " + Brand + ", " + BrandOwner + ", " + 
                Manufacturer + ", " + CountryOfOrigin + ", " + Description + ", " + 
                ProductImage + ", " + Base64Image + ", " + Url + ", " + IsLegal;
        }
    }
}
