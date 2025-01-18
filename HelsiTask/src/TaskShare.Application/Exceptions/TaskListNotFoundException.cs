namespace TaskShare.Application.Exceptions;
public class TaskListNotFoundException(string taskListId) : Exception($"Task list with ID {taskListId} was not found");
