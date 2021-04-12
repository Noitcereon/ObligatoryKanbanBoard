using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KanbanBoardMVCApp.ViewModels
{
    public class UserIndexVM
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserIndexVM(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            Users = _userManager.Users.ToList();
        }

        public List<IdentityUser> Users { get; }

        public async Task<IList<string>> UserRoles(IdentityUser user)
        {
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            return userRoles;
        }


    }
}
