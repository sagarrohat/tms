namespace Domain;

public class MessagingConfiguration
{
    public Guid UserId { get; init; }
    public string? Token { get; set; }
    public DateTime ModifiedOnUtc { get; set; }
}