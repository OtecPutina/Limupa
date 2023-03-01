using Limupa.Areas.ControlPanel.ViewModels;
using Limupa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    
    public class AccountController : Controller
    {
        public UserManager<AppUser> _userManager { get; }
        public SignInManager<AppUser> _signInManager { get; }

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginViewModel)
        {
            if(!ModelState.IsValid) return View();
            AppUser admin = await _userManager.FindByNameAsync(adminLoginViewModel.UserName);
            if(admin == null)
            {
                ModelState.AddModelError("", "Username or Password is invalid");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(admin, adminLoginViewModel.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is invalid");
                return View();
            }
            return RedirectToAction("Index", "Dashboard");

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
