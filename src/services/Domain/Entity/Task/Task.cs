namespace Domain;

public class Task : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.New;
    public TaskPriority Priority { get; set; } = TaskPriority.Normal;
    public Guid? AssignedUserId { get; set; }
    public User? AssignedUser { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime ModifiedOnUtc { get; set; }
    public DateTime? AssignedOnUtc { get; set; }
    public DateTime? CancelledOnUtc { get; set; }
    public DateTime? CompletedOnUtc { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
    public decimal PercentageCompleted { get; set; }
}
