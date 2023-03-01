using System.ComponentModel.DataAnnotations;

namespace Limupa.ViewModel
{
    public class UserLoginViewModel
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
