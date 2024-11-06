using System.ComponentModel.DataAnnotations;

namespace HostingProject.Models.RolesViewModel
{
    public class EditRoleView
    {
        public EditRoleView() {
            Users = new List<UserRole>();
        }

        [Required]
        public string RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        public List<UserRole> Users { get; set; }
        
    }
}
