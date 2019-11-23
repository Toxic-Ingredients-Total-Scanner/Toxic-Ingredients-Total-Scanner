using System;
using System.Collections.Generic;
using System.Text;
using TITS_API.Architecture;
using TITS_API.Models.Models;
using TITS_API.Repositories.Interfaces;

namespace TITS_API.Repositories.Repositories
{
    public class IngredientHazardStatementRepository : Repository<IngredientHazardStatement, DatabaseContext>
    {
        public IngredientHazardStatementRepository(DatabaseContext context) : base(context)
        { }
    }
}
