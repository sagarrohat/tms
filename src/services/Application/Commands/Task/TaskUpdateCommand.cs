using Common;
using Domain;
using ExternalInfrastructure;
using Newtonsoft.Json;
using Persistence;
using Task = System.Threading.Tasks.Task;

namespace Application;

public class TaskUpdateCommand : ITaskUpdateCommand
{
    private readonly ITaskByIdQuery _taskByIdQuery;
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly INotificationProducer _notificationProducer;

    public TaskUpdateCommand(IRepositoryFactory repositoryFactory, INotificationProducer notificationProducer,
        ITaskByIdQuery taskByIdQuery)
    {
        _repositoryFactory = repositoryFactory;
        _notificationProducer = notificationProducer;
        _taskByIdQuery = taskByIdQuery;
    }

    public async Task ExecuteAsync(UserContext userContext, TaskUpdateRequest request)
    {
        ValidateRequest(request);

        PreprocessRequest(request);

        var previous = await _taskByIdQuery.ExecuteAsync(userContext, request.Id);

        var isAlreadyCancelledOrCompleted =
            previous.Status is Domain.TaskStatus.Cancelled or Domain.TaskStatus.Completed;
        if (isAlreadyCancelledOrCompleted)
        {
            throw new AppException(ErrorCodes.BadRequest,
                string.Format(ErrorMessages.TaskCompletedOrCancelled, request.Id));
        }

        await _repositoryFactory.TaskRepository.UpdateAsync(userContext, previous, request);

        ProcessNotification(userContext, request, previous);
    }

    private void ProcessNotification(UserContext userContext, TaskUpdateRequest request, TaskResponse previous)
    {
        _notificationProducer.Produce(new NotificationMessage(userContext, EntityType.Task, ActionType.Updated,
                JsonConvert.SerializeObject(new TaskUpdateNotification()
                {
                    Current = request,
                    Previous = previous
                })
            )
        );
    }

    private static void ValidateRequest(TaskUpdateRequest request)
    {
        if (request.Id == Guid.Empty)
            throw new AppException(ErrorCodes.BadRequest, string.Format(ErrorMessages.Required, nameof(request.Id)));

        if (string.IsNullOrEmpty(request.Title))
            throw new AppException(ErrorCodes.BadRequest, string.Format(ErrorMessages.Required, nameof(request.Title)));
    }

    private static void PreprocessRequest(TaskUpdateRequest request)
    {
        request.DueDate = request.DueDate.AsUtcDateOnly();
    }
}