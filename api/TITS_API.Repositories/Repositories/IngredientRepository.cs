using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TITS_API.Architecture;
using TITS_API.Models.Models;
using TITS_API.Repositories.Interfaces;
using System.Linq;

namespace TITS_API.Repositories.Repositories
{
    public class IngredientRepository : Repository<Ingredient, DatabaseContext>
    {
        public IngredientRepository(DatabaseContext context) : base(context)
        { }

        public async Task<Ingredient> GetByName(string name)
        {
            return await Task.Run(() => _context.Ingredients.FirstOrDefault(p => p.PolishName == name));
        }

        public async Task<string[]> GetIngredientNames(string name)
        {
            return await Task.Run(() => _context.Ingredients.Where(p => p.PolishName.ToUpper().Contains(name.ToUpper())).Select(p => p.PolishName).ToArray());
        }
    }
}
