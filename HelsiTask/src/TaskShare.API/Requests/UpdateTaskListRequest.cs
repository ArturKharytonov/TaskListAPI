namespace TaskShare.API.Requests;
public record UpdateTaskListRequest
{
    public required string Name { get; init; }
    public required int UserId { get; init; }
}