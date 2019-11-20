using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleTranslateFreeApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TITS_API.Architecture;
using TITS_API.Repositories.Repositories;
using TITS_API.Services.Services;

namespace TITS_API.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<DatabaseContext>
                (options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                npgsql => 
                { 
                    npgsql.MigrationsAssembly("TITS_API.Repositories");
                    npgsql.SetPostgresVersion(new Version(9, 5));
                }));
            
            services.AddTransient<ProductRepository>();
            services.AddTransient<IngredientRepository>();
            services.AddTransient<ProductCompositionRepository>();
            services.AddTransient<ProductService>();
            services.AddTransient<TranslateService>();
            services.AddTransient<PubChemService>();
            services.AddTransient<GoogleTranslator>();

            services.AddControllers();

            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Toxic Ingredients Total Scanner - API"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Toxic Ingredients Total Scanner - API");
            });
        }
    }
}
