using System.ComponentModel.DataAnnotations;

namespace TaskShare.Application.DTO;

public class TaskListDto
{
    public string Id { get; set; } = null!;

    [StringLength(255, MinimumLength = 1, ErrorMessage = "The Name field must be between 1 and 255 characters.")]
    public string Name { get; set; } = null!;

    public int OwnerId { get; set; }

    public List<int> SharedWithUserIds { get; set; } = [];

    public DateTime CreationDate { get; set; }
}