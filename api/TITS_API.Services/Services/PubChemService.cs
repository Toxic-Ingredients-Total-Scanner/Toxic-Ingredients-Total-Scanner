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
using TITS_API.Architecture;
using TITS_API.Models.Models;
using TITS_API.Models.PubChemResponses;
using TITS_API.Repositories.Repositories;

namespace TITS_API.Services.Services
{
    public class PubChemService
    {
        private readonly TranslateService _translateService;
        private readonly HazardStatementRepository _hazardStatementRepository;
        private readonly HttpClient _http;

        private const string apiUrl = "https://pubchem.ncbi.nlm.nih.gov/rest/pug/";
        private const string baseUrl = "https://pubchem.ncbi.nlm.nih.gov/compound/";
        private const string autoCompleteUrl = "https://pubchem.ncbi.nlm.nih.gov/rest/autocomplete/compound/";
        private const string informationUrl = "https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/data/compound/";

        public PubChemService(TranslateService translateService, DatabaseContext context)
        {
            _translateService = translateService;
            _hazardStatementRepository = new HazardStatementRepository(context);
            _http = new HttpClient();
        }

        public async Task<Ingredient> AutoComplete(Ingredient ingredient)
        {
            try
            {
                var properName = await FindProperEnglishName(ingredient);
                if (properName == null) return null;
                ingredient.EnglishName = properName;

                string cids = await _http.GetStringAsync(apiUrl + "compound/name/" + ingredient.EnglishName + "/cids/TXT");

                ingredient.PubChemCID = Int32.Parse(cids.Split()[0]);
                ingredient.PubChemUrl = baseUrl + ingredient.PubChemCID;
                ingredient.MolecularFormula = (await _http.GetStringAsync(apiUrl + "compound/cid/" + ingredient.PubChemCID + "/property/MolecularFormula/TXT")).Trim();
                ingredient.StructureImageUrl = apiUrl + "compound/cid/" + ingredient.PubChemCID + "/PNG";
                ingredient.WikiUrl = await WikipediaURL(ingredient);

                var codes = await GHSStatements(ingredient);
                ingredient.HazardStatements = await GetStatementsByCode(codes);
                if (!codes.Contains("X404"))
                {
                    ingredient.GHSClasificationRaportUrl = ingredient.PubChemUrl + "#datasheet=LCSS&section=GHS-Classification&fullscreen=true";
                }

                return ingredient;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<string> FindProperEnglishName(Ingredient ingredient)
        {
            string pubChemAutoCompleteResponse;

            if (ingredient.EnglishName != null)
            {
                try
                {
                    var synonyms = await _http.GetStringAsync(apiUrl + "compound/name/" + ingredient.EnglishName + "/synonyms/TXT");
                    var synonym = synonyms.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                    return synonym;
                }
                catch {}

                pubChemAutoCompleteResponse = await _http.GetAsync(autoCompleteUrl + ingredient.EnglishName + "/json?limit=1").Result.Content.ReadAsStringAsync();

            }
            else
            {
                var translationResult = _translateService.Translate(ingredient.PolishName, Language.Polish, Language.English);                
                try
                {
                    var synonyms = await _http.GetStringAsync(apiUrl + "compound/name/" + translationResult.MergedTranslation + "/synonyms/TXT");
                    var synonym = synonyms.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                    return synonym;
                }
                catch { }
                pubChemAutoCompleteResponse = await _http.GetAsync(autoCompleteUrl + translationResult.MergedTranslation + "/json?limit=1").Result.Content.ReadAsStringAsync();
            }

            var response = JsonConvert.DeserializeObject<AutoCompleteResponse>(pubChemAutoCompleteResponse);
            if (response.Total > 0) return response.Dictionary_Terms.Compound[0];

            return null;
        }

        private async Task<string> WikipediaURL(Ingredient ingredient)
        {
            string wikiUrl = "";

            try
            {
                var wikiResponse = await _http.GetStringAsync(informationUrl + ingredient.PubChemCID + "/XML?heading=Wikipedia");

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
            catch (Exception)
            {
                return null;
            }

            return String.IsNullOrEmpty(wikiUrl) ? null : wikiUrl;
        }

        private async Task<List<string>> GHSStatements(Ingredient ingredient)
        {
            List<string> codes = new List<string>();

            Regex regex = new Regex(@"(H\d{3}([a-z]|[A-Z]){0,2}( |\:))|(EUH\d{3}( |\:))|(AUH\d{3})( |\:)");

            try
            {
                var GHSResponse = await _http.GetStringAsync(informationUrl + ingredient.PubChemCID + "/XML?heading=GHS%20Classification");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(GHSResponse);

                XmlNodeList nodes = doc.GetElementsByTagName("String");
                foreach (XmlNode node in nodes)
                {
                    Match match = regex.Match(node.InnerXml);
                    if (match.Success)
                    {
                        String[] splitTable = node.InnerXml.Split(new char[] { ' ', ':' });
                        if (!codes.Contains(splitTable[0]))
                        {
                            codes.Add(splitTable[0]);
                        }
                    }
                }
                
                return codes.Count > 0 ? codes : new string[] { "X404" }.ToList();
            }
            catch (Exception)
            {
                return new string[] { "X404" }.ToList() ;
            }
        }

        private async Task<List<HazardStatement>> GetStatementsByCode(List<string> codes)
        {
            try
            {
                List<HazardStatement> hazardStatements = new List<HazardStatement>();

                foreach (var code in codes)
                {
                    hazardStatements.Add(await _hazardStatementRepository.GetByCode(code));
                }

                return hazardStatements;
            }
            catch(Exception)
            { 
                return null; 
            }
        }

    }
}
