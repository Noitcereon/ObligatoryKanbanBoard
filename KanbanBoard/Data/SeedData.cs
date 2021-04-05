using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KanbanBoardMVCApp.Data
{
    public class SeedData
    {
        /// <summary>
        /// Fills the database with the default roles and an admin user (if no roles and if no admin exists)
        /// </summary>
        public static void Initialise(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait(TimeSpan.FromSeconds(10));
                roleManager.CreateAsync(new IdentityRole("Team")).Wait(TimeSpan.FromSeconds(5));
                roleManager.CreateAsync(new IdentityRole("Contributor")).Wait(TimeSpan.FromSeconds(5));
                roleManager.CreateAsync(new IdentityRole("Observer")).Wait(TimeSpan.FromSeconds(5));
            }

            if (userManager.GetUsersInRoleAsync("Admin").Result.Count <= 0)
            {
                string adminUsername = "admin@admin.com";
                var admin = userManager.Users.First(x => x.UserName == adminUsername);
                if (admin == null)
                {
                    IdentityUser newAdmin = new IdentityUser(adminUsername);
                    newAdmin.Email = adminUsername;
                    IdentityResult result = userManager.CreateAsync(newAdmin, "GU9O`cVista=0E$e").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(userManager.Users.FirstOrDefault(x => x.UserName == adminUsername), "Admin");
                    }
                }
                else
                {
                    userManager.AddToRoleAsync(userManager.Users.FirstOrDefault(x => x.UserName == adminUsername), "Admin");
                }
            }
        }
    }
}
