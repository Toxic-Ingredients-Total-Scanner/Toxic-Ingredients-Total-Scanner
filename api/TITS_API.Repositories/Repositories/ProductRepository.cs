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

        public async Task<Product> GetByEan(string ean)
        {
            return await Task.Run(() => _context.Products.FirstOrDefault(p => p.Gtin == ean));
        }

        public async Task<Product> GetByName(string name)
        {
            return await Task.Run(() => _context.Products.FirstOrDefault(p => p.ProductName == name));
        }

        public async Task<string[]> GetProductNames(string name)
        {
            return await Task.Run(() => _context.Products.Where(p => p.ProductName.ToUpper().Contains(name.ToUpper())).Select(p => p.ProductName).ToArray());
        }

    }
}
