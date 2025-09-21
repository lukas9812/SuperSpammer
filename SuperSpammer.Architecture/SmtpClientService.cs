using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SuperSpammer.Architecture.Models;
using SuperSpammer.Infastructure;

namespace SuperSpammer.Architecture;

public class SmtpClientService : ISmtpClientService
{
    public SmtpClientService(IOptions<EmailCredentials> emailCredentials)
    {
        _emailCredentials = emailCredentials.Value;
    }
    public async Task SendEmailAsync(string from, string to, string subject)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "EmailTemplate.html");
        var htmlBody = await File.ReadAllTextAsync(filePath);

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(from, from));
        message.To.Add(new MailboxAddress(to, to));
        message.Subject = subject;

        message.Body = new TextPart("html")
        {
            Text = htmlBody
        };

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_smtpServer, _port, _useSsl);
            await client.AuthenticateAsync(_emailCredentials.EmailAddress, _emailCredentials.EmailPassword);
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
    }
    
    readonly string _smtpServer = "smtp.gmail.com";
    readonly int _port = 587;
    readonly string _username = "tvuj@email.com";
    readonly string _password = "xxxx";
    readonly bool _useSsl = false;
    readonly EmailCredentials _emailCredentials;
}