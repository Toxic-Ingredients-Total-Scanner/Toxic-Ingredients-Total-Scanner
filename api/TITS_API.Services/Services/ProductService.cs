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
using System.Linq;
using TITS_API.Architecture;

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
        

        public ProductService(DatabaseContext context, PubChemService pubChemService , IngredientService ingredientService)
        {
            _productRepository = new ProductRepository(context);
            _ingredientRepository = new IngredientRepository(context);
            _productCompositionRepository = new ProductCompositionRepository(context);
            _pubChemService = pubChemService;
            _ingredientService = ingredientService;
        }


        public async Task<Product> GetFullRequestById(int id)
        {
            var product = await _productRepository.Get(id);

            if (product != null)
            {
                product.Ingredients = await GetIngredientList(product.Id);
            }

            return product;
        }


        public async Task<Product> GetFullRequestByEan(string ean)
        {
            var product = await _productRepository.GetByEan(ean);
            if (product == null)
            {
                product = await GetFromPWS(ean);
                if (product != null)
                {
                    product.Gtin = product.Gtin.Length == 14 && product.Gtin[0] == '0' ? product.Gtin.Substring(1, 13) : product.Gtin;
                    product = await _productRepository.Add(product);
                }
            }

            if (product != null)
            {
                product.Ingredients = await GetIngredientList(product.Id);
            }

            return product;
        }


        public async Task<Product> GetFullRequestByName(string name)
        {
            var product = await _productRepository.GetByName(name);

            if (product != null)
            {
                product.Ingredients = await GetIngredientList(product.Id);
            }

            return product;
        }


        public async Task<Product> Update(Product product)
        {
            List<Ingredient> ingredients = null;

            try
            {
                var relations = await _productCompositionRepository.GetRelations(product.Id);
                var ids = relations.Where(r => r.ProductId == product.Id).Select(r => r.Id).ToList();

                if (ids != null)
                {
                    foreach(var id in ids)
                    {
                        await _productCompositionRepository.Delete(id);
                    }
                }
            }
            catch (Exception)
            { }

            if (product.Ingredients != null)
            {
                ingredients = product.Ingredients;

                for(int i = 0; i < ingredients.Count; i++)
                {
                    var ing = await _ingredientRepository.GetByName(ingredients[i].PolishName);

                    if (ing != null)
                    {
                        ingredients[i] = ing;
                    }
                    else if (!String.IsNullOrEmpty(ingredients[i].PolishName))
                    {
                        var completed = await _pubChemService.AutoComplete(ingredients[i]);
                        ingredients[i] = await _ingredientRepository.Add(completed);
                        ingredients[i].HazardStatements = completed.HazardStatements;
                        await _ingredientService.AddRelationsToHazardStatements(ingredients[i].Id, ingredients[i].HazardStatements);
                    }           

                    await _productCompositionRepository.Add(new ProductComposition
                    {
                        ProductId = product.Id,
                        IngredientId = ingredients[i].Id
                    });
                }
            }

            product.ModifiedDate = DateTime.Now;
            var p = await _productRepository.Update(product);

            p.Ingredients = ingredients;

            return p;
        }


        public async Task<Product> Add(Product product)
        {
            List<Ingredient> ingredients = null;

            if (product.Ingredients != null)
            {
                ingredients = product.Ingredients;

                for (int i = 0; i < ingredients.Count; i++)
                {
                    var ing = await _ingredientRepository.GetByName(ingredients[i].PolishName);

                    if (ing != null)
                    {
                        ingredients[i] = ing;
                    }
                    else if (!String.IsNullOrEmpty(ingredients[i].PolishName))
                    {
                        var completed = await _pubChemService.AutoComplete(ingredients[i]);
                        ingredients[i] = await _ingredientRepository.Add(completed);
                        ingredients[i].HazardStatements = completed.HazardStatements;
                    }
                }           
            }

            product.ModifiedDate = DateTime.Now;
            Product p = await _productRepository.Add(product);

            if(ingredients != null)
            {
                for (int i = 0; i < ingredients.Count; i++)
                {
                    await _productCompositionRepository.Add(new ProductComposition
                    {
                        ProductId = p.Id,
                        IngredientId = ingredients[i].Id
                    });

                    var hs = await _ingredientService.GetHazardStatemensList(ingredients[i].Id);
                    if (hs == null)
                    {
                        await _ingredientService.AddRelationsToHazardStatements(ingredients[i].Id, ingredients[i].HazardStatements);
                    }
                }

            }

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
