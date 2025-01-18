using TaskShare.Core.Entities;

namespace TaskShare.Core.Repositories;
public interface ITaskListRepository
{
    Task<IEnumerable<TaskList>> GetAllAsync();
    Task<IEnumerable<TaskList>> GetPagedAsync(int page, int pageSize, int userId);
    Task<TaskList?> GetByIdAsync(string id);
    Task CreateAsync(TaskList taskList);
    Task UpdateAsync(string id, TaskList updatedTaskList);
    Task DeleteAsync(string id);
}