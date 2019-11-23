using GoogleTranslateFreeApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
        private const string informationUrl = "https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/data/compound/";

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
            ingredient.WikiUrl = await WikipediaURL(ingredient);
             

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

        private async Task<string> WikipediaURL(Ingredient ingredient)
        {
            string wikiUrl = "";

            var wikiResponse = await _http.GetStringAsync(informationUrl + ingredient.PubChemCID + "/XML");

            if (!wikiResponse.Contains("PUGVIEW.NotFound"))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(wikiResponse);

                XmlNodeList nodes = doc.GetElementsByTagName("URL");
                foreach (XmlNode node in nodes)
                {
                    if (node.InnerText.Contains("https://en.wikipedia.org/wiki/"))
                    {
                        wikiUrl = node.InnerXml;
                        break;
                    }
                }
            }

            return wikiUrl;
        }

        private async Task<string[]> GHSStatements(Ingredient ingredient)
        {
            string[] GHSStatemments;
            string temp = "";
            var GHSResponse = await _http.GetStringAsync(informationUrl + ingredient.PubChemCID + "/XML");
            Regex regex = new Regex(@"(H\d{3}([a-z]|[A-Z]){0,2}( |\:))|(EUH\d{3}( |\:))|(AUH\d{3})( |\:)");

            if (!GHSResponse.Contains("PUGVIEW.NotFound"))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(GHSResponse);

                XmlNodeList nodes = doc.GetElementsByTagName("String");
                foreach (XmlNode node in nodes)
                {
                    Match match = regex.Match(node.InnerXml);
                    if (match.Success)
                    {
                        String[] splitTable = node.InnerXml.Split(new char[] {' ', ':'});
                        if (!temp.Contains(splitTable[0]))
                        {
                            temp += splitTable[0] + ' ';
                        }
                    }
                }
            }

            temp = temp.Trim();
            GHSStatemments = temp.Split(' ');

            return GHSStatemments;
        }

    }
}
