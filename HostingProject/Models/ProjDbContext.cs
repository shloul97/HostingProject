using HostingProject.Models.AccountViewModel;
using HostingProject.Models.CartViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace HostingProject.Models
{
    public class ProjDbContext: IdentityDbContext
    {
        public ProjDbContext(DbContextOptions<ProjDbContext> options): base(options) 
        {
        
        }
        public DbSet<RegisterModel> Users { get; set; }
        public DbSet<PlanViewModel> Plans { get; set; }
        public DbSet<CartModel> Carts { get; set; }
        public DbSet<UserInformation> info { get; set; }
        public DbSet<ServiceViewModel> userServices { get; set; }


    }
}
