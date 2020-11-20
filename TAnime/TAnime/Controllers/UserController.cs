using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TAnime.Models.Identities;


namespace TAnime.Controllers
{
    //[Authorize(Roles = "System Admin, Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              RoleManager<IdentityRole> roleManager,
                              IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var users = userManager.Users;
            var newUsers = users.Select(u => new UserViewModel()
            {
                UserId = u.Id,
                Email = u.Email,
                Address = u.Address,
                Avatar = u.Avatar,
                City = u.City,
                FullName = u.FullName
            }).ToList();
            foreach(var user in newUsers)
            {
                user.RolesName = GetRolesName(user.UserId);
            }
            return View(newUsers);
        }

        public string GetRolesName(string userId)
        {
            var user = Task.Run(async () => await userManager.FindByIdAsync(userId)).Result;
            var roles = Task.Run(async () => await userManager.GetRolesAsync(user)).Result;
            return roles != null ? string.Join(", ", roles) : string.Empty;
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = roleManager.Roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Address = model.Address,
                    City = model.City,
                    FullName = model.FullName
                };
                string fileName = string.Empty;
                if (model.ImagePath != null)
                {
                    var uploadFodel = Path.Combine(webHostEnvironment.WebRootPath, "avatars");
                    fileName = $"{Guid.NewGuid()}_{model.ImagePath.FileName}";
                    var filePath = Path.Combine(uploadFodel, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImagePath.CopyToAsync(fs);
                    }
                }
                user.Avatar = fileName;
                var result = await userManager.CreateAsync(user: user, password: model.Password);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.RoleId))
                    {
                        var role = await roleManager.FindByIdAsync(model.RoleId);
                        var addRole = await userManager.AddToRoleAsync(user, role.Name);
                        if (addRole.Succeeded)
                        {
                            return RedirectToAction("Index", "User");
                        }
                        else
                        {
                            foreach(var error in addRole.Errors)
                            {
                                ModelState.AddModelError(" ", error.Description);
                            }
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Roles = roleManager.Roles;
            var user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                var model = new EditUserViewModel()
                {
                    FullName = user.FullName,
                    EmailId = user.Id,
                    Address = user.Address,
                    City = user.City,
                    Email = user.Email,
                    Avatar = user.Avatar
                };
                var rolesName = await userManager.GetRolesAsync(user);
                if(rolesName != null && rolesName.Any())
                {
                    var role = await roleManager.FindByNameAsync(rolesName.FirstOrDefault());
                    model.RoleId = role.Id;
                }
                return View(model);


            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.EmailId);
                if(user != null)
                {
                    user.Email = model.Email;
                    user.Id = model.EmailId;
                    user.FullName = model.FullName;
                    user.City = model.City;
                    user.Address = model.Address;
                    user.UserName = model.Email;

                    var fileName = string.Empty;
                    if(model.ImagePath != null)
                    {
                        if(user.Avatar != null && user.Avatar.Any())
                        {
                            var delAvatar = Path.Combine(webHostEnvironment.WebRootPath, "avatars", user.Avatar);
                            System.IO.File.Delete(delAvatar);
                        }

                        var uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "avatars");
                        fileName = $"{Guid.NewGuid()}_{model.ImagePath.FileName}";
                        var filePath = Path.Combine(uploadFolder, fileName);
                        using(var fs = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ImagePath.CopyToAsync(fs);
                        }
                        user.Avatar = fileName;
                    }
                    else
                    {
                        user.Avatar = model.Avatar;
                    }
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var roles = await userManager.GetRolesAsync(user);
                        var delRole = await userManager.RemoveFromRolesAsync(user, roles);
                        if (!string.IsNullOrEmpty(model.RoleId))
                        {
                            var role = await roleManager.FindByIdAsync(model.RoleId);
                            var addRole = await userManager.AddToRoleAsync(user, role.Name);
                            if (addRole.Succeeded)
                            {
                                return RedirectToAction("Index", "User");
                            }
                            return RedirectToAction("Index", "User");
                        }
                    }
                    else
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }            
            }
            return View(model);
        }

        [Route("/User/Delete/{id}")]
        public async Task<JsonResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { result});
                }
                else
                {  
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return null;
        }
    }
}
