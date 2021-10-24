using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using HackedNews.Data;
using HackedNews.Data.Repository;
using HackedNews.Data.Models.NewsModel;
using HackedNews.Data.Interfaces;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace HackedNews
{
    public class Startup
    {
        public Startup(IHostEnvironment hostEnv) => this.Configuration = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HackedNews:DefaultConnection")));

            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HackedNewsIdentity:DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();


            services.AddTransient<IAllNews, NewsRepository>();
            services.AddTransient<INewsCategory, CategoryRepository>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews().AddViewLocalization().AddDataAnnotationsLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("ru"),
                    new CultureInfo("en"),
                    new CultureInfo("be")
                };

                options.DefaultRequestCulture = new RequestCulture("ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        
            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            { 
                app.UseDeveloperExceptionPage().UseStatusCodePages();
               
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization();//поддержка локализации
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints => {
                    endpoints.MapControllerRoute(
                           name: null,
                           pattern: "News{NewsPage:int}",
                           defaults: new { controller = "News", action = "InfNews" });
                    endpoints.MapControllerRoute(
                           name: null,
                           pattern: "{categoryId}/Page{NewsPage:int}",
                           defaults: new { controller = "News", action = "List" });
                    endpoints.MapControllerRoute(
                           name: null,
                           pattern: "Page{NewsPage:int}",
                           defaults: new { controller = "News", action = "List", NewsPage = 1 });
                    endpoints.MapControllerRoute(
                           name: null,
                           pattern: "{categoryId}",
                           defaults: new { controller = "News", action = "List", NewsPage = 1 });
                    endpoints.MapControllerRoute(
                           name: null,
                           pattern: "{controller=Home}/{action=Index}/{id?}"
                           );
                });

            
            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
