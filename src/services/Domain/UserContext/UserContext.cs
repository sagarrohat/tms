namespace Domain;

public class UserContext
{
    public Guid UserId { get; private set; }

    public UserContext(Guid userId)
    {
        UserId = userId;
    }
}