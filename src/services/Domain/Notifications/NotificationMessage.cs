namespace Domain;

public enum EntityType
{
    Task = 1
}

public enum ActionType
{
    Added = 1,
    Updated = 2,
}

public class NotificationMessage
{
    public UserContext UserContext { get; }
    public EntityType EntityType { get; }
    public ActionType ActionType { get; }
    public string Data { get; }

    public NotificationMessage(UserContext userContext, EntityType entityType, ActionType actionType, string data)
    {
        UserContext = userContext;
        EntityType = entityType;
        ActionType = actionType;
        Data = data;
    }
}