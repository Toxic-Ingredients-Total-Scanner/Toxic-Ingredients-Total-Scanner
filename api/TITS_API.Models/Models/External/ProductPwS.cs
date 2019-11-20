using System;
using System.Collections.Generic;
using System.Text;

namespace TITS_API.Models.Models.External
{
    public class ProductPwS
    {
        public string Gtin { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string BrandOwner { get; set; }
        public string Manufacturer { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public string Url { get; set; }
        public bool IsLegal { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Product GetProduct()
        {
            return new Product
            {
                Gtin = Gtin,
                ItemName = ProductName,
                BrandName = Brand,
                BrandOwner = BrandOwner,
                ManufacturerName = Manufacturer,
                CountryOfOrigin = CountryOfOrigin,
                Description = Description,
                Picture = ProductImage,
                Url = Url
            };
        }
    }
}
