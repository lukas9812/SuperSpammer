namespace SuperSpammer.Infastructure;

public interface IAttendantService
{
    Task ComposeMessage(string from, string to);
}