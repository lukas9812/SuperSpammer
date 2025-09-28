namespace SuperSpammer.Engine.Models;

public class EmailCredentials
{
    public bool UseCachedCredentials { get; set; }
    public string? EmailAddress { get; set; } = string.Empty;
    public string? EmailPassword { get; set; } = string.Empty;
}