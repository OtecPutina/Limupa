using Limupa.Context;
using Limupa.Models;
using Limupa.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Controllers
{
    public class AccountUserController : Controller
    {
        public AccountUserController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,SignInManager<AppUser> signInManager,AppDbContext context)
        {
            _userManager = userManager; 
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }

        public UserManager<AppUser> _userManager { get; }
        public RoleManager<IdentityRole> _roleManager { get; }
        public SignInManager<AppUser> _signInManager { get; }
        public AppDbContext _context { get; }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel userRegisterViewModel)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = null;
            user = await  _userManager.FindByNameAsync(userRegisterViewModel.UserName);
            if (user != null)
            {
                ModelState.AddModelError("Username", "Already exist!");
                return View();
            }
            user = await _userManager.FindByEmailAsync(userRegisterViewModel.Email);
            if (user != null)
            {
                ModelState.AddModelError("Email", "Already exist!");
                return View();
            }
            user = new AppUser
            {
                Fullname = userRegisterViewModel.FullName,
                UserName = userRegisterViewModel.UserName,
                Email = userRegisterViewModel.Email,
                IsAdmin = false
            };
            var result=await _userManager.CreateAsync(user, userRegisterViewModel.Password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            await _userManager.AddToRoleAsync(user, "Member");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("index","home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(userLoginViewModel.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or Password is invalid!");
                return View();
            }
            var result=await _signInManager.PasswordSignInAsync(user, userLoginViewModel.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is invalid!");
                return View();
            }
            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "accountuser");
        }
        public async Task<IActionResult> Profile()
        {
            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            List<Order> orders = _context.Orders.Where(x=>x.AppUserId==member.Id).ToList();
            return View(orders);
        }
    }
}
