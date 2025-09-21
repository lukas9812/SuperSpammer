namespace SuperSpammer.Infastructure;

public interface ISmtpClientService
{
    Task SendEmailAsync(string from, string to, string subject);
}