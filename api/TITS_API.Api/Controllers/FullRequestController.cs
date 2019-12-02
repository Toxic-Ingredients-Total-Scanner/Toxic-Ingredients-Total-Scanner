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


        [HttpPost]
        public async Task<ActionResult<Product>> Add(Product product)
        {
            var p = await _productService.Add(product);
            if (p == null)
            {
                return NotFound();
            }
            return p;
        }


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