using Microsoft.AspNetCore.Identity.UI.Services;
using Resend;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private IResend _resend;

    public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger, IResend resend)
    {
        _configuration = configuration;
        _logger = logger;
        _resend = resend;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var resendproviderKey = _configuration["resendkey"];
        ArgumentNullException.ThrowIfNullOrEmpty(resendproviderKey, nameof(resendproviderKey));
        await Execute(resendproviderKey, subject, message, toEmail);
    }

    private async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        _resend = ResendClient.Create(apiKey);

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

        _logger.LogInformation(resp.Success
                               ? $"Email to {toEmail} queued successfully!"
                               : $"Failure Email to {toEmail}");
    }
}