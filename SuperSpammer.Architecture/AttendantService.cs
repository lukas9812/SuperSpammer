using SuperSpammer.Infastructure;

namespace SuperSpammer.Architecture;

public class AttendantService : IAttendantService
{
    public AttendantService(ISmtpClientService smtpClientService)
    {
        _smtpClientService = smtpClientService;
    }

    public async Task ProcessMessages(string from, string to, int spamNumberCount)
    {
        for (var i = 0; i <= spamNumberCount; i++)
        {
            await _smtpClientService.SendEmailAsync(from, to, _message);
        }
    }
    
    readonly ISmtpClientService _smtpClientService;
    readonly string _message = "This is spam";
}