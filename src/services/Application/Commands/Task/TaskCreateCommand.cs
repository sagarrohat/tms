using Common;
using Domain;
using ExternalInfrastructure;
using Newtonsoft.Json;
using Persistence;

namespace Application;

public class TaskCreateCommand : ITaskCreateCommand
{
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly INotificationProducer _notificationProducer;

    public TaskCreateCommand(IRepositoryFactory repositoryFactory, INotificationProducer notificationProducer)
    {
        _repositoryFactory = repositoryFactory;
        _notificationProducer = notificationProducer;
    }

    public async Task<Guid> ExecuteAsync(UserContext userContext, TaskCreateRequest request)
    {
        PreprocessRequest(request);

        ValidateRequest(request);

        var taskId = await _repositoryFactory.TaskRepository.CreateAsync(userContext, request);

        ProcessNotification(userContext, request, taskId);

        return taskId;
    }

    private void ProcessNotification(UserContext userContext, TaskCreateRequest request, Guid taskId)
    {
        _notificationProducer.Produce(new NotificationMessage(userContext, EntityType.Task, ActionType.Added,
            JsonConvert.SerializeObject(new TaskCreateNotification()
                {
                    Id = taskId,
                    Current = request
                }
            )));
    }

    private static void ValidateRequest(TaskCreateRequest request)
    {
        if (string.IsNullOrEmpty(request.Title))
            throw new AppException(ErrorCodes.BadRequest, string.Format(ErrorMessages.Required, nameof(request.Title)));
    }

    private static void PreprocessRequest(TaskCreateRequest request)
    {
        request.DueDate = request.DueDate.AsUtcDateOnly();
    }
}