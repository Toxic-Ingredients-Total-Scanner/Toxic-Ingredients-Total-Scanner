using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TITS_API.Models.Models;
using System.Security.Cryptography;
using System.Text;

namespace TITS_API.Architecture
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            ChangeTracker.Tracked += (sender, e) =>
            {
                if (!e.FromQuery && e.Entry.Entity is TrackedEntity entity)
                {
                    if(e.Entry.State == EntityState.Added)
                    {
                        var now = DateTimeOffset.Now;
                        entity.CreatedAt = now;
                        entity.UpdatedAt = now;
                        entity.UpdatedBy ??= "default";

                        MD5 md5Hash = MD5.Create();
                        entity.Checksum = GetMd5Hash(md5Hash, entity.ToString());
                    }
                    else if(e.Entry.State == EntityState.Modified)
                    {
                        entity.UpdatedAt = DateTimeOffset.Now;
                        entity.UpdatedBy ??= "default";

                        MD5 md5Hash = MD5.Create();
                        entity.Checksum = GetMd5Hash(md5Hash, entity.ToString());
                    }
                }
            };
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductComposition> ProductCompositions { get; set; }
        public DbSet<HazardStatement> HazardStatements { get; set; }
        public DbSet<IngredientHazardStatement> IngredientHazardStatements { get; set; }


        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
