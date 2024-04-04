using LS.DAL.Helper;

namespace LS.BLL.Repositories
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
