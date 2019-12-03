﻿using System;
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

        public IngredientService(DatabaseContext context)
        {
            _ingredientRepository = new IngredientRepository(context);
            _hsRepository = new HazardStatementRepository(context);
            _ihsRepository = new IngredientHazardStatementRepository(context);
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
    }
}
