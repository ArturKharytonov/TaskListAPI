namespace TaskShare.API.Requests;

public record CreateTaskListRequest
{
    public required string Name { get; init; }
    public required int OwnerId { get; init; }
}