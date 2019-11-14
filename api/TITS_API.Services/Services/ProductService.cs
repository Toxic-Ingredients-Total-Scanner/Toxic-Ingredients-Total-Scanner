using GoogleTranslateFreeApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TITS_API.Models.Models;
using TITS_API.Repositories.Repositories;

namespace TITS_API.Services.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly IngredientRepository _ingredientRepository;
        private readonly ProductCompositionRepository _productCompositionRepository;

        public ProductService(ProductRepository productRepository, IngredientRepository ingredientRepository,
            ProductCompositionRepository productCompositionRepository)
        {
            _productRepository = productRepository;
            _ingredientRepository = ingredientRepository;
            _productCompositionRepository = productCompositionRepository;
        }

        public async Task<Product> Add(Product product)
        {
            if (product.Ingredients != null)
            {
                product.Ingredients.ForEach(async ingredient =>
                {
                    if (!await _ingredientRepository.Exists(ingredient) && !String.IsNullOrEmpty(ingredient.PolishName))
                    {
                        ingredient = await _ingredientRepository.Add(await PubChemService.AutoComplete(ingredient));                        
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
    }
}
