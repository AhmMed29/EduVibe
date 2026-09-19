using Microsoft.AspNetCore.Identity.UI.Services;
using Resend;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly IResend _resend;

    public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger, IResend resend)
    {
        _configuration = configuration;
        _logger = logger;
        _resend = resend;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var resendproviderKey = Environment.GetEnvironmentVariable("resendkey");
        await Execute(resendproviderKey, subject, message, toEmail);
    }

    private async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        // other options(overloads) & more ...
        // EmailSendAsync (email , cancelation token) --> [there are many overrides like (send one mail ! to avoid recurrency)]
        
        var resp = await _resend.EmailSendAsync( new EmailMessage()
        {
            // the name user see <any related word to the service@eduvibe.ahmedghazi.me>
            From = "EduVibe <noreply@eduvibe.ahmedghazi.me>",
            To = toEmail,
            Subject = subject,
            HtmlBody = message,
        } );
        if (resp.Success)
        {
            _logger.LogInformation("Email to {toEmail}", toEmail);
        }
        else
        {
            _logger.LogError("Failed to send email to {toEmail}. Error: {@Error}", toEmail, resp.Exception);
            throw new Exception($"Email sending failed to {toEmail}");
        }
    }
}