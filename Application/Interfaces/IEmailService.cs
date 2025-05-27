using Microsoft.Graph.Models;

namespace Application.Interfaces;
public interface IEmailService
{
    Task SendEmailAsync(Message message);
}
