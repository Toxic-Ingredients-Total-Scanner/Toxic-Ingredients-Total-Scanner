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
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientRepository _ingredientRepository;

        public IngredientsController(IngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        [Route(ApiRoutes.IngredientsGetIngredientById)]
        [HttpGet]
        public async Task<ActionResult<Ingredient>> GetById(int id)
        {
            var ingredient = await _ingredientRepository.Get(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return ingredient;
        }


        [Route(ApiRoutes.IngredientsGetIngredientByName)]
        [HttpGet]
        public async Task<ActionResult<Ingredient>> GetByName(string name)
        {
            var ingredient = await _ingredientRepository.GetByName(name);
            if (ingredient == null)
            {
                return NotFound();
            }
            return ingredient;
        }


        [Route(ApiRoutes.IngredientsAddIngredient)]
        [HttpPost]
        public async Task<ActionResult<Ingredient>> Add(Ingredient ingredient)
        {
            var _ingredient = await _ingredientRepository.Add(ingredient);
            if (_ingredient == null)
            {
                return NotFound();
            }
            return _ingredient;
        }


        [Route(ApiRoutes.IngredientsUpdateIngredient)]
        [HttpPut]
        public async Task<ActionResult<Ingredient>> Update(Ingredient ingredient)
        {
            var _ingredient = await _ingredientRepository.Update(ingredient);
            if (_ingredient == null)
            {
                return NotFound();
            }
            return _ingredient;
        }
    }
}