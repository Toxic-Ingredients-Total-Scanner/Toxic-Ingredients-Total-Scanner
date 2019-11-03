using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TITS_API.Models.Models;
using TITS_API.Repositories.Architecture;
using TITS_API.Repositories.Repositories;

namespace TITS_API.Api.Controllers
{
    
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductsController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [Route(ApiRoutes.ProductsGetProductById)]
        [HttpGet]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productRepository.Get(id);
            if(product == null)
            {
                return NotFound();
            }
            return product;
        }


        [Route(ApiRoutes.ProductsGetProductByEan)]
        [HttpGet]
        public async Task<ActionResult<Product>> GetByEan(string ean)
        {
            var product = await _productRepository.GetByEan(ean);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }


        [Route(ApiRoutes.ProductsGetProductByName)]
        [HttpGet]
        public async Task<ActionResult<Product>> GetByName(string name)
        {
            var product = await _productRepository.GetByName(name);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }


        [Route(ApiRoutes.ProductsAddProduct)]
        [HttpPost]
        public async Task<ActionResult<Product>> Add(Product product)
        {
            var _product = await _productRepository.Add(product);
            if (_product == null)
            {
                return NotFound();
            }
            return _product;
        }


        [Route(ApiRoutes.ProductsUpdateProduct)]
        [HttpPut]
        public async Task<ActionResult<Product>> Update(Product product)
        {
            var _product = await _productRepository.Update(product);
            if (_product == null)
            {
                return NotFound();
            }
            return _product;
        }
    }
}