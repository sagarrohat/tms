using Domain;
using ExternalInfrastructure;
using Newtonsoft.Json;
using Persistence;
using Task = System.Threading.Tasks.Task;

namespace Application;

public class TaskNotificationProcessor : INotificationProcessor
{
    private readonly INotificationService _notificationService;
    private readonly IMessagingConfigurationRepository _messagingConfigurationRepository;
    private readonly IUserRepository _userRepository;

    public TaskNotificationProcessor(INotificationService notificationService, IRepositoryFactory repositoryFactory)
    {
        _notificationService = notificationService;
        _messagingConfigurationRepository = repositoryFactory.MessagingConfigurationRepository;
        _userRepository = repositoryFactory.UserRepository;
    }

    public async Task ProcessAsync(NotificationMessage notificationMessage)
    {
        if (notificationMessage.ActionType == ActionType.Added)
        {
            await ProcessTaskCreateNotificationAsync(notificationMessage);
        }
        else
        {
            await ProcessTaskUpdateNotificationAsync(notificationMessage);
        }
    }

    private async Task ProcessTaskUpdateNotificationAsync(NotificationMessage notificationMessage)
    {
        var taskUpdateNotification = JsonConvert.DeserializeObject<TaskUpdateNotification>(notificationMessage.Data);

        var previous = taskUpdateNotification.Previous;
        var userContext = notificationMessage.UserContext;
        var request = taskUpdateNotification.Current;

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
                await NotifyUserAsync(userContext.UserId, request.AssignedUserId!.Value, "Assigned", previous.Id);
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
                await NotifyUserAsync(userContext.UserId, previous.AssignedUserId!.Value, "Unassigned", previous.Id);
            }
        }
        else if (isBeingReassigned) // If reassigned then notify both users
        {
            if (wasNotAssignedToCurrentUser)
            {
                await NotifyUserAsync(userContext.UserId, previous.AssignedUserId!.Value, "Unassigned", previous.Id);
            }

            var notBeingAssignedToCurrentUser = request.AssignedUserId != userContext.UserId;
            if (notBeingAssignedToCurrentUser)
            {
                await NotifyUserAsync(userContext.UserId, request.AssignedUserId!.Value, "Assigned", previous.Id);
            }
        }
        else if (wasNotAssignedToCurrentUser)
        {
            await NotifyUserAsync(userContext.UserId, previous.AssignedUserId!.Value, "Updated", previous.Id);
        }
    }

    private async Task ProcessTaskCreateNotificationAsync(NotificationMessage notificationMessage)
    {
        var taskCreateNotification = JsonConvert.DeserializeObject<TaskCreateNotification>(notificationMessage.Data);

        var request = taskCreateNotification.Current;
        var userContext = notificationMessage.UserContext;
        var taskId = taskCreateNotification.Id;

        var isBeingAssigned = request.AssignedUserId.HasValue && request.AssignedUserId.Value != Guid.Empty;
        if (isBeingAssigned)
        {
            var notToCurrentUser = request.AssignedUserId!.Value != userContext.UserId;
            if (notToCurrentUser)
            {
                await NotifyUserAsync(userContext.UserId, request.AssignedUserId!.Value, "Assigned", taskId);
            }
        }
    }


    private async Task NotifyUserAsync(Guid senderId, Guid receiverId, string action, Guid taskId)
    {
        var userToken = await _messagingConfigurationRepository.GetAsync(receiverId);

        if (string.IsNullOrEmpty(userToken))
            return;

        var sender = await _userRepository.GetByIdAsync(senderId);
        var receiver = await _userRepository.GetByIdAsync(receiverId);

        var body = action switch
        {
            "Assigned" => $"Hi {receiver.FirstName}, {GetFullName(sender)} assigned a task with ID: {taskId} to you.",
            "Updated" => $"Hi {receiver.FirstName}, {GetFullName(sender)} updated your task with ID: {taskId}.",
            _ => $"Hi {receiver.FirstName}, {GetFullName(sender)} changed assignee of a task with ID: {taskId}."
        };

        await _notificationService.SendAsync(userToken, $"Task {action}", body);

        string GetFullName(User user)
        {
            return $"{user.FirstName} {user.LastName}";
        }
    }
}