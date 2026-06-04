using FlowDesk.API.Models.Enums;

namespace FlowDesk.API.Models;

public class TaskItem
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public FlowDesk.API.Models.Enums.TaskStatus Status { get; set; } = FlowDesk.API.Models.Enums.TaskStatus.Todo;

    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    public DateTime? DueDate { get; set; }

    public Guid ProjectId { get; set; }

    public Guid? AssignedUserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}