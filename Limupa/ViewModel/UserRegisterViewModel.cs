using System.ComponentModel.DataAnnotations;

namespace Limupa.ViewModel
{
    public class UserRegisterViewModel
    {
        [StringLength(100)]
        public string FullName { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(200),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(maximumLength:30,MinimumLength =8), DataType(DataType.Password)]
        public string Password { get; set; }
        [StringLength(maximumLength: 30, MinimumLength = 8), DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
