using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TITS_API.Architecture;
using TITS_API.Models.Models;
using TITS_API.Repositories.Interfaces;

namespace TITS_API.Repositories.Repositories
{
    public class IngredientHazardStatementRepository : Repository<IngredientHazardStatement, DatabaseContext>
    {
        public IngredientHazardStatementRepository(DatabaseContext context) : base(context)
        { }

        public async Task<List<IngredientHazardStatement>> GetRelations(int ingredientId)
        {
            return await Task.Run(() => _context.IngredientHazardStatements.Where(ihs => ihs.IngredientId == ingredientId).ToList());
        }
    }
}
