using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LS.DAL.Helper
{
    public partial class LoginDbContext : IdentityDbContext<ApplicationUser>
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
