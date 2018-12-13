using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (env.IsDevelopment())
            {
                using (var serviceScope =
                    app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                    dbContext.Set<InventoryProduct>().Add(new InventoryProduct()
                    {
                        Amount = 20,
                        Price = 20,
                        Product = new Product()
                        {
                            Name = "Test",
                            Color = new Color()
                            {
                                Name = "Name"
                            }
                        }
                    });

                    dbContext.SaveChanges();
                }

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
