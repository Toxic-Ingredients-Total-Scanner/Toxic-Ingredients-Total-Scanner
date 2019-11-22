using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TITS_API.Models.Models;

namespace TITS_API.Architecture
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductComposition> ProductCompositions { get; set; }
        public DbSet<HazardStatement> HazardStatements { get; set; }
        public DbSet<IngredientHazardStatement> IngredientHazardStatements { get; set; }

    }
}
