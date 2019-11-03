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
    public class ProductRepository : Repository<Product, DatabaseContext>
    {
        public ProductRepository(DatabaseContext context) : base(context)
        { }

        public Task<Product> GetByEan(string ean)
        {
            return Task.Run(() => _context.Products.FirstOrDefault(p => p.Gtin == ean));
        }

        public Task<Product> GetByName(string name)
        {
            return Task.Run(() => _context.Products.FirstOrDefault(p => p.ItemName == name));
        }
    }
}
