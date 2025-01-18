using MongoDB.Driver;
using TaskShare.Core.Entities;
using TaskShare.Core.Repositories;
using TaskShare.Infrastructure.Data;

namespace TaskShare.Infrastructure.Repositories;

public class TaskListRepository : ITaskListRepository
{
    private readonly TaskShareDbContext _dbContext;
    public TaskListRepository(TaskShareDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TaskList>> GetPagedAsync(int page, int pageSize, int userId)
    {
        return await _dbContext.TaskLists
            .Find(taskList => taskList.OwnerId == userId || taskList.SharedWithUserIds.Contains(userId))
            .SortByDescending(taskList => taskList.CreationDate)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize) 
            .ToListAsync();
    }
    public async Task<IEnumerable<TaskList>> GetAllAsync()
    {
        return await _dbContext.TaskLists.Find(taskList => true).ToListAsync();
    }

    public async Task<TaskList?> GetByIdAsync(string id)
    {
        return await _dbContext.TaskLists.Find(taskList => taskList.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(TaskList taskList)
    {
        await _dbContext.TaskLists.InsertOneAsync(taskList);
    }

    public async Task UpdateAsync(string id, TaskList updatedTaskList)
    {
        await _dbContext.TaskLists.ReplaceOneAsync(taskList => taskList.Id == id, updatedTaskList);
    }

    public async Task DeleteAsync(string id)
    {
        await _dbContext.TaskLists.DeleteOneAsync(taskList => taskList.Id == id);
    }

}