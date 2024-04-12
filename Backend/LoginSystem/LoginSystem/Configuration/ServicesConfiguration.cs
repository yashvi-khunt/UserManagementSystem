using LS.BLL.Interfaces;
using LS.BLL.Repository;
using LS.BLL.Services;
using LS.Core.Middlewares;

namespace LoginSystem.Configuration
{
    public static class ServicesConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IDBRepository<>), typeof(DBRepository<>)); 
        }

        public static void AddRepoServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ExceptionMiddleware>();
        }

    }
}
