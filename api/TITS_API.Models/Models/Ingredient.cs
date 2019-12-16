using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TITS_API.Models.Interfaces;

namespace TITS_API.Models.Models
{
    [Table("Ingredients")]
    public class Ingredient : TrackedEntity
    {
        public int? PubChemCID { get; set; }
        public string PolishName { get; set; }
        public string EnglishName { get; set; }
        public string MolecularFormula { get; set; }
        public string StructureImageUrl { get; set; }
        public string GHSClasificationRaportUrl { get; set; }
        public string PubChemUrl { get; set; }
        public string WikiUrl { get; set; }


        [NotMapped]
        public List<HazardStatement> HazardStatements { get; set; }

        public override string ToString()
        {
            return PubChemCID + ", " + PolishName + ", " + EnglishName + ", " + 
                MolecularFormula + ", " + StructureImageUrl + ", " +
                GHSClasificationRaportUrl + ", " + PubChemUrl + ", " + WikiUrl;
        }
    }
}
