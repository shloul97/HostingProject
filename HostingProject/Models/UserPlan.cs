using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostingProject.Models
{
    public class UserPlan : SharedService
    {
        [Key]
        public int subId { get; set; }
        [ForeignKey("PlanViewModel")]
        public int planId { get; set; }

        [ForeignKey("UserViewModel")]
        public int userId { get; set; }
        public bool IsSubscribe { get; set; }
        public DateTime endDate { get; set; }
    }
}
