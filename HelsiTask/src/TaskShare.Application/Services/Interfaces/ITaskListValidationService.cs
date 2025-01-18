using TaskShare.Application.DTO;

namespace TaskShare.Application.Services.Interfaces;

public interface ITaskListValidationService
{
    Task<TaskListDto> ValidateUserAccessAsync(string taskListId, int userId);
}