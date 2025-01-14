using WoodyFrontToBack.Helpers.Emails;

namespace WoodyFrontToBack.Services.Interfaces;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}
