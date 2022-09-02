namespace Domain;

public class TaskCreateNotification
{
    public Guid Id { get; init; }
    public TaskCreateRequest Current { get; init; } = null!;
}