using Microsoft.AspNetCore.Identity;


namespace LS.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActivated { get; set; }
    }
}
