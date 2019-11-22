﻿using GoogleTranslateFreeApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TITS_API.Models.Models;
using TITS_API.Models.PubChemResponses;

namespace TITS_API.Services.Services
{
    public class PubChemService
    {
        private readonly TranslateService _translateService;
        private readonly HttpClient _http;

        private const string baseUrl = "https://pubchem.ncbi.nlm.nih.gov/rest/pug";
        private const string autoCompleteUrl = "https://pubchem.ncbi.nlm.nih.gov/rest/autocomplete/compound/";

        public PubChemService(TranslateService translateService)
        {
            _translateService = translateService;
            _http = new HttpClient();
        }

        public async Task<Ingredient> AutoComplete(Ingredient ingredient)
        {
            var properName = await FindProperEnglishName(ingredient.PolishName);

            if (properName == null) return ingredient;

            ingredient.EnglishName = properName;
//            ingredient.PubChemCID = Int32.Parse(await _http.GetStringAsync(baseUrl + properName[0] + "/name/" + properName[1] + "/cids/TXT"));
//            ingredient.PubChemUrl = baseUrl + properName[0] + properName[1] + "/json";

            return ingredient;
        }

        private async Task<string> FindProperEnglishName(string polishName)
        {
            var translationResult = _translateService.Translate(polishName, Language.Polish, Language.English);

            var pubChemAutoCompleteResponse = await _http.GetAsync(autoCompleteUrl + translationResult.MergedTranslation + "/json?limit=1").Result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<AutoCompleteResponse>(pubChemAutoCompleteResponse);
            if (response.Total > 0) return response.Dictionary_Terms.Compound[0];

    

            return null;
        }
    }
}
