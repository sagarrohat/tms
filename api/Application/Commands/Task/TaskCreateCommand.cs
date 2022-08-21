using Common;
using Domain;
using Persistence;
using Task = System.Threading.Tasks.Task;

namespace Application;

public class TaskCreateCommand : ITaskCreateCommand
{
    private readonly ITaskRepository _taskRepository;
    private readonly INotificationRepository _notificationRepository;
    
    public TaskCreateCommand(ITaskRepository taskRepository, INotificationRepository notificationRepository)
    {
        _taskRepository = taskRepository;
        _notificationRepository = notificationRepository;
    }

    public async Task<Guid> ExecuteAsync(UserContext userContext, TaskCreateRequest request)
    {
        PreprocessRequest(request);
        
        ValidateRequest(request);
        
        var taskId = await _taskRepository.CreateAsync(userContext, request);

        await ProcessNotification(userContext, request, taskId);

        return taskId;
    }

    private async Task ProcessNotification(UserContext userContext, TaskCreateRequest request, Guid taskId)
    {
        var isBeingAssigned = request.AssignedUserId.HasValue && request.AssignedUserId.Value != Guid.Empty;
        if (isBeingAssigned)
        {
            var notToCurrentUser = request.AssignedUserId!.Value != userContext.UserId;
            if (notToCurrentUser)
            {
                await _notificationRepository.CreateAsync(new Notification
                {
                    UserId = request.AssignedUserId!.Value,
                    TaskId = taskId,
                    Type = NotificationType.TaskAssigned,
                    CreatedBy = userContext.UserId,
                });
            }
        }
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
