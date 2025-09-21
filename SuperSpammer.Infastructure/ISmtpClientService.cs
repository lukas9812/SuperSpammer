namespace SuperSpammer.Infastructure;

public interface ISmptClientService
{
    Task SendEmailAsync(string from, string to, string subject);
}