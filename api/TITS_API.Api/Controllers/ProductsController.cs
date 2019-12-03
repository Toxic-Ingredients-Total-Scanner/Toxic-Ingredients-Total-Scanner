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