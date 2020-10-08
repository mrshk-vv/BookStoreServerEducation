using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Store.DataAccess.Entities;
using Store.Shared.Enums;

namespace Store.DataAccess.Initialize
{
    public static class DefaultIdentityInitializer
    {
        private const string CLIENT_EMAIL = "client@gmail.com";
        private const string ADMIN_EMAIL = "admin@gmail.com";
        private const string PASSWORD = "qwerty123";
        public static async Task IdentityInitializerAsync(this IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            if (await roleManager.FindByNameAsync(Enums.Roles.Admin.ToString()) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            }

            if (await roleManager.FindByNameAsync(Enums.Roles.Client.ToString()) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Client.ToString()));
            }

            User client = new User
            {
                FirstName = "Default",
                LastName = "Client",
                Email = CLIENT_EMAIL,
                UserName = CLIENT_EMAIL,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(client, PASSWORD);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(client, Enums.Roles.Client.ToString());
            }

            User admin = new User
            {
                FirstName = "Default",
                LastName = "Admin",
                Email = ADMIN_EMAIL,
                UserName = ADMIN_EMAIL,
                EmailConfirmed = true
            };

            result = await userManager.CreateAsync(admin, PASSWORD);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, Enums.Roles.Admin.ToString());
            }
        }
    }
}
