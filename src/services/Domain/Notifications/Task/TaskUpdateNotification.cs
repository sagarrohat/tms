namespace Domain;

public class TaskUpdateNotification
{
    public TaskUpdateRequest Current { get; init; } = null!;
    public TaskResponse Previous { get; init; } = null!;
}