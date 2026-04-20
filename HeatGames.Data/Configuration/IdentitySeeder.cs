using HeatGames.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace HeatGames.Data.Configuration
{
    public class IdentitySeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                }
            }

            var adminEmail = "admin@heatgames.com";
            var adminNickname = "admin";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdmin = new User
                {
                    UserName = adminNickname,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    WalletBalance = 1000.00m,
                    RegistrationDate = DateTime.UtcNow
                };

                var createPowerUser = await userManager.CreateAsync(newAdmin, "Admin123!");
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }
    }
}