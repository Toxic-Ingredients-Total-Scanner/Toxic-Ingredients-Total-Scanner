using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TITS_API.Models.Models;

namespace TITS_API.Services.Services
{
    public class PubChemService
    {
        private readonly TranslateService _translateService;

        public PubChemService(TranslateService translateService)
        {
            _translateService = translateService;
        }

        public async Task<Ingredient> AutoComplete(Ingredient ingredient)
        {
            //TODO


            return null;
        }
    }
}
