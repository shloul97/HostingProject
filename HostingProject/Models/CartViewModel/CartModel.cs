using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HostingProject.Models.CartViewModel
{
    public class CartModel : SharedService
    {
        
        [Key]
        public int cartId { get; set; }
        public int planId { get; set; }
        [ForeignKey("planId")]
        public PlanViewModel plan { get; set; }
        public string userId { get; set; }

    }
}
