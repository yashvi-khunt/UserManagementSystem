using LS.DAL.Helper;

namespace LS.BLL.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
