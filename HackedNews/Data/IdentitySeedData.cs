using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser("ArtemAdmin_74@tut.by");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
