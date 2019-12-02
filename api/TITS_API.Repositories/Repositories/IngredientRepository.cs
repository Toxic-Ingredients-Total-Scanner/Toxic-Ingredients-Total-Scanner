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
            var words = name.Split().ToList();
            return await Task.Run(() => _context.Ingredients.AsEnumerable().Where(i => words.All(w => i.PolishName.ToUpper().Contains(w.ToUpper()))).Select(i => i.PolishName).ToArray());
        }
    }
}
