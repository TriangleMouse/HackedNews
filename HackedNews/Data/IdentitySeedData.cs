using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HackedNews.Data
{
    public static class IdentitySeedData
    {
        private const string adminUser = "ArtemAdmin_74@tut.by";
        private const string adminPassword = "Admin_123";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser("ArtemAdmin_74@tut.by");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}