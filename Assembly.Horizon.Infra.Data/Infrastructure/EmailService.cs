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

    public async Task SendProposalSubmittedEmailAsync(ProposalSubmittedEmail model)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Assembly Horizon", _smtpSettings.FromEmail));
        message.To.Add(new MailboxAddress(model.RealtorName, model.RealtorEmail));
        message.Subject = $"New {model.ProposalType} Proposal - {model.PropertyTitle}";

        var builder = new BodyBuilder();
        builder.HtmlBody = GenerateProposalEmailTemplate(model);
        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    private string GenerateProposalEmailTemplate(ProposalSubmittedEmail model)
    {
        return $@"
        <h2>New Property Proposal Received</h2>
        <p>A new {model.ProposalType.ToLower()} proposal has been submitted for {model.PropertyTitle}</p>
        
        <h3>Proposal Details:</h3>
        <ul>
            <li>Type: {model.ProposalType}</li>
            <li>Proposed Value: {model.ProposedValue:C}</li>
            <li>Payment Method: {model.PaymentMethod}</li>
            <li>Intended Move Date: {model.IntendedMoveDate:d}</li>
            <li>Property: {model.PropertyTitle}</li>
            <li>Address: {model.PropertyAddress}</li>
        </ul>

        <h3>Client Information:</h3>
        <ul>
            <li>Name: {model.UserName}</li>
            <li>Email: {model.UserEmail}</li>
            {(string.IsNullOrEmpty(model.PhoneNumber) ? "" : $"<li>Phone Number: {model.PhoneNumber}</li>")}
        </ul>

        <p style='color: #666; font-size: 12px;'>You can review and respond to this proposal through the Assembly Horizon platform.</p>
    ";
    }

    public async Task SendContractSubmittedEmailAsync(ContractSubmittedEmail model)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Assembly Horizon", _smtpSettings.FromEmail));
        message.To.Add(new MailboxAddress(model.CustomerName, model.CustomerEmail));
        message.Cc.Add(new MailboxAddress(model.RealtorName, model.RealtorEmail));
        message.Subject = $"New Contract Created - {model.PropertyTitle}";

        var builder = new BodyBuilder();
        builder.HtmlBody = GenerateContractEmailTemplate(model);
        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
    private string GenerateContractEmailTemplate(ContractSubmittedEmail model)
    {
        return $@"
        <h2>New Contract Created</h2>
        <p>A new contract has been created for {model.PropertyTitle}</p>
        
        <h3>Contract Details:</h3>
        <ul>
            <li>Type: {model.ContractType}</li>
            <li>Contract Value: {model.ContractValue:C}</li>
            <li>Payment Frequency: {model.PaymentFrequency}</li>
            <li>Start Date: {model.StartDate:d}</li>
            <li>End Date: {model.EndDate:d}</li>
            <li>Security Deposit: {model.SecurityDeposit:C}</li>
            <li>Property Address: {model.PropertyAddress}</li>
        </ul>

        <h3>Contact Information:</h3>
        <ul>
            <li>Customer: {model.CustomerName} ({model.CustomerEmail})</li>
            <li>Realtor: {model.RealtorName} ({model.RealtorEmail})</li>
        </ul>

        <p>The contract document is attached to this email for your records.</p>

        <p style='color: #666; font-size: 12px;'>You can view and manage this contract through the Assembly Horizon platform.</p>
    ";
    }

}
