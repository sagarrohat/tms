using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace Service;

public static class TaskEndpoints
{
    public static async Task<Guid> CreateTask(HttpContext httpContext,
        [FromServices] ITaskCreateCommand command, [FromBody] TaskCreateRequest request)
    {
        return await command.ExecuteAsync(httpContext.GetUserContext(), request);
    }

    public static async Task DeleteTaskById(HttpContext httpContext,
        [FromServices] ITaskDeleteCommand command, Guid id)
    {
        await command.ExecuteAsync(id);
    }

    public static async Task<TaskResponse> GetTaskById(HttpContext httpContext,
        [FromServices] ITaskByIdQuery query, Guid id)
    {
        return await query.ExecuteAsync(httpContext.GetUserContext(), id);
    }

    public static async Task UpdateTask(HttpContext httpContext,
        [FromServices] ITaskUpdateCommand command, [FromBody] TaskUpdateRequest request, Guid id)
    {
        request.Id = id;
        await command.ExecuteAsync(httpContext.GetUserContext(), request);
    }
    
    public static async Task<ICollection<TaskResponse>> GetTasksByFilters(HttpContext httpContext,
        [FromServices] ITasksByFiltersQuery query,
        string? assignedUserId, DateTime? from, DateTime? to, string? keyword)
    {
        return await query.ExecuteAsync(httpContext.GetUserContext(), assignedUserId, from, to, keyword);
    }
    
    public static async Task PinTask(HttpContext httpContext,
        [FromServices] ITaskPinCommand command, Guid id)
    {
        await command.ExecuteAsync(httpContext.GetUserContext(), id);
    }
    
    public static async Task UnpinTask(HttpContext httpContext,
        [FromServices] ITaskUnpinCommand command, Guid id)
    { 
        await command.ExecuteAsync(httpContext.GetUserContext(), id);
    }
}