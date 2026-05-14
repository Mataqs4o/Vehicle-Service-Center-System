namespace WebApplication2.Services;

public class ConsoleEmailNotificationService(ILogger<ConsoleEmailNotificationService> logger) : IEmailNotificationService
{
    public Task SendAsync(string to, string subject, string body)
    {
        logger.LogInformation("Email => To: {To}, Subject: {Subject}, Body: {Body}", to, subject, body);
        return Task.CompletedTask;
    }
}
