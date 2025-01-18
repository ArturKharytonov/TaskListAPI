namespace TaskShare.API.Requests;

public record ShareTaskListRequest
{
    public required int UserId { get; init; }
    public required int UserIdToShare { get; init; }
}