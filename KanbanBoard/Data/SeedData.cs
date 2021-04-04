using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                roleManager.CreateAsync(new IdentityRole("Admin"));
                roleManager.CreateAsync(new IdentityRole("Team"));
                roleManager.CreateAsync(new IdentityRole("Contributor"));
                roleManager.CreateAsync(new IdentityRole("Observer"));
            }

            if (userManager.GetUsersInRoleAsync("Admin").Result.Count <= 0)
            {
                userManager.CreateAsync(new IdentityUser("admin"));
                userManager.AddToRoleAsync(userManager.Users.FirstOrDefault(x => x.UserName == "admin"), "Admin");
            }
        }
    }
}
