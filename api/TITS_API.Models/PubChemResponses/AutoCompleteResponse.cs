using System;
using System.Collections.Generic;
using System.Text;

namespace TITS_API.Models.PubChemResponses
{
    public class AutoCompleteResponse
    {
        public int Total { get; set; }
        public Dictionary_Terms Dictionary_Terms { get; set; }
    }
}
