using Limupa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class DashboardController : Controller
    {
        public UserManager<AppUser> _userManager { get; }
        public RoleManager<IdentityRole> _roleManager { get; }

        public DashboardController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser admin = new AppUser
        //    {
        //        UserName = "admin",
        //        Fullname= "admin"
        //    };
        //    var result=await _userManager.CreateAsync(admin,"admin123");
        //    return Ok("Created");

        //}
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1= new IdentityRole("Admin");
        //    IdentityRole role2= new IdentityRole("SuperAdmin");
        //    IdentityRole role3= new IdentityRole("Member");
        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);
        //    return Ok("Created");
        //}
        //public async Task<IActionResult> AddToRole()
        //{
        //    AppUser appUser = await _userManager.FindByNameAsync("admin");
        //    await _userManager.AddToRoleAsync(appUser, "SuperAdmin");
        //    return Ok("Role Added");
        //}
    }
}
