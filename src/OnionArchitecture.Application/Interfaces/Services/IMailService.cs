using System.Threading.Tasks;
using OnionArchitecture.Application.DTOs.Mail;

namespace OnionArchitecture.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}