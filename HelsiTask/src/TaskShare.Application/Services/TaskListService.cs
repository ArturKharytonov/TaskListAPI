using AutoMapper;
using TaskShare.Application.DTO;
using TaskShare.Application.Services.Interfaces;
using TaskShare.Core.Entities;
using TaskShare.Core.Repositories;

namespace TaskShare.Application.Services;

public class TaskListService : ITaskListService
{
    private readonly ITaskListRepository _taskListRepository;
    private readonly IMapper _mapper;
    public TaskListService(IMapper mapper, ITaskListRepository repository)
    {
        _mapper = mapper;
        _taskListRepository = repository;
    }

    public async Task CreateTaskListAsync(TaskListDto taskListDto)
    {
        var taskList = _mapper.Map<TaskList>(taskListDto);
        await _taskListRepository.CreateAsync(taskList);
    }

    public async Task UpdateTaskListAsync(string id, TaskListDto updatedTaskList)
    {
        var taskList = _mapper.Map<TaskList>(updatedTaskList);
        await _taskListRepository.UpdateAsync(id, taskList);
    }

    public async Task DeleteTaskList(string taskListId)
    {
        await _taskListRepository.DeleteAsync(taskListId);
    }

    public async Task<TaskListDto?> GetTaskListAsync(string taskListId)
    {
        var taskList = await _taskListRepository.GetByIdAsync(taskListId);
        return _mapper.Map<TaskListDto>(taskList);
    }

    public async Task<IReadOnlyCollection<TaskListDto>> GetTaskListsAsync(int page, int pageSize, int userId)
    {
        var taskLists = await _taskListRepository.GetPagedAsync(page, pageSize, userId);
        return _mapper.Map<IReadOnlyCollection<TaskListDto>>(taskLists);
    }

    public async Task<bool> ShareTaskListAsync(string taskListId, int userId)
    {
        var taskList = await _taskListRepository.GetByIdAsync(taskListId);

        if (taskList == null || taskList.SharedWithUserIds.Contains(userId))
            return false;

        taskList.SharedWithUserIds.Add(userId);
        await _taskListRepository.UpdateAsync(taskListId, taskList);
        return true;
    }

    public async Task<IEnumerable<int>> GetSharedUsersAsync(string taskListId)
    {
        var taskList = await _taskListRepository.GetByIdAsync(taskListId);
        return taskList?.SharedWithUserIds ?? [];
    }

    public async Task<bool> UnshareTaskListAsync(string id, int userId)
    {
        var taskList = await _taskListRepository.GetByIdAsync(id);

        if (taskList == null || !taskList.SharedWithUserIds.Remove(userId))
            return false;

        await _taskListRepository.UpdateAsync(id, taskList);
        return true;
    }
}