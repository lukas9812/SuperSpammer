using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SuperSpammer.Infastructure;
using Microsoft.Extensions.Caching.Memory;
using SuperSpammer.Engine.Models;

namespace SuperSpammer.Engine;

public class SmtpClientService : ISmtpClientService
{
    public SmtpClientService(IOptions<EmailCredentials> emailCredentials, IMemoryCache  cache)
    {
        _emailCredentials = emailCredentials.Value;
        _cache = cache;
    }
    public async Task<bool> SendEmailAsync(string to, string subject)
    {
        GetSenderCredentials(out var emailAddress, out var emailPassword);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "EmailTemplate.html");
        var htmlBody = await File.ReadAllTextAsync(filePath);

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("SuperSpammer_TestEnvironment", emailAddress));
        message.To.Add(new MailboxAddress(to, to));
        message.Subject = subject;

        message.Body = new TextPart("html")
        {
            Text = htmlBody
        };
        // du diz tol auz

        using var client = new SmtpClient();

        if (string.IsNullOrEmpty(emailAddress) && string.IsNullOrEmpty(emailPassword))
        {
            // ToDo: Add logger and resolve logic.
            return false;
        }
        
        try
        {
            await client.ConnectAsync(_smtpServer, _port, _useSsl);
            await client.AuthenticateAsync(emailAddress, emailPassword);
            await client.SendAsync(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Send email error: {ex.Message}");
            throw;
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
        
        return true;
    }

    void GetSenderCredentials(out string? emailAddress, out string? emailPassword)
    {
        if (_emailCredentials.UseCachedCredentials)
        {
            _cache.TryGetValue("UserEmail",  out emailAddress);
            _cache.TryGetValue("UserEmailPwd", out emailPassword);
            return;
        }
        
        emailAddress = _emailCredentials.EmailAddress;
        emailPassword = _emailCredentials.EmailPassword;
    }
    
    readonly string _smtpServer = "smtp.gmail.com";
    readonly int _port = 587;
    readonly bool _useSsl = false;
    readonly EmailCredentials _emailCredentials;
    readonly IMemoryCache _cache;
}