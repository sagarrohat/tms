namespace Domain;

public class TaskUpdateRequest
{
    public Guid Id { get; set; }
    public string Title  { get; init; } = null!;
    public string? Description { get; set; } = null;
    public TaskStatus Status { get; set; } = TaskStatus.New;
    public TaskPriority Priority { get; set; } = TaskPriority.Normal;
    public Guid? AssignedUserId { get; set; } = null;
    public DateTime DueDate { get; set; }
    public decimal PercentageCompleted { get; set; } = 0;
}