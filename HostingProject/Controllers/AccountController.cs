using HostingProject.Models;
using HostingProject.Models.AccountViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HostingProject.Controllers
{
    
    public class AccountController : Controller
    {

        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<IdentityUser> _userManage, SignInManager<IdentityUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManage;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new AccountGetViewModel
            {
                register = new RegisterModel(),
                login = new LoginModel()
            };
            return View(model);
            
        }

        public async Task<IActionResult> Logout() 
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountGetViewModel model)
        {


            if (!ModelState.IsValid)
            {
                string fullName = model.register.FName + " " + model.register.LName;
                var user = new IdentityUser
                {
                    UserName = model.register.FName,
                    Email = model.register.Email,
                };

                var result = await userManager.CreateAsync(user, model.register.PasswordConfirmed);

                if (result.Succeeded)
                {
                    
                    var role = await roleManager.FindByIdAsync("936cd51c-bc11-44f2-a796-4639248418e1");
                    if (role != null) 
                    {
                        var resultUserRole = await userManager.IsInRoleAsync(user, role.Name);
                        if (!resultUserRole) 
                        {
                            await userManager.AddToRoleAsync(user, role.Name);
                            await signInManager.SignInAsync(user, isPersistent: false);
                            return Json(new { success = true, message = "Register Successful", redirectUrl = Url.Action("Index", "Home") });
                        }
                    }
                    

                    
                }
                else
                {
                    string er = "";

                    foreach (var error in result.Errors)
                    {
                        er += error.Description;
                    }
                    return Json(new { success = false, message = er });
                }
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountGetViewModel model)
        {


            if (!ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.login.Email, model.login.Password, false, false); ;
                
                var user = await userManager.FindByEmailAsync(model.login.Email);

                if (user != null)
                {
                     result = await signInManager.PasswordSignInAsync(user.UserName, model.login.Password, false, false);
                }

                if (result.Succeeded)
                {

                    return Json(new { success = true, message = "Login successful" , redirectUrl = Url.Action("Index", "Home") });
                }

                else
                {

                    return Json(new { success = false , message = "Invalid E-mail or Password"});
                    //ModelState.AddModelError("", "Invalid login attempt. Please check your email and password.");
                }
            }
            return View("Index");
        }
    }

}
