using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TITS_API.Models.Models
{
    [NotMapped]
    public class ProductHint
    {
        public string Gtin { get; set; }
        public string ProductName { get; set; }
    }
}
