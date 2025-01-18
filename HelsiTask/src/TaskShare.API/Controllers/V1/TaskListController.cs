using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TaskShare.API.Requests;
using TaskShare.Application.DTO;
using TaskShare.Application.Exceptions;
using TaskShare.Application.Services.Interfaces;
using TaskShare.Core.Entities;

namespace TaskShare.API.Controllers.V1;

[ApiVersion(1)]
[Route("task-lists")]
public class TaskListController : ControllerBase
{
    private readonly ITaskListService _taskListService;
    private readonly ITaskListValidationService _validationService;
    public TaskListController(ITaskListService taskListService, ITaskListValidationService validationService)
    {
        _taskListService = taskListService;
        _validationService = validationService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTaskListRequest request)
    {
        var taskList = new TaskListDto
        {
            Name = request.Name,
            OwnerId = request.OwnerId,
            SharedWithUserIds = [],
            CreationDate = DateTime.UtcNow
        };

        await _taskListService.CreateTaskListAsync(taskList);
        return Ok("Task list created successfully");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateTaskListRequest request)
    {
        try
        {
            var taskList = await _validationService.ValidateUserAccessAsync(id, request.UserId);

            var taskListDto = new TaskListDto
            {
                Id = taskList.Id,
                Name = request.Name,
                OwnerId = taskList.OwnerId,
                SharedWithUserIds = taskList.SharedWithUserIds,
                CreationDate = taskList.CreationDate
            };

            await _taskListService.UpdateTaskListAsync(id, taskListDto);
            return Ok("Task list updated successfully");
        }
        catch (TaskListNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedTaskListAccessException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id, [FromQuery] int userId)
    {
        var taskList = await _taskListService.GetTaskListAsync(id);

        if (taskList is null)
            return NotFound("Task list not found");

        if (taskList.OwnerId != userId)
            return BadRequest("You are not able to delete this task list because you are not owner");

        await _taskListService.DeleteTaskList(id);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id, [FromQuery] int userId)
    {
        try
        {
            var taskList = await _validationService.ValidateUserAccessAsync(id, userId);
            return Ok(taskList);
        }
        catch (TaskListNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedTaskListAccessException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskList>>> QueryAsync([FromQuery] GetTaskListsRequest request)
    {
        var taskLists = await _taskListService.GetTaskListsAsync(request.Page, request.PageSize, request.UserId);
        return Ok(taskLists);
    }

    [HttpPost("{id}/share")]
    public async Task<IActionResult> ShareAsync(string id, [FromBody] ShareTaskListRequest request)
    {
        try
        {
            var taskList = await _validationService.ValidateUserAccessAsync(id, request.UserId);
            if (taskList!.OwnerId == request.UserIdToShare ||
                taskList.SharedWithUserIds.Contains(request.UserIdToShare))
            {
                return Ok("This user already has access to task list");
            }

            var result = await _taskListService.ShareTaskListAsync(id, request.UserIdToShare);
            if (!result) return BadRequest("Unable to share the task list");
            return Ok("Task list shared successfully");
        }
        catch (TaskListNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedTaskListAccessException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}/shared-users")]
    public async Task<IActionResult> GetSharedUsersAsync(string id, [FromQuery] int userId)
    {
        try
        {
            await _validationService.ValidateUserAccessAsync(id, userId);
            var sharedUsers = await _taskListService.GetSharedUsersAsync(id);
            if (!sharedUsers.Any())
            {
                return NotFound("No users are currently shared with this task list");
            }
            return Ok(sharedUsers);
        }
        catch (TaskListNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedTaskListAccessException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/unshare")]
    public async Task<IActionResult> UnshareAsync(string id, [FromBody] UnshareTaskListRequest request)
    {
        try
        {
            var taskList = await _validationService.ValidateUserAccessAsync(id, request.UserId);
            if (!taskList!.SharedWithUserIds.Contains(request.UserIdToUnshare))
            {
                return Ok("There is no user with this ID");
            }

            var result = await _taskListService.UnshareTaskListAsync(id, request.UserIdToUnshare);
            if (!result) return NotFound("Failed to unshare the task list");
            return Ok("Task list un-shared successfully");
        }
        catch (TaskListNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedTaskListAccessException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}