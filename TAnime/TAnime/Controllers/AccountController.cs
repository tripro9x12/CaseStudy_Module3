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
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccountController(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager,
                            IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
        }
        
        [HttpGet]
        [Route("/Account/Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
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
                    using(var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImagePath.CopyToAsync(fs);
                    }
                }
                user.Avatar = fileName;
                var result = await userManager.CreateAsync(user: user, password: model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user: user, isPersistent: false);
                    return RedirectToAction("Index", "Movie");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult Index()
        {
            var user = userManager.Users;
            var model = new List<UserViewModel>();
            model = user.Select(u=> new UserViewModel()
            {
                UserId = u.Id,
                Email = u.Email,
                Address = u.Address,
                Avatar = u.Avatar,
                City = u.City
            }).ToList();

            return View(model);
        }
        [HttpGet]
        [Route("/Account/Login")]
        public IActionResult Login(string Url)
        {
            ViewBag.ReturnUrl = Url;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                                userName: model.Email,
                                password: model.Password,
                                isPersistent: model.RememberMe,
                                lockoutOnFailure: false
                                );
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Movie");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Attemp");
                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                }
             }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel usermodel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);
            var result = await userManager.ChangePasswordAsync(user, usermodel.oldPassword, usermodel.newPassword);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", "Movie");
            }
            return View();
        }
    }
}
