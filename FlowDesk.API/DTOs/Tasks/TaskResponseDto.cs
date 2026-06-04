using FlowDesk.API.Models.Enums;

using TaskStatusEnum = FlowDesk.API.Models.Enums.TaskStatus;

namespace FlowDesk.API.DTOs.Tasks;

public class TaskResponseDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public TaskStatusEnum Status { get; set; }

    public TaskPriority Priority { get; set; }

    public DateTime? DueDate { get; set; }

    public Guid ProjectId { get; set; }

    public Guid? AssignedUserId { get; set; }

    public DateTime CreatedAt { get; set; }
}