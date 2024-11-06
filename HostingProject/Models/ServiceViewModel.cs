using Microsoft.AspNetCore.Identity;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostingProject.Models
{
    public class ServiceViewModel
    {
        [Key]
        public int sId { get; set; }
        [Required]
        public string userId { get; set; }
        [ForeignKey("userId")]
        public IdentityUser User { get; set; }
        [Required]
        public int planId { get; set; }
        [ForeignKey("planId")]
        public PlanViewModel Plan { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime DateUntil { get; set; }

    }
}
