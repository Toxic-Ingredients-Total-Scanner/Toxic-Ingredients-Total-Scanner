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

        public async Task<List<ProductHint>> GetProductNames(string name)
        {
            const int take = 10;
            var words = name.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            switch (words.Count)
            {
                case 0: 
                    return null;
                case 1:
                    return await Task.Run(() => _context.Products.Where(p => 
                        p.ProductName.ToUpper().Contains(words[0].ToUpper()))
                        .Select(p => new ProductHint{ ProductName = p.ProductName, Gtin = p.Gtin }).Take(take).ToList());
                case 2:
                    return await Task.Run(() => _context.Products.Where(p =>
                        p.ProductName.ToUpper().Contains(words[0].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[1].ToUpper()))
                        .Select(p => new ProductHint { ProductName = p.ProductName, Gtin = p.Gtin }).Take(take).ToList());
                case 3:
                    return await Task.Run(() => _context.Products.Where(p =>
                        p.ProductName.ToUpper().Contains(words[0].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[1].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[2].ToUpper()))
                        .Select(p => new ProductHint { ProductName = p.ProductName, Gtin = p.Gtin }).Take(take).ToList());
                case 4:
                    return await Task.Run(() => _context.Products.Where(p =>
                        p.ProductName.ToUpper().Contains(words[0].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[1].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[2].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[3].ToUpper()))
                        .Select(p => new ProductHint { ProductName = p.ProductName, Gtin = p.Gtin }).Take(take).ToList());
                case 5:
                    return await Task.Run(() => _context.Products.Where(p =>
                        p.ProductName.ToUpper().Contains(words[0].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[1].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[2].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[3].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[4].ToUpper()))
                        .Select(p => new ProductHint { ProductName = p.ProductName, Gtin = p.Gtin }).Take(take).ToList());
                default:
                    return await Task.Run(() => _context.Products.Where(p =>
                        p.ProductName.ToUpper().Contains(words[0].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[1].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[2].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[3].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[4].ToUpper()) &&
                        p.ProductName.ToUpper().Contains(words[5].ToUpper()))
                        .Select(p => new ProductHint { ProductName = p.ProductName, Gtin = p.Gtin }).Take(take).ToList());
            }
        }

    }
}
