using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TITS_API.Models.Interfaces;

namespace TITS_API.Models.Models
{
    [Table("IngredientHazardStatements")]
    public class IngredientHazardStatement : IEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Ingredients")]
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        [ForeignKey("HazardStatements")]
        public int HazardStatementId { get; set; }
        public virtual HazardStatement HazardStatement { get; set; }
    }
}
