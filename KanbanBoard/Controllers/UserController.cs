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
        public IActionResult Index()
        {
            UserIndexVM vm = new UserIndexVM(_userManager);
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
                bool success;
                if (model.User == null)
                {
                    IdentityUser user = await _userManager.FindByIdAsync(model.UserId);
                    success = await ReplaceRole(user, model.SelectedRole);
                }
                else
                {
                    success = await ReplaceRole(model.User, model.SelectedRole);
                }

                if (success == false)
                {
                    ViewBag.ErrorMessage = "There must always be at least one admin.";
                    // TODO: replace ViewBag.ErrorMessage with UserIndexVM errormessages.

                    UserIndexVM vm = new UserIndexVM(_userManager);
                    return View(nameof(Index), vm);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ReplaceRole(IdentityUser user, string selectedRole)
        {
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Any())
            {
                bool isAdmin = userRoles.Contains("Admin");
                if (isAdmin && _roleManager.Roles.Count(role => role.Name == "Admin") <= 1)
                {
                    _logger.LogWarning("Someone attemped to replace the role on the last admin.");
                    return false;
                }

                await _userManager.RemoveFromRolesAsync(user, userRoles);
            }
            await _userManager.AddToRoleAsync(user, selectedRole);
            _logger.LogInformation($"{user.UserName} was assigned the {selectedRole} role.");
            return true;
        }

        // Delete request (adding HttpDelete gives 405 error)
        public async Task<IActionResult> RemoveRoles(string userId)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByIdAsync(userId);
                var roles = await _userManager.GetRolesAsync(user);
                bool isAdmin = roles.Contains("Admin");
                if (isAdmin && _roleManager.Roles.Count(role => role.Name == "Admin") <= 1)
                {
                    _logger.LogWarning("Someone attemped to remove all roles from the last admin.");

                    // TODO: replace ViewBag.ErrorMessage with UserIndexVM errormessages.
                    ViewBag.ErrorMessage = "There must always be at least one admin.";

                    UserIndexVM vm = new UserIndexVM(_userManager);
                    return View(nameof(Index), vm);
                }
                await _userManager.RemoveFromRolesAsync(user, roles);
                _logger.LogInformation($"{user.UserName} no longer has any roles.");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
