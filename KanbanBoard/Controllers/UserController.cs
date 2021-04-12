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
using Microsoft.Extensions.Logging;

namespace KanbanBoardMVCApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager,
        ILogger<UserController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
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
        public async Task<IActionResult> AssignRole(UserAssignRoleVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.User == default || model.User == null)
                {
                    IdentityUser user = await _userManager.FindByIdAsync(model.UserId);
                    await _userManager.AddToRoleAsync(user, model.SelectedRole);
                }
                else
                {
                    await _userManager.AddToRoleAsync(model.User, model.SelectedRole);
                }

                _logger.LogInformation("User was assigned a role");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRoles(string userId)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByIdAsync(userId);
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                _logger.LogInformation($"{user.UserName} no longer has any roles.");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
