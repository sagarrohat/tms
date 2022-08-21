namespace Domain;

public class Notification : EntityBase
{
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
    public NotificationType Type { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public Guid CreatedBy { get; set; }
    public bool Unread { get; set; }
}

