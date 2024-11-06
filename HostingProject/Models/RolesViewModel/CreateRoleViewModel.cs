using System.ComponentModel.DataAnnotations;

namespace HostingProject.Models.RolesViewModel
{
    public class CreateRoleViewModel
    {

        [Required(ErrorMessage = "Insert role Name")]
        [Display(Name = "Role Name")]
        public string roleName { get; set; }
    }
}
