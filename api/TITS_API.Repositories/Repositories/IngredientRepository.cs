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
            const int take = 10;
            var words = name.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            switch (words.Count)
            {
                case 0:
                    return null;
                case 1:
                    return await Task.Run(() => _context.Ingredients.Where(i =>
                        i.PolishName.ToUpper().Contains(words[0].ToUpper()))
                        .Select(i => i.PolishName).Take(take).ToArray());
                case 2:
                    return await Task.Run(() => _context.Ingredients.Where(i =>
                        i.PolishName.ToUpper().Contains(words[0].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[1].ToUpper()))
                        .Select(i => i.PolishName).Take(take).ToArray());
                case 3:
                    return await Task.Run(() => _context.Ingredients.Where(i =>
                        i.PolishName.ToUpper().Contains(words[0].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[1].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[2].ToUpper()))
                        .Select(i => i.PolishName).Take(take).ToArray());
                case 4:
                    return await Task.Run(() => _context.Ingredients.Where(i =>
                        i.PolishName.ToUpper().Contains(words[0].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[1].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[2].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[3].ToUpper()))
                        .Select(i => i.PolishName).Take(take).ToArray());
                case 5:
                    return await Task.Run(() => _context.Ingredients.Where(i =>
                        i.PolishName.ToUpper().Contains(words[0].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[1].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[2].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[3].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[4].ToUpper()))
                        .Select(i => i.PolishName).Take(take).ToArray());
                default:
                    return await Task.Run(() => _context.Ingredients.Where(i =>
                        i.PolishName.ToUpper().Contains(words[0].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[1].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[2].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[3].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[4].ToUpper()) &&
                        i.PolishName.ToUpper().Contains(words[5].ToUpper()))
                        .Select(i => i.PolishName).Take(take).ToArray());
            }
        }

        public async Task<string[]> GetAllNames()
        {
            return await Task.Run(() => _context.Ingredients.Select(i => i.PolishName).ToArray());
        }
    }
}
