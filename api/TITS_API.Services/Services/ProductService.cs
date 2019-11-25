using GoogleTranslateFreeApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TITS_API.Api.Configuration;
using TITS_API.Models.Models;
using TITS_API.Repositories.Repositories;

namespace TITS_API.Services.Services
{
    public class ProductService
    {
        private const string pws = "http://produktywsieci.gs1.pl/api/products/";

        private readonly ProductRepository _productRepository;
        private readonly IngredientRepository _ingredientRepository;
        private readonly ProductCompositionRepository _productCompositionRepository;
        private readonly PubChemService _pubChemService;
        private readonly IngredientService _ingredientService;
        

        public ProductService(ProductRepository productRepository, IngredientRepository ingredientRepository,
            ProductCompositionRepository productCompositionRepository, PubChemService pubChemService , IngredientService ingredientService)
        {
            _productRepository = productRepository;
            _ingredientRepository = ingredientRepository;
            _productCompositionRepository = productCompositionRepository;
            _pubChemService = pubChemService;
            _ingredientService = ingredientService;
        }

        public async Task<Product> Add(Product product)
        {
            if (product.Ingredients != null)
            {
                product.Ingredients.ForEach(async ingredient =>
                {
                    if (!await _ingredientRepository.Exists(ingredient) && !String.IsNullOrEmpty(ingredient.PolishName))
                    {
                        ingredient = await _ingredientRepository.Add(await _pubChemService.AutoComplete(ingredient));                        
                    }
                });
            }

            Product p = await _productRepository.Add(product);

            product.Ingredients.ForEach(async ingredient =>
            {
                await _productCompositionRepository.Add(new ProductComposition
                { 
                    ProductId = p.Id,
                    IngredientId = ingredient.Id
                });
            });

            return p;
        }

        public async Task<Product> GetFromPWS(string gtin)
        {
            try
            {
                Credentials credentials = SettingsReader.GetCredentials("PwSAPI");

                var request = (HttpWebRequest)WebRequest.Create(pws + gtin + "?aggregation=SOCIAL");
                request.Method = "GET";
                request.Headers.Add(HttpRequestHeader.Accept, "application/json");
                request.Headers.Add(HttpRequestHeader.Authorization,
                    "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes($"{credentials.Login}:{credentials.Key}")));

                var response = await request.GetResponseAsync();
                var stream = new StreamReader(response.GetResponseStream());

                var product = JsonConvert.DeserializeObject<Product>(stream.ReadToEnd());

                if (product.ProductName == null)
                {
                    return null;
                }
                else return product; 
            }
            catch(Exception)
            {
                return null;
            }
        }
        
        public async Task<List<Ingredient>> GetIngredientList(int productId)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            var relations = await _productCompositionRepository.GetRelations(productId);
            if (relations != null)
            {
                foreach (var relation in relations)
                {
                    ingredients.Add(await _ingredientRepository.Get(relation.IngredientId));
                }
                foreach (var ingredient in ingredients)
                {
                    ingredient.HazardStatements = await _ingredientService.GetHazardStatemensList(ingredient.Id);
                }
            }

            return ingredients.Count > 0 ? ingredients : null;
        }

        public async Task<Product> GetFullProductInfo(string gtin)
        {
            var product = await _productRepository.GetByEan(gtin);
            if(product == null)
            {
                product = await GetFromPWS(gtin);
                if (product != null)
                {
                    product.Gtin = product.Gtin.Length == 14 && product.Gtin[0] == '0' ? product.Gtin.Substring(1, 13) : product.Gtin; 
                    product = await _productRepository.Add(product);
                    product.Ingredients = await GetIngredientList(product.Id);
                }
            }

            return product;
        }

        public async Task<List<ProductComposition>> AddRelationsToIngrediends(int productId, List<Ingredient> ingredients)
        {
            List<ProductComposition> pc = new List<ProductComposition>();

            foreach (var  ingredient in ingredients)
            {
                pc.Add(await _productCompositionRepository.Add(new ProductComposition
                {
                    ProductId = productId,
                    IngredientId = ingredient.Id
                }));
            }

            return pc;
        }
    }
}
