using TaskShare.Application.DTO;
using TaskShare.Application.Exceptions;
using TaskShare.Application.Services.Interfaces;

namespace TaskShare.Application.Services;

public class TaskListValidationService : ITaskListValidationService
{
    private readonly ITaskListService _taskListService;

    public TaskListValidationService(ITaskListService taskListService)
    {
        _taskListService = taskListService;
    }

    public async Task<TaskListDto> ValidateUserAccessAsync(string taskListId, int userId)
    {
        var taskList = await _taskListService.GetTaskListAsync(taskListId);

        if (taskList == null)
        {
            throw new TaskListNotFoundException(taskListId);
        }

        if (taskList.OwnerId != userId && !taskList.SharedWithUserIds.Contains(userId))
        {
            throw new UnauthorizedTaskListAccessException(userId, taskListId);
        }

        return taskList;
    }
}