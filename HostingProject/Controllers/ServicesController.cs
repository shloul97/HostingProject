using HostingProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HostingProject.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ProjDbContext db;
        public ServicesController(ProjDbContext _db, UserManager<IdentityUser> _userManager)
        { 
            db = _db;
            userManager = _userManager;
        }
        public async Task<IActionResult> ClientArea()
        {
            var userId = userManager.GetUserId(User);
            var serviceItem = await db.userServices
                .Where(c => c.userId == userId && c.planId == c.Plan.planId)
                .Include(c=> c.Plan)
                .Select(c => new {
                    c.userId,
                    c.sId,
                    c.IsActive,
                    c.CreatedDate,
                    c.DateUntil,
                    Plan = c.Plan
                } ).ToListAsync();

            ViewBag.Services = serviceItem;
            return View();
        }
    }
}
