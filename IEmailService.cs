using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.MicrosoftGraph;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.Email;

public interface IEmailService
{
    Task<bool> SendEmailAsync(List<MemberNameAndEmailDto> toEmails, Dictionary<string, object> dynamicParameters, string templateId, Dictionary<string, string> attachments = null);
    Task SendEmail(string message, string toEmail = default, string toName = default);
}
