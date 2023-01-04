using System.Globalization;
using HackedNews.Data;
using HackedNews.Data.Interfaces;
using HackedNews.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HackedNews
{
    public class Startup
    {
        public Startup(IHostEnvironment hostEnv)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath)
                .AddJsonFile("dbsettings.json").Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HackedNews:DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders();
            //services.AddDbContext<AppIdentityDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("HackedNewsIdentity:DefaultConnection")));

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
            app.UseRequestLocalization(); //поддержка локализации
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute(
                        null,
                        "News{NewsPage:int}",
                        new { controller = "News", action = "InfNews" });
                    endpoints.MapControllerRoute(
                        null,
                        "{categoryId}/Page{NewsPage:int}",
                        new { controller = "News", action = "List" });
                    endpoints.MapControllerRoute(
                        null,
                        "Page{NewsPage:int}",
                        new { controller = "News", action = "List", NewsPage = 1 });
                    endpoints.MapControllerRoute(
                        null,
                        "{categoryId}",
                        new { controller = "News", action = "List", NewsPage = 1 });
                    endpoints.MapControllerRoute(
                        null,
                        "{controller=Home}/{action=Index}/{id?}"
                    );
                });


            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}