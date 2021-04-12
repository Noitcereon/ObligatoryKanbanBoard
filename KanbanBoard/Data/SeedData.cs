using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoardMVCApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KanbanBoardMVCApp.Data
{
    public class SeedData
    {
        /// <summary>
        /// Fills the database with the default roles and an admin user (if no roles and if no admin exists), in addition to THE kanban board.
        /// </summary>
        public static void Initialise(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait(TimeSpan.FromSeconds(10));
                roleManager.CreateAsync(new IdentityRole("Team")).Wait(TimeSpan.FromSeconds(5));
                roleManager.CreateAsync(new IdentityRole("Contributor")).Wait(TimeSpan.FromSeconds(5));
                roleManager.CreateAsync(new IdentityRole("Observer")).Wait(TimeSpan.FromSeconds(5));
            }

            if (!context.KanbanBoards.Any())
            {
                var kanbanBoard = new KanbanBoard();
                kanbanBoard.Id = 1; // is probably overriden, when inserted into the database. (db is identity)
                kanbanBoard.ProjectName = "Kanban Board";
                context.KanbanBoards.Add(kanbanBoard);

                var kanbanColumn = new KanbanColumn("To Do", kanbanBoard.Id);
                var kanbanColumn2 = new KanbanColumn("Doing", kanbanBoard.Id);
                var kanbanColumn3 = new KanbanColumn("Testing", kanbanBoard.Id);
                var kanbanColumn4 = new KanbanColumn("Done", kanbanBoard.Id);
                List<KanbanColumn> columns = new List<KanbanColumn>{kanbanColumn, kanbanColumn2, kanbanColumn3, kanbanColumn4};
                foreach (var column in columns)
                {
                    context.KanbanColumns.Add(column);
                }
            }

            if (userManager.GetUsersInRoleAsync("Admin").Result.Count <= 0)
            {
                string adminUsername = "admin@admin.com";
                var admin = userManager.Users.FirstOrDefault(x => x.UserName == adminUsername);
                if (admin == default)
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
