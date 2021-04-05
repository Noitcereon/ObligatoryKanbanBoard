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

        public UserIndexVM(UserManager<IdentityUser> userManager, IEnumerable<IdentityRole> roles)
        {
            _userManager = userManager;
            Users = _userManager.Users.ToList();
            Roles = new List<IdentityRole>(roles);
        }

        public List<IdentityUser> Users { get; }
        public List<IdentityRole> Roles { get; set; } // neccessary?

        public async Task<IList<string>> UserRoles(IdentityUser user)
        {

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            return userRoles;
        }


    }
}
