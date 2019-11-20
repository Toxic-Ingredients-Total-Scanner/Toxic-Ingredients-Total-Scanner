using GoogleTranslateFreeApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TITS_API.Models.Models;

namespace TITS_API.Services.Services
{
    public class PubChemService
    {
        private readonly TranslateService _translateService;
        private readonly HttpClient _http;

        private const string baseUrl = "https://pubchem.ncbi.nlm.nih.gov/rest/pug";

        public PubChemService(TranslateService translateService)
        {
            _translateService = translateService;
            _http = new HttpClient();
        }

        public async Task<Ingredient> AutoComplete(Ingredient ingredient)
        {
            var properName = await FindProperEnglishName(ingredient.PolishName);

            if (properName == null) return ingredient;

            ingredient.EnglishName = properName[1];
            ingredient.PubChemID = Int32.Parse(await _http.GetStringAsync(baseUrl + properName[0] + "/name/" + properName[1] + "/cids/TXT"));
            ingredient.PubChemUrl = baseUrl + properName[0] + properName[1] + "/json";

            return ingredient;
        }

        private async Task<string[]> FindProperEnglishName(string polishName)
        {
            var translationResult = _translateService.Translate(polishName, Language.Polish, Language.English);

            var pubChemResponse = await _http.GetAsync(baseUrl + "/compound/name/" + translationResult.MergedTranslation + "/json").Result.Content.ReadAsStringAsync();
            if (!pubChemResponse.Contains("PUGREST.NotFound")) return new string[] { "/compound", translationResult.MergedTranslation };

            pubChemResponse = await _http.GetAsync(baseUrl + "/substance/name/" + translationResult.MergedTranslation + "/json").Result.Content.ReadAsStringAsync();
            if (!pubChemResponse.Contains("PUGREST.NotFound")) return new string[] { "/substance", translationResult.MergedTranslation };

            foreach (var text in translationResult.Synonyms.Noun)
            {
                pubChemResponse = await _http.GetAsync(baseUrl + "/compound/name/" + translationResult.MergedTranslation + "/json").Result.Content.ReadAsStringAsync();
                if (!pubChemResponse.Contains("PUGREST.NotFound")) return new string[] { "/compound", translationResult.MergedTranslation };

                pubChemResponse = await _http.GetAsync(baseUrl + "/substance/name/" + translationResult.MergedTranslation + "/json").Result.Content.ReadAsStringAsync();
                if (!pubChemResponse.Contains("PUGREST.NotFound")) return new string[] { "/substance", translationResult.MergedTranslation };
            }

            return null;
        }
    }
}
