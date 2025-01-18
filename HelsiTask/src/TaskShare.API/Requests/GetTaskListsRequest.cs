namespace TaskShare.API.Requests;

public record GetTaskListsRequest
{
    public required int UserId { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}