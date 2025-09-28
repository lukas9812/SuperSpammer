namespace SuperSpammer.Infastructure;

public interface IAttendantService
{
    Task<bool> ProcessMessages(string to, int spamNumberCount);
}