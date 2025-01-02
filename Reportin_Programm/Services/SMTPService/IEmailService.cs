using Reportin_Programm.Services.DTOs;

namespace Reportin_Programm.Services.SMTPService
{
    public interface IEmailService
    {
        Task SendMail(EmailDTO mailInfo);
    }
}
