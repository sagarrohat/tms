using Common;
using Domain;
using Persistence;
using Task = System.Threading.Tasks.Task;

namespace Application;

public class TaskUpdateCommand : ITaskUpdateCommand
{
    private readonly ITaskByIdQuery _taskByIdQuery;
    private readonly ITaskRepository _taskRepository;
    private readonly INotificationRepository _notificationRepository;

    public TaskUpdateCommand(ITaskRepository taskRepository, INotificationRepository notificationRepository,
        ITaskByIdQuery taskByIdQuery)
    {
        _taskRepository = taskRepository;
        _notificationRepository = notificationRepository;
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
        
        await _taskRepository.UpdateAsync(userContext, previous, request);

        await ProcessNotification(userContext, request, previous);
    }

    private async Task ProcessNotification(UserContext userContext, TaskUpdateRequest request, TaskResponse previous)
    {
        var isPreviouslyAssigned = previous.AssignedUserId.HasValue && previous.AssignedUserId != Guid.Empty;
        if (isPreviouslyAssigned)
        {
            await ProcessPreviouslyAssignedTaskNotification(userContext, request, previous);
        }
        else
        {
            var isBeingAssigned = request.AssignedUserId.HasValue && request.AssignedUserId != Guid.Empty;
            var notToCurrentUser = request.AssignedUserId != userContext.UserId;

            if (isBeingAssigned && notToCurrentUser)
            {
                await _notificationRepository.CreateAsync(new Notification
                {
                    UserId = request.AssignedUserId!.Value,
                    TaskId = previous.Id,
                    Type = NotificationType.TaskAssigned,
                    CreatedBy = userContext.UserId,
                });
            }
        }
    }

    private async Task ProcessPreviouslyAssignedTaskNotification(UserContext userContext, TaskUpdateRequest request,
        TaskResponse previous)
    {
        var wasNotAssignedToCurrentUser = previous.AssignedUserId != userContext.UserId;
        var isBeingUnassigned = request.AssignedUserId is null || request.AssignedUserId == Guid.Empty;
        var isBeingReassigned = request.AssignedUserId != previous.AssignedUserId;

        if (isBeingUnassigned)
        {
            if (wasNotAssignedToCurrentUser)
            {
                await _notificationRepository.CreateAsync(new Notification
                {
                    UserId = previous.AssignedUserId!.Value,
                    TaskId = previous.Id,
                    Type = NotificationType.TaskUnassigned,
                    CreatedBy = userContext.UserId,
                });
            }
        }
        else if (isBeingReassigned) // If reassigned then notify both users
        {
            if (wasNotAssignedToCurrentUser)
            {
                // Previous: Unassigned
                await _notificationRepository.CreateAsync(new Notification
                {
                    UserId = previous.AssignedUserId!.Value,
                    TaskId = previous.Id,
                    Type = NotificationType.TaskUnassigned,
                    CreatedBy = userContext.UserId,
                });
            }

            var notBeingAssignedToCurrentUser = request.AssignedUserId != userContext.UserId;
            if (notBeingAssignedToCurrentUser)
            {
                // Current: Assigned
                await _notificationRepository.CreateAsync(new Notification
                {
                    UserId = request.AssignedUserId!.Value,
                    TaskId = previous.Id,
                    Type = NotificationType.TaskAssigned,
                    CreatedBy = userContext.UserId,
                });
            }
        }
        else if (wasNotAssignedToCurrentUser)
        {
            // Previous Updated
            await _notificationRepository.CreateAsync(new Notification
            {
                UserId = previous.AssignedUserId!.Value,
                TaskId = previous.Id,
                Type = NotificationType.TaskUpdated,
                CreatedBy = userContext.UserId,
            });
        }
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