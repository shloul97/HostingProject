using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HostingProject.Models.AccountViewModel
{
    public class RegisterModel : SharedService
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Insert First Name")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Insert Last Name")]
        public string LName { get; set; }
        [Required(ErrorMessage = "Insert Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Insert Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Doesn\'t Match")]
        public string PasswordConfirmed { get; set; }

    }
}
