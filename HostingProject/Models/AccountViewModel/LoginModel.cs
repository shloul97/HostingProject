using System.ComponentModel.DataAnnotations;

namespace HostingProject.Models.AccountViewModel
{
    public class LoginModel : SharedService
    {

        [Required(ErrorMessage = "Insert E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Insert Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
