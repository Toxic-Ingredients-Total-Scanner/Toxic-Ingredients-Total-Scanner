using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TITS_API.Models.Models;
using TITS_API.Services.Services;

namespace TITS_API.Api.Controllers
{
    [Route("api/Products/fullRequest")]
    [ApiController]
    public class FullRequestController : ControllerBase
    {
        private readonly ProductService _productService;

        public FullRequestController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get product with ingredients by id, ean(gtin) or name. Priority: id, ean, name.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ean"></param>
        /// <param name="name"></param>
        /// <returns>Product</returns>
        [HttpGet]
        public async Task<ActionResult<Product>> Get(int id, string ean, string name)
        {
            Product product = null;

            if (id != 0)
            {
                product = await _productService.GetFullRequestById(id);
            }
            else if (!String.IsNullOrEmpty(ean))
            {
                product ??= await _productService.GetFullRequestByEan(ean);
            }
            else if (!String.IsNullOrEmpty(name))
            {
                product ??= await _productService.GetFullRequestByName(name);
            }
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        /// <summary>
        /// Add new product and ingredients if they don't exists, and relations between them.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Product</returns>
        /// <response code="409">If product with specified ean(gtin) already exists in database.</response>
        [HttpPost]
        public async Task<ActionResult<Product>> Add(Product product)
        {
            var p = await _productService.Add(product);
            if (p == null)
            {
                return Conflict();
            }
            return p;
        }

        /// <summary>
        /// Update product and relations between product and ingredients.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Product</returns>
        [HttpPut]
        public async Task<ActionResult<Product>> Update(Product product)
        {
            var p = await _productService.Update(product);
            if (p == null)
            {
                return NotFound();
            }
            return p;
        }
    }
}