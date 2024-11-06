using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostingProject.Models.CartViewModel
{
    public class UserInformation
    {
        [Key]
        public int infoId { get; set; }
        public string userId { get; set; }

        [ForeignKey("userId")]
        public IdentityUser User { get; set; }

        [Required(ErrorMessage = "Enter Your full Name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Enter Your Country")]
        public string country { get; set; }

        [Required(ErrorMessage = "Enter Your Address")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }
        [Required(ErrorMessage = "Enter Your City")]
        public string city { get; set; }
        [Required(ErrorMessage = "Enter Your Mobile Number")]
        public string phone { get; set; }
    }
}
