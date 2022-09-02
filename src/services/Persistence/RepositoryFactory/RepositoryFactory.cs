namespace Persistence;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly DatabaseContext _dbContext;

    public RepositoryFactory(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ITaskRepository TaskRepository => new TaskRepository(_dbContext);
    public IUserRepository UserRepository => new UserRepository(_dbContext);
    public IPinnedTaskRepository PinnedTaskRepository => new PinnedTaskRepository(_dbContext);

    public IMessagingConfigurationRepository MessagingConfigurationRepository =>
        new MessagingConfigurationRepository(_dbContext);
}