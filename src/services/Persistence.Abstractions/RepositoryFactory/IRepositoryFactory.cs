namespace Persistence;

public interface IRepositoryFactory
{
    public ITaskRepository TaskRepository { get; }
    public IUserRepository UserRepository { get; }
    public IPinnedTaskRepository PinnedTaskRepository { get; }
    public IMessagingConfigurationRepository MessagingConfigurationRepository { get; }
}