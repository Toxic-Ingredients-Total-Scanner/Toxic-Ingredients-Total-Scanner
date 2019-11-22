using GoogleTranslateFreeApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TITS_API.Models.Models;
using TITS_API.Models.PubChemResponses;

namespace TITS_API.Services.Services
{
    public class PubChemService
    {
        private readonly TranslateService _translateService;
        private readonly HttpClient _http;

        private const string apiUrl = "https://pubchem.ncbi.nlm.nih.gov/rest/pug/";
        private const string baseUrl = "https://pubchem.ncbi.nlm.nih.gov/compound/";
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
            ingredient.PubChemCID = Int32.Parse(await _http.GetStringAsync(apiUrl + "compound/name/" + ingredient.EnglishName +"/cids/TXT"));
            ingredient.PubChemUrl = baseUrl + ingredient.PubChemCID;
            ingredient.MolecularFormula = (await _http.GetStringAsync(apiUrl + "compound/cid/" + ingredient.PubChemCID + "/property/MolecularFormula/TXT")).Trim();
            ingredient.StructureImageUrl = apiUrl + "compound/cid/" + ingredient.PubChemCID + "/PNG";
            ingredient.GHSClasificationRaportUrl = ingredient.PubChemUrl + "#datasheet=LCSS&section=GHS-Classification&fullscreen=true";

            var wikiResponse = await _http.GetStringAsync("https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/data/compound/" + ingredient.PubChemCID + "/XML");

            if (!wikiResponse.Contains("PUGVIEW.NotFound"))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(wikiResponse);

                XmlNodeList elemList = doc.GetElementsByTagName("URL");
                foreach (XmlNode elem in elemList)
                {
                    if (elem.InnerText.Contains("https://en.wikipedia.org/wiki/"))
                    {
                        ingredient.WikiUrl = elem.InnerXml;
                        break;
                    }
                }
            }

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
