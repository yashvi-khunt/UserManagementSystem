using LS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace LS.DAL.Helper
{
    public partial class LoginDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed Roles
            var adminRole = new IdentityRole("Admin");
            builder.Entity<IdentityRole>().HasData(
               adminRole,
                new IdentityRole { Name = "User" }
                );

            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();


            //Seeding the User to AspNetUsers table
            var admin = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                EmailConfirmed = true,
                IsActivated = true,
                CreatedDate = DateTime.Now,
            };
            builder.Entity<ApplicationUser>().HasData(admin);


            //Seeding the relation between our user and role to AspNetUserRoles table
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRole.Id,
                    UserId = admin.Id
                }
            );
        }
    }
}

