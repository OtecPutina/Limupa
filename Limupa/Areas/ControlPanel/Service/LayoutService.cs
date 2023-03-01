using Limupa.Models;
using Microsoft.AspNetCore.Identity;

namespace Limupa.Areas.ControlPanel.Service
{
    public class LayoutService
    {
        public LayoutService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public UserManager<AppUser> _userManager { get; }
        public IHttpContextAccessor _httpContextAccessor { get; }

        public async Task<AppUser> GetUser()
        {
            string name = _httpContextAccessor.HttpContext.User.Identity.Name;
            AppUser appUser = await _userManager.FindByNameAsync(name);
            return appUser;

        }
    }
}
