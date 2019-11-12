using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TITS_API.Models.Interfaces;

namespace TITS_API.Models.Models
{
    [Table("ProductCompositions")]
    public class ProductComposition : IEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("Ingredients")]
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
