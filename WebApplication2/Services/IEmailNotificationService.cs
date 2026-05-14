namespace WebApplication2.Services;

public interface IEmailNotificationService
{
    Task SendAsync(string to, string subject, string body);
}
