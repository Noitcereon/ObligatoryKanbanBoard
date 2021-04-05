using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using KanbanBoardMVCApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoardMVCApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();

            UserIndexVM vm = new UserIndexVM(_userManager, roles);
            return View(vm);
        }

        [HttpPost]
        public IActionResult AssignRole(IdentityUser user, string role)
        {
            if (ModelState.IsValid)
            {
                _userManager.AddToRoleAsync(user, role);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult RemoveRole(IdentityUser user, string role)
        {
            if (ModelState.IsValid)
            {
                _userManager.RemoveFromRoleAsync(user, role);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
