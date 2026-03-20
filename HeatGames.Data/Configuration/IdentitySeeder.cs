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
            // 1. Взимаме нужните сървиси, като изрично указваме <Guid> и твоя <User> модел
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // 2. Създаваме ролите
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                }
            }

            // 3. Създаваме първия Администратор (ако не съществува)
            var adminEmail = "admin@heatgames.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdmin = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    WalletBalance = 1000.00m, // Даваме му малко пари в портфейла за тестове
                    RegistrationDate = DateTime.UtcNow
                };

                // Създаваме потребителя с парола
                var createPowerUser = await userManager.CreateAsync(newAdmin, "Admin123!");
                if (createPowerUser.Succeeded)
                {
                    // Чак сега го добавяме към ролята
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }
    }
}