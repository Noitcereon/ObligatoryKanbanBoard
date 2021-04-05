using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
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

        [HttpGet]
        public IActionResult AssignRole(string userId)
        {
            IdentityUser user = _userManager.Users.First(x => x.Id == userId);
            UserAssignRoleVM vm = new UserAssignRoleVM(user, _roleManager.Roles);
            return View(vm);
        }

        [HttpPost]
        public IActionResult AssignRole(UserAssignRoleVM model)
        {
            // BUG: the user is always null when submitting. 
            if (ModelState.IsValid)
            {
                _userManager.AddToRoleAsync(model.User, model.SelectedRole);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRoles(IdentityUser user)
        {
            if (ModelState.IsValid)
            {
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
