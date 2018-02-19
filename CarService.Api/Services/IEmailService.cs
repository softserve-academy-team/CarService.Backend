using System.Threading.Tasks;

namespace CarService.Api.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
