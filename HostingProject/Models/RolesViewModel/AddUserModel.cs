using System.ComponentModel.DataAnnotations;

namespace HostingProject.Models.RolesViewModel
{
    public class AddUserModel
    {
        [Required]
        public string roleId { get; set; }
        [Required]
        public string UserEmail { get; set; }
        
    }
}
