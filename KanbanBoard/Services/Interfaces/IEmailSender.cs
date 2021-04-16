using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanbanBoardMVCApp.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendMailAsync(string receiverEmail, string subject, string emailBody);
        Task SendMailToMultipleAsync(List<string> receiverEmails, string subject, string emailBody);
    }
}