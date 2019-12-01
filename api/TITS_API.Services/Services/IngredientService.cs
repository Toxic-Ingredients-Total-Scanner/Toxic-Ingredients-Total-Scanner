using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TITS_API.Models.Models;
using TITS_API.Repositories.Repositories;
using System.Linq;

namespace TITS_API.Services.Services
{
    public class IngredientService
    {
        private readonly IngredientRepository _ingredientRepository;
        private readonly HazardStatementRepository _hsRepository;
        private readonly IngredientHazardStatementRepository _ihsRepository;

        public IngredientService(IngredientRepository ingredientRepository,
            HazardStatementRepository hazardStatementRepository,
            IngredientHazardStatementRepository ingredientHazardStatementRepository)
        {
            _ingredientRepository = ingredientRepository;
            _hsRepository = hazardStatementRepository;
            _ihsRepository = ingredientHazardStatementRepository;
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

            return hazardStatements;
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
    }
}
