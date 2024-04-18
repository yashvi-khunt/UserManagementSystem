using System.ComponentModel.DataAnnotations;

namespace LS.DAL.ViewModels
{
    public class VMChangePassword
    {
        

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
