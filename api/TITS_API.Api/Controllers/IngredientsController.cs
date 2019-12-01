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
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientRepository _ingredientRepository;
        private readonly IngredientService _ingredientService;
        private readonly PubChemService _pubChemService;

        public IngredientsController(IngredientRepository ingredientRepository,
            PubChemService pubChemService,
            IngredientService ingredientService)
        {
            _ingredientRepository = ingredientRepository;
            _ingredientService = ingredientService;
            _pubChemService = pubChemService;
        }


        [HttpGet]
        public async Task<ActionResult<Ingredient>> Get(int id, string name)
        {
            Ingredient ingredient = null;

            if (id != 0)
            {
                ingredient = await _ingredientRepository.Get(id);
            }
            else if (!String.IsNullOrEmpty(name))
            {
                ingredient ??= await _ingredientRepository.GetByName(name);
            }

            if (ingredient == null)
            {
                return NotFound();
            }
            return ingredient;
        }


        [Route("names")]
        [HttpGet]
        public async Task<ActionResult<string[]>> GetIngredientNames(string name)
        {
            var names = await _ingredientRepository.GetIngredientNames(name);
            if (names == null)
            {
                return NotFound();
            }
            return names;
        }


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


        [Route("autocomplete")]
        [HttpGet]
        public async Task<ActionResult<Ingredient>> PubChemAutocomplete(string name)
        {
            var ingredient = await _pubChemService.AutoComplete(new Ingredient { PolishName = name});
            if (ingredient == null)
            {
                return NotFound();
            }
            return ingredient;
        }


        [Route("autocomplete")]
        [HttpPost]
        public async Task<ActionResult<Ingredient>> AddAutocompletedIngredient(string name)
        {
            var ingredient = await _pubChemService.AutoComplete(new Ingredient { PolishName = name });
            if (ingredient == null)
            {
                return NotFound();
            }
            ingredient = await _ingredientRepository.Add(ingredient);
            await _ingredientService.AddRelationsToHazardStatements(ingredient.Id, ingredient.HazardStatements);

            return ingredient;
        }
    }
}