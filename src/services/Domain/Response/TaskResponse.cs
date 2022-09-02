namespace Domain;

public class TaskResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public TaskStatus? Status { get; init; }
    public TaskPriority? Priority { get; init; }
    public Guid? AssignedUserId { get; init; }
    public string? AssignedUserFirstName { get; set; }
    public string? AssignedUserLastName { get; set; }
    public DateTime DueDate { get; init; }
    public decimal? PercentageCompleted { get; init; }
    public bool IsOverDue { get; set; }
    public bool? IsPinned { get; init; }
}