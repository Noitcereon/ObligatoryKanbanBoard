using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KanbanBoardMVCApp.ViewModels
{
    public class UserAssignRoleVM
    {
        public UserAssignRoleVM() { }

        public UserAssignRoleVM(IdentityUser user, IEnumerable<IdentityRole> roles)
        {
            User = user;
            Roles = new List<SelectListItem>();
            foreach (var role in roles)
            {
                Roles.Add(new SelectListItem(role.Name, role.Name));
            }

        }

        [Required]
        public IdentityUser User { get; set; }
        public List<SelectListItem> Roles { get; set; }

        [Required]
        public string SelectedRole { get; set; }
    }
}
