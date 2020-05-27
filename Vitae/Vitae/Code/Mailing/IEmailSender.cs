using System.Threading.Tasks;

namespace Vitae.Code.Mailing
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}