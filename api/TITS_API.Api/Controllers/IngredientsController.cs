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

        /// <summary>
        /// Get ingredient with hazard statements by id or name. If both are set, id has higher priority.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns>Ingredient</returns>
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
            ingredient.HazardStatements = await _ingredientService.GetHazardStatemensList(ingredient.Id);
            return ingredient;
        }

        /// <summary>
        /// Get ingredients names matching param as string array.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>string[]</returns>
        [Route("names/{name}")]
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
        
        /// <summary>
        /// Get all ingredients names as string array.
        /// </summary>
        /// <returns>string[]</returns>
        [Route("names")]
        [HttpGet]
        public async Task<ActionResult<string[]>> GetIngredientNames()
        {
            var names = await _ingredientRepository.GetAllNames();
            if (names == null)
            {
                return NotFound();
            }
            return names;
        }

        /// <summary>
        /// Get all ingredients names as string array calling sql function.
        /// </summary>
        /// <returns>string[]</returns>
        [Route("namessql")]
        [HttpGet]
        public async Task<ActionResult<string[]>> GetIngredientNamesSql()
        {
            var names = await _ingredientRepository.GetAllNames();
            if (names == null)
            {
                return NotFound();
            }
            return names;
        }

        /// <summary>
        /// Add ingredient without hazard statements.
        /// </summary>
        /// <param name="ingredient"></param>
        /// <returns>Ingredient</returns>
        /// <response code="409">If ingredient with specified polish name already exists in database.</response>
        [HttpPost]
        public async Task<ActionResult<Ingredient>> Add(Ingredient ingredient)
        {
            if (_ingredientRepository.GetByName(ingredient.PolishName) != null)
            {
                return Conflict();
            }

            ingredient.PolishName = ingredient.PolishName.ToLower();

            var _ingredient = await _ingredientRepository.Add(ingredient);
            if (_ingredient == null)
            {
                return NotFound();
            }
            return _ingredient;
        }

        /// <summary>
        /// Update ingredient without hazard statements.
        /// </summary>
        /// <param name="ingredient"></param>
        /// <returns>Ingredient</returns>
        [HttpPut]
        public async Task<ActionResult<Ingredient>> Update(Ingredient ingredient)
        {
            ingredient.PolishName = ingredient.PolishName.ToLower();

            var _ingredient = await _ingredientRepository.Update(ingredient);
            if (_ingredient == null)
            {
                return NotFound();
            }
            return _ingredient;
        }

        /// <summary>
        /// Get info about specified ingredient from PubChem.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Ingredient</returns>
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

        /// <summary>
        /// Get info about specified ingredient from PubChem and insert it to database. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Ingredient</returns>
        [Route("autocomplete")]
        [HttpPost]
        public async Task<ActionResult<Ingredient>> AddAutocompletedIngredient(string name)
        {
            var ingredient = await _pubChemService.AutoComplete(new Ingredient { PolishName = name });
            if (ingredient == null)
            {
                return NotFound();
            }
            await _ingredientService.AddRelationsToHazardStatements(ingredient.Id, ingredient.HazardStatements);
            var ing = await _ingredientRepository.Add(ingredient);
            ing.HazardStatements = ingredient.HazardStatements;

            return ing;
        }

        /// <summary>
        /// Scan again specified ingredient in PubChem and update info in database.
        /// </summary>
        /// <param name="ingredient"></param>
        /// <returns>Ingredient</returns>
        [Route("autocomplete")]
        [HttpPut]
        public async Task<ActionResult<Ingredient>> UpdateAutocompletedIngredient(Ingredient ingredient)
        {
            var ing = await _pubChemService.AutoComplete(ingredient);
            if (ing == null)
            {
                return NotFound();
            }
            await _ingredientService.UpdateRelationsWithHazardStatements(ingredient);
            var i = await _ingredientRepository.Update(ing);
            i.HazardStatements = ingredient.HazardStatements;

            return i;
        }
    }
}