using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebShop.Application.Commands;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.Domain.UnitOfWorks;
using SimpleWebShop.Infrastruture.EFCore;
using SimpleWebShop.Infrastruture.UnitOfWorks;

namespace SimpleWebShop
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add mvc to pipeline.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add in memory efcore to the project.
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("SimpleWebShop"));
            
            services.AddTransient<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();

            // Add MediatR.
            services.AddMediatR(typeof(Command));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();


                List<Color> colors = new List<Color>()
                {
                    new Color() { Name = "Blue", Hex = "#4286f4" }, new Color() { Name = "Red", Hex = "#ef3d21" }, new Color() { Name = "Green", Hex = "#2bef20" }
                };

                List<string> pictures = new List<string>()
                {
                    "product-1.jpg", "product-2.jpg", "product-3.jpg", "product-4.jpg"
                };

                var productFakter = new Faker<Product>()
                    .RuleFor(p => p.Color, f => f.PickRandom(colors))
                    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                    .RuleFor(p => p.Picture, f => f.PickRandom(pictures));
                
                var inventoryProductFaker = new Faker<InventoryProduct>()
                    .RuleFor(i => i.Amount, f => f.Random.Int(0, 500))
                    .RuleFor(i => i.Price, f => f.Random.Double(0, 10000))
                    .RuleFor(i => i.Product, f => productFakter.Generate());
                
                
                dbContext.Set<InventoryProduct>().AddRange(inventoryProductFaker.Generate(500));

                dbContext.SaveChanges();
            }

            if (env.IsDevelopment())
            {
                

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
