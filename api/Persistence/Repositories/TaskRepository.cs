using Common;
using Domain;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
using TaskStatus = Domain.TaskStatus;

namespace Persistence
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DatabaseContext _dbContext;

        public TaskRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateAsync(UserContext userContext, TaskCreateRequest request)
        {
            var utcNow = DateTime.UtcNow;

            var task = new Domain.Task()
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Status = TaskStatus.New,
                Priority = request.Priority,
                CreatedOnUtc = utcNow,
                ModifiedOnUtc = utcNow,
                AssignedUserId = request.AssignedUserId,
                AssignedOnUtc = request.AssignedUserId.HasValue ? utcNow : null,
                CreatedBy = userContext.UserId,
                ModifiedBy = userContext.UserId,
                DueDate = request.DueDate,
                PercentageCompleted = request.PercentageCompleted,
                IsDeleted = false,
            };
            
            _dbContext.Add(task);
            await _dbContext.SaveChangesAsync();
            
            return task.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _dbContext.Tasks.SingleOrDefaultAsync(x => x.Id == id);

            if (task != null)
            {
                task.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<TaskResponse?> GetAsync(UserContext userContext, Guid id)
        {
            var isPinned = await _dbContext.PinnedTasks
                .AnyAsync(x => x.TaskId == id && x.UserId == userContext.UserId);
            
            return await _dbContext.Tasks
                .Where(x => x.Id == id)
                .Include(x=>x.AssignedUser)
                .Select(x=>new TaskResponse()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Status = x.Status,
                    Priority = x.Priority,
                    AssignedUserId = x.AssignedUserId,
                    AssignedUserFirstName = x.AssignedUser!.FirstName,
                    AssignedUserLastName = x.AssignedUser!.LastName,
                    DueDate = x.DueDate,
                    PercentageCompleted = x.PercentageCompleted,
                    IsOverDue = IsOverDue(x),
                    IsPinned = isPinned
                })
                .SingleOrDefaultAsync();
        }

        public async Task<List<TaskResponse>> GetAllAsync(UserContext userContext, string? assignedUserId,
            DateTime? from, DateTime? to, string? keyword)
        {
            var pinnedTaskIds = await _dbContext.PinnedTasks
                .Where(x => x.UserId == userContext.UserId)
                .Select(x => x.TaskId)
                .ToListAsync();
            
            var tasksQuery = _dbContext.Tasks.Where(x => x.IsDeleted == false && x.Status == TaskStatus.New);

            if (string.IsNullOrEmpty(assignedUserId))
            {
                tasksQuery = tasksQuery.Where(x => !x.AssignedUserId.HasValue);
            }
            else if (assignedUserId != "*")
            {
                tasksQuery = tasksQuery.Where(x =>
                    x.AssignedUserId.HasValue && x.AssignedUserId.Value == new Guid(assignedUserId));
            }

            if (from.HasValue && to.HasValue)
            {
                tasksQuery = tasksQuery.Where(x =>
                    x.DueDate >= from.Value.ToUniversalTime() && x.DueDate <= to.Value.ToUniversalTime());
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                tasksQuery = tasksQuery.Where(x =>
                    (x.Title.Contains(keyword) || (x.Description != null && x.Description.Contains(keyword))));
            }

            return await tasksQuery
                .Select(x => new TaskResponse()
                {
                    Id = x.Id,
                    DueDate = x.DueDate,
                    Title = x.Title,
                    Description = x.Description,
                    Status = x.Status,
                    Priority = x.Priority,
                    AssignedUserId = x.AssignedUserId,
                    PercentageCompleted = x.PercentageCompleted,
                    AssignedUserFirstName = x.AssignedUser!.FirstName,
                    AssignedUserLastName = x.AssignedUser!.LastName,
                    IsPinned = pinnedTaskIds.Contains(x.Id),
                    IsOverDue = IsOverDue(x)
                })
                .OrderByDescending(t => t.IsPinned)
                .ThenBy(t => t.DueDate)
                .ToListAsync();
        }

        private static bool IsOverDue(Domain.Task x)
        {
            return x.DueDate.Date < DateTime.UtcNow.Date;
        }
        
        public async Task UpdateAsync(UserContext userContext, TaskResponse previous, TaskUpdateRequest request)
        {
            var utcNow = DateTime.UtcNow;
            
            var currentTask = await _dbContext.Tasks.SingleAsync(x=>x.Id == previous.Id);
            currentTask.Title = request.Title;
            currentTask.Description = request.Description;
            
            switch (request.Status)
            {
                case TaskStatus.Completed:
                    currentTask.Status = TaskStatus.Completed;
                    currentTask.CompletedOnUtc = utcNow;
                    break;
                case TaskStatus.Cancelled:
                    currentTask.Status = TaskStatus.Cancelled;
                    currentTask.CancelledOnUtc = utcNow;
                    break;
            }
            
            currentTask.Priority = request.Priority;

            currentTask.AssignedUserId = request.AssignedUserId;
            if (request.AssignedUserId.HasValue && request.AssignedUserId != previous.AssignedUserId)
            {
                currentTask.AssignedOnUtc = utcNow;
            }
            else
            {
                currentTask.AssignedOnUtc = null;
            }

            currentTask.DueDate = request.DueDate;
            currentTask.PercentageCompleted = request.PercentageCompleted;
            currentTask.ModifiedOnUtc = utcNow;
            currentTask.ModifiedBy = userContext.UserId;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<DashboardResponse> GetDashboardItemsAsync(string? assignedUserId)
        {
            var userQuery = _dbContext.Tasks.Where(x=>x.IsDeleted == false);
            if(string.IsNullOrEmpty(assignedUserId))
            {
                userQuery = userQuery.Where(x => x.AssignedUserId == null);
            }
            else if (assignedUserId != "*")
            {
                userQuery = userQuery.Where(x => x.AssignedUserId == new Guid(assignedUserId));
            }

            var utcNow = DateTime.UtcNow.Date;

            return await (
                from t in _dbContext.Tasks
                let lowCompleted =
                    userQuery.Count(x => x.Priority == TaskPriority.Low && x.Status == TaskStatus.Completed)
                let lowCancelled =
                    userQuery.Count(x => x.Priority == TaskPriority.Low && x.Status == TaskStatus.Cancelled)
                let lowOverDue =
                    userQuery.Count(x => x.Priority == TaskPriority.Low && x.DueDate < utcNow && x.Status == TaskStatus.New)
                let normalCompleted =
                    userQuery.Count(x => x.Priority == TaskPriority.Normal && x.Status == TaskStatus.Completed)
                let normalCancelled =
                    userQuery.Count(x => x.Priority == TaskPriority.Normal && x.Status == TaskStatus.Cancelled)
                let normalOverDue =
                    userQuery.Count(x => x.Priority == TaskPriority.Normal && x.DueDate < utcNow && x.Status == TaskStatus.New)
                let highCompleted =
                    userQuery.Count(x => x.Priority == TaskPriority.High && x.Status == TaskStatus.Completed)
                let highCancelled =
                    userQuery.Count(x => x.Priority == TaskPriority.High && x.Status == TaskStatus.Cancelled)
                let highOverDue =
                    userQuery.Count(x => x.Priority == TaskPriority.High && x.DueDate < utcNow && x.Status == TaskStatus.New)
                let completedCount =
                    userQuery.Count(x => x.Status == TaskStatus.Completed)
                let cancelledCount =
                    userQuery.Count(x => x.Status == TaskStatus.Cancelled)
                let overDueCount =
                    userQuery.Count(x => x.DueDate < utcNow && x.Status == TaskStatus.New)
                select new DashboardResponse()
                {
                    LowCompletedCount = lowCompleted,
                    LowCancelledCount = lowCancelled,
                    LowOverDueCount = lowOverDue,
                    NormalCompletedCount = normalCompleted,
                    NormalCancelledCount = normalCancelled,
                    NormalOverDueCount = normalOverDue,
                    HighCompletedCount = highCompleted,
                    HighCancelledCount = highCancelled,
                    HighOverDueCount = highOverDue,
                    CompletedCount = completedCount,
                    CancelledCount = cancelledCount,
                    OverDueCount = overDueCount
                }).FirstOrDefaultAsync() ?? new DashboardResponse();
        }
    }
}