using HostingProject.Models;
using HostingProject.Models.CartViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Runtime.Intrinsics.Arm;


namespace HostingProject.Controllers
{
    public class CartController : Controller
    {
        private readonly ProjDbContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly string connString;

        public CartController(ProjDbContext _db, UserManager<IdentityUser> _userManager) 
        {
            db = _db;
            userManager = _userManager;
        }
        public async Task<IActionResult> Index()
        {
            var model = new UserInformation();
            var userId = userManager.GetUserId(User);
            if (!string.IsNullOrEmpty(userId))
            {
                
                var cartItems = await db.Carts
                    .Where(c => c.userId == userId && c.IsDeleted == false && c.plan.planId == c.planId)
                    .Include(c => c.plan) 
                    .Select(c => new
                    {
                        userId = userId,
                        cartId = c.cartId,
                        planId = c.plan.planId,
                        planName = c.plan.planName,
                        planType = c.plan.planType,
                        serverType = c.plan.serverType,
                        storage = c.plan.storage,
                        RAM = c.plan.RAM,
                        database = c.plan.database,
                        bw = c.plan.bandwidth,
                        cpu = c.plan.CPU,
                        prot = c.plan.protection,
                        ssl = c.plan.ssl,
                        upgrade = c.plan.upgrade,
                        price = c.plan.price
                    })
                    .ToListAsync();

                ViewBag.Items = cartItems;
            }
            else
            {
                ViewBag.Items = null;
                
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult AddToCart(int planId) 
        {
            var userId = userManager.GetUserId(User);
            if (planId == null || userId == null || string.IsNullOrEmpty(userId)) 
            {
                return RedirectToAction("Index", "Account");
            }

            var cart = new CartModel
            {
                planId = planId,
                userId = userId
            };
            
            db.Carts.Add(cart);
            db.SaveChanges();
            return RedirectToAction("Hosting", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddUserInfo(UserInformation model)
        {
            var userId = userManager.GetUserId(User);
            if (!ModelState.IsValid)
            {
                var data = new UserInformation
                {
                    userId = userId,
                    phone = model.phone,
                    FullName = model.FullName,
                    country = model.country,
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    city = model.city
                };
                db.info.Add(data);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved your Data" });
            }

            return Json(new { success = false, message = "An Error occur" });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromCart(int cartId) 
        {
            var resultCart = await db.Carts.FindAsync(cartId);
            if (resultCart != null) 
            {
                resultCart.IsDeleted = true;
                db.Carts.Update(resultCart);
                db.SaveChanges();
                return RedirectToAction("Index", "Cart");
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(List<int> cartIds)
        {
            if (cartIds != null || cartIds.Count > 0) 
            {
                for (int i = 0; i < cartIds.Count; i++)
                {
                    var resultCart = await db.Carts.FindAsync(cartIds[i]);
                    if (resultCart != null)
                    {
                        resultCart.IsDeleted = true;
                        db.Carts.Update(resultCart);
                        var services = new ServiceViewModel
                        {
                            userId = resultCart.userId,
                            planId = resultCart.planId,
                            IsActive = true,
                            CreatedDate = DateTime.Now,
                            DateUntil = DateTime.Now.AddMonths(1)
                        };
                        db.userServices.Add(services);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "An Error occur");
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            ModelState.AddModelError(string.Empty, "No items selected for checkout.");
            return RedirectToAction("Index", "Cart");
        }


    }
}
