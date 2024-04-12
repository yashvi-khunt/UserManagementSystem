using LS.DAL.Helper;

namespace LS.BLL.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
