using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TITS_API.Models.Models;
using TITS_API.Repositories.Repositories;
using TITS_API.Services.Services;

namespace TITS_API.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductsController(ProductRepository productRepository, ProductService productService)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Get product without ingredients by id, ean(gtin) or name. Priority: id, ean, name.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ean"></param>
        /// <param name="name"></param>
        /// <returns>Product</returns>
        [HttpGet]
        public async Task<ActionResult<Product>> Get(int id, string ean, string name)
        {
            Product product = null;

            if(id != 0)
            {
                product = await _productRepository.Get(id);
            }
            else if(!String.IsNullOrEmpty(ean))
            {
                product ??= await _productRepository.GetByEan(ean);
            }
            else if (!String.IsNullOrEmpty(name))
            {
                product ??= await _productRepository.GetByName(name);
            }

            if(product == null)
            {
                return NotFound();
            }
            return product;
        }

        /// <summary>
        /// Get products names as string array.
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns>Product</returns>
        [Route("names")]
        [HttpGet]
        public async Task<ActionResult<string[]>> GetProductNames(string phrase)
        {
            var names = await _productRepository.GetProductNames(phrase);
            if (names == null)
            {
                return NotFound();
            }
            return names;
        }

        /// <summary>
        /// Add product without ingredients.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Product</returns>
        [HttpPost]
        public async Task<ActionResult<Product>> Add(Product product)
        {
            product.ModifiedDate = DateTime.Now;
            var _product = await _productRepository.Add(product);
            if (_product == null)
            {
                return NotFound();
            }
            return _product;
        }

        /// <summary>
        /// Update product without ingredients
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Product</returns>
        [HttpPut]
        public async Task<ActionResult<Product>> Update(Product product)
        {
            product.ModifiedDate = DateTime.Now;
            var _product = await _productRepository.Update(product);
            if (_product == null)
            {
                return NotFound();
            }
            return _product;
        }
    }
}