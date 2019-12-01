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
        public async Task<ActionResult<Product>> Get(string gtin)
        {
            var product = await _productService.GetFullProductInfo(gtin);
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
    }
}