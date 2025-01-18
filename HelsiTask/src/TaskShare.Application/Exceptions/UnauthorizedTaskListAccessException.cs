namespace TaskShare.Application.Exceptions;
public class UnauthorizedTaskListAccessException(int userId, string taskListId)
    : Exception($"User {userId} does not have access to task list {taskListId}");