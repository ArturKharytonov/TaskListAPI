namespace TaskShare.API.Requests;

public record UnshareTaskListRequest
{
    public required int UserId { get; init; }
    public required int UserIdToUnshare { get; init; }
}