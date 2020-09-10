using Microsoft.AspNetCore.Identity;
using Praktyki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praktyki.Extensions
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@email.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin@email.com",
                    Email = "admin@email.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "secret").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
