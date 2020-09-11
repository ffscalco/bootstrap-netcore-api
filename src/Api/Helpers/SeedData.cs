using System;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Api.Helpers
{
    public static class SeedData
    {
        public static void Seed(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByEmailAsync("admin@email.com").Result == null)
            {
                var user = new User
                {
                    FirstName = "Super",
                    LastName = "Admin",
                    Email = "admin@email.com",
                    UserName = "admin@email.com".Split("@")[0]
                };

                var result = userManager.CreateAsync(user, "Qwerty!23456").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserRoles.Admin).Wait();
                }

            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(UserRoles.Admin).Result)
            {
                roleManager.CreateAsync(new IdentityRole(UserRoles.Admin)).Wait();
            }

            if (!roleManager.RoleExistsAsync(UserRoles.Athlete).Result)
            {
                roleManager.CreateAsync(new IdentityRole(UserRoles.Athlete)).Wait();
            }
        }
    }
}
