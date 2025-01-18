using TaskShare.Application.DTO;

namespace TaskShare.Application.Services.Interfaces;

public interface ITaskListService
{
    Task CreateTaskListAsync(TaskListDto taskList);
    Task UpdateTaskListAsync(string id, TaskListDto updatedTaskList);
    Task DeleteTaskList(string taskListId);
    Task<TaskListDto?> GetTaskListAsync(string taskListId);
    Task<IReadOnlyCollection<TaskListDto>> GetTaskListsAsync(int page, int pageSize, int userId);
    Task<bool> ShareTaskListAsync(string taskListId, int userId);
    Task<IEnumerable<int>> GetSharedUsersAsync(string taskListId);
    Task<bool> UnshareTaskListAsync(string id, int userId);
}