using System.ComponentModel.DataAnnotations;

namespace LS.DAL.ViewModels
{
    public class VMChangePassword
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
