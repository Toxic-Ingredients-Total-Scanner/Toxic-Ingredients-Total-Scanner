using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TITS_API.Models.Interfaces;

namespace TITS_API.Models.Models
{
    [Table("Ingredients")]
    public class Ingredient : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int PubChemCID { get; set; }
        public string PolishName { get; set; }
        public string EnglishName { get; set; }
        public string MolecularFormula { get; set; }
        public string ToxicityGrade { get; set; }
        public string PubChemUrl { get; set; }
        public string WikiUrl { get; set; }

    }
}
