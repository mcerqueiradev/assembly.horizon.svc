using Assembly.Horizon.Infra.Data.Infrastructure.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Assembly.Horizon.Infra.Data.Infrastructure;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly SmtpSettings _smtpSettings;
    private readonly string _baseUrl;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
        _smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
        _baseUrl = _configuration["AppUrl"];
    }

    public async Task SendVisitScheduledEmailAsync(VisitScheduledEmail model)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Assembly Horizon", _smtpSettings.FromEmail));
        message.To.Add(new MailboxAddress(model.RealtorName, model.RealtorEmail));
        message.Subject = $"New Visit Scheduled - {model.PropertyTitle}";

        var builder = new BodyBuilder();
        builder.HtmlBody = GenerateEmailTemplate(model);
        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    private string GenerateEmailTemplate(VisitScheduledEmail model)
    {
        var confirmUrl = $"{_baseUrl}/api/PropertyVisit/confirm/{model.ConfirmationToken}";
        var declineUrl = $"{_baseUrl}/api/PropertyVisit/decline/{model.ConfirmationToken}";

        return $@"
            <h2>New Property Visit Scheduled</h2>
            <p>A new visit has been scheduled for {model.PropertyTitle}</p>
            
            <h3>Visit Details:</h3>
            <ul>
                <li>Date: {model.VisitDate:d}</li>
                <li>Time: {model.TimeSlot}</li>
                <li>Property: {model.PropertyTitle}</li>
                <li>Address: {model.PropertyAddress}</li>
            </ul>

            <h3>Client Information:</h3>
            <ul>
                <li>Name: {model.UserName}</li>
                <li>Email: {model.UserEmail}</li>
                {(string.IsNullOrEmpty(model.PhoneNumber) ? "" : $"<li>Phone Number: {model.PhoneNumber}</li>")}
            </ul>

            <h3>Notes:</h3>
            <p>{model.Notes ?? "No additional notes"}</p>

            <div style='margin-top: 30px; margin-bottom: 30px;'>
                <a href='{confirmUrl}' style='background-color: #4CAF50; color: white; padding: 12px 24px; text-decoration: none; margin-right: 15px; border-radius: 4px; font-weight: bold;'>Confirm Visit</a>
                <a href='{declineUrl}' style='background-color: #f44336; color: white; padding: 12px 24px; text-decoration: none; border-radius: 4px; font-weight: bold;'>Decline Visit</a>
            </div>

            <p style='color: #666; font-size: 12px;'>Click the buttons above to confirm or decline this visit request.</p>
        ";
    }

    public async Task SendVisitConfirmedEmailAsync(VisitConfirmedEmail model)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Assembly Horizon", _smtpSettings.FromEmail));
        message.To.Add(new MailboxAddress(model.UserName, model.UserEmail));
        message.Subject = $"Visit Confirmed - {model.PropertyTitle}";

        var builder = new BodyBuilder();
        builder.HtmlBody = GenerateConfirmationEmailTemplate(model);
        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    public async Task SendVisitDeclinedEmailAsync(VisitDeclinedEmail model)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Assembly Horizon", _smtpSettings.FromEmail));
        message.To.Add(new MailboxAddress(model.UserName, model.UserEmail));
        message.Subject = $"Visit Request Declined - {model.PropertyTitle}";

        var builder = new BodyBuilder();
        builder.HtmlBody = GenerateDeclinedEmailTemplate(model);
        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    private string GenerateConfirmationEmailTemplate(VisitConfirmedEmail model)
    {
        return $@"
            <h2>Visit Confirmed!</h2>
            <p>Your visit request for {model.PropertyTitle} has been confirmed.</p>
            
            <h3>Visit Details:</h3>
            <ul>
                <li>Date: {model.VisitDate:d}</li>
                <li>Time: {model.TimeSlot}</li>
                <li>Property: {model.PropertyTitle}</li>
            </ul>

            <h3>Realtor Contact Information:</h3>
            <ul>
                <li>Name: {model.RealtorName}</li>
                <li>Email: {model.RealtorEmail}</li>
                {(string.IsNullOrEmpty(model.RealtorPhone) ? "" : $"<li>Phone: {model.RealtorPhone}</li>")}
            </ul>

            <p style='color: #4CAF50; font-weight: bold;'>We look forward to meeting you!</p>
        ";
    }

    private string GenerateDeclinedEmailTemplate(VisitDeclinedEmail model)
    {
        return $@"
            <h2>Visit Request Update</h2>
            <p>Unfortunately, your visit request for {model.PropertyTitle} could not be accommodated at the requested time.</p>
            
            <h3>Visit Details:</h3>
            <ul>
                <li>Date: {model.VisitDate:d}</li>
                <li>Time: {model.TimeSlot}</li>
                <li>Property: {model.PropertyTitle}</li>
            </ul>

            <p>Please feel free to request another time slot or contact the realtor directly:</p>
            <ul>
                <li>Name: {model.RealtorName}</li>
                <li>Email: {model.RealtorEmail}</li>
                {(string.IsNullOrEmpty(model.RealtorPhone) ? "" : $"<li>Phone: {model.RealtorPhone}</li>")}
            </ul>

            <p style='color: #666;'>We appreciate your understanding.</p>
        ";
    }
}
