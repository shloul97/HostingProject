using HostingProject.Models;
using HostingProject.Models.RolesViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HostingProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        //private readonly ProjDbContext db;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;


        public RoleController(RoleManager<IdentityRole> _roleManager, UserManager<IdentityUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            //db = _db;
        }
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveRole(string roleId)
        {
            
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
            }
            else 
            {
                await roleManager.DeleteAsync(role);
            }
            
            
            var roles = roleManager.Roles;
            return View("AllRoles",roles);

            
            
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return View("CreateRole");
            }
            else 
            {
                var model = new EditRoleView
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                foreach (var user in userManager.Users) 
                {
                    var resultRole = await userManager.IsInRoleAsync(user, role.Name);
                    if (resultRole) 
                    {
                        model.Users.Add(new UserRole
                        {
                            UserId = user.Id,
                            UserName = user.UserName,
                            Email = user.Email
                        });
                    }
                }

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid) 
            {
                //var roles = roleManager.Roles;
                var identity = new IdentityRole
                {
                    Name = model.roleName
                };

                var result = await roleManager.CreateAsync(identity);

                if (result.Succeeded) 
                {
                    return RedirectToAction("AllRoles");
                }


                foreach (var err in result.Errors) {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }
            return View("CreateRole", "Role");
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleView model)
        {
            var roleModel = roleManager.Roles;
            var role = await roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
            {
                return View("CreateRole");
            }
            else
            {

                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded) 
                {
                    return View("AllRoles", roleModel);
                }
                else 
                {
                    foreach (var err in result.Errors) 
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> addToRole(string roleId) 
        {

            var model = new AddUserModel {
                roleId = roleId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> addToRole(AddUserModel model)
        {
            if (ModelState.IsValid) 
            {
                var roleResult = await roleManager.FindByIdAsync(model.roleId);
                if (roleResult != null)
                {
                    var resultUser = await userManager.FindByEmailAsync(model.UserEmail);
                    if (resultUser != null)
                    {
                        await userManager.AddToRoleAsync(resultUser, roleResult.Name);
                        return RedirectToAction("AllRoles");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "There's No user with E-mail: "+ model.UserEmail);
                        return View("AddToRole", model);
                    }

                }
                else
                {
                    return RedirectToAction("AllRoles");
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BlockUser(string userId,string roleId) 
        {
            if (ModelState.IsValid)
            {
                var roleResult = await roleManager.FindByIdAsync(roleId);
                if (roleResult != null)
                {
                    var resultUser = await userManager.FindByIdAsync(userId);
                    if (resultUser != null)
                    {
                        await userManager.RemoveFromRoleAsync(resultUser, roleResult.Name);
                        return RedirectToAction("EditRole", roleId);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "There's No user with this E-mail");
                        return View("AddToRole", roleId);
                    }

                }
                else
                {
                    return RedirectToAction("EditRole", roleId);
                }
            }
            return View("EditRole", roleId);        
        }
    }
}
