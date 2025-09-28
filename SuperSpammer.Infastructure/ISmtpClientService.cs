namespace SuperSpammer.Infastructure;

public interface ISmtpClientService
{
    Task<bool> SendEmailAsync(string to, string subject);
}