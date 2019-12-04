using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TITS_API.Models.Models;
using TITS_API.Repositories.Repositories;
using System.Linq;
using TITS_API.Architecture;

namespace TITS_API.Services.Services
{
    public class IngredientService
    {
        private readonly IngredientRepository _ingredientRepository;
        private readonly HazardStatementRepository _hsRepository;
        private readonly IngredientHazardStatementRepository _ihsRepository;
        private readonly PubChemService _pubChemService;

        public IngredientService(DatabaseContext context, PubChemService pubChemService)
        {
            _ingredientRepository = new IngredientRepository(context);
            _hsRepository = new HazardStatementRepository(context);
            _ihsRepository = new IngredientHazardStatementRepository(context);
            _pubChemService = pubChemService;
        }


        public async Task<List<HazardStatement>> GetHazardStatemensList(int ingredientId)
        {
            List<HazardStatement> hazardStatements = new List<HazardStatement>();
            var relations = await _ihsRepository.GetRelations(ingredientId);
            if(relations != null)
            {
                foreach(var relation in relations)
                {
                    hazardStatements.Add(await _hsRepository.Get(relation.HazardStatementId));
                }
            }

            return hazardStatements.Count > 0 ? hazardStatements : null;
        }


        public async Task<List<IngredientHazardStatement>> AddRelationsToHazardStatements(int ingredientId, List<HazardStatement> hazardStatements)
        {
            List<IngredientHazardStatement> ihs = new List<IngredientHazardStatement>();

            foreach(var statement in hazardStatements)
            {
                ihs.Add(await _ihsRepository.Add(new IngredientHazardStatement
                {
                    IngredientId = ingredientId,
                    HazardStatementId = statement.Id
                }));
            }

            return ihs;
        }


        public async Task<Ingredient> GetOrAddIfNotExists(Ingredient ingredient)
        {
            if (ingredient.PolishName == null) return null;

            var ing = await _ingredientRepository.GetByName(ingredient.PolishName);

            if(ing != null)
            {
                ing.HazardStatements = await GetHazardStatemensList(ing.Id);
                return ing;
            }
            else
            {
                ing = await _pubChemService.AutoComplete(ingredient);

                if (ing == null) return null;

                var newIngredient = await _ingredientRepository.Add(ing);
                await AddRelationsToHazardStatements(newIngredient.Id, ing.HazardStatements);
                newIngredient.HazardStatements = ing.HazardStatements;

                return newIngredient;
            }
        }
    }
}
