using SuperSpammer.Infastructure;

namespace SuperSpammer.Architecture;

public class AttendantService : IAttendantService
{
    public AttendantService(ISmptClientService smptClientService)
    {
        _smptClientService = smptClientService;
    }

    public async Task ComposeMessage(string from, string to)
    {
        await _smptClientService.SendEmailAsync(from, to, _message, _message);
        
        // Here should be reference to .html UI for a message (Don't want to have only plain text).
    }
    
    readonly ISmptClientService _smptClientService;
    readonly string _message = "This is spam";
}