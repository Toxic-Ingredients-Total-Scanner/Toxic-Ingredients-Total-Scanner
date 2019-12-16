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
    public class ProductCompositionRepository : Repository<ProductComposition, DatabaseContext>
    {
        public ProductCompositionRepository(DatabaseContext context) : base(context)
        { }

        public async Task<List<ProductComposition>> GetRelations(int productId)
        {
            return await Task.Run(() => _context.ProductCompositions.Where(pc => pc.ProductId == productId).ToList());
        }
    }
}
