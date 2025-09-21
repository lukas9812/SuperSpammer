namespace SuperSpammer.Infastructure;

public interface IAttendantService
{
    Task ProcessMessages(string from, string to, int spamNumberCount);
}