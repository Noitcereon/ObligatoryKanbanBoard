using System.Threading.Tasks;

namespace KanbanBoardMVCApp.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendMail(string receiverEmail, string subject, string emailBody);
    }
}