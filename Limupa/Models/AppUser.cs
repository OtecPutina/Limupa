using Microsoft.AspNetCore.Identity;

namespace Limupa.Models
{
    public class AppUser:IdentityUser
    {
       
        public string Fullname { get; set; }
        public bool IsAdmin { get; set; }
    }
}
