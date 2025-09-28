using SuperSpammer.Infastructure;

namespace SuperSpammer.Engine;

public class AttendantService : IAttendantService
{
    public AttendantService(ISmtpClientService smtpClientService)
    {
        _smtpClientService = smtpClientService;
    }

    public async Task<bool> ProcessMessages(string to, int spamNumberCount)
    {
        bool retval = false;
        for (var i = 0; i < spamNumberCount; i++)
        {
            retval = await _smtpClientService.SendEmailAsync(to, _message);
        }
        return retval;
    }
    
    readonly ISmtpClientService _smtpClientService;
    readonly string _message = "This is spam";
}