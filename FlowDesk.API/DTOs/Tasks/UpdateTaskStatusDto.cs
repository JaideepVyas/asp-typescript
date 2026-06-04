using TaskStatusEnum = FlowDesk.API.Models.Enums.TaskStatus;

namespace FlowDesk.API.DTOs.Tasks;

public class UpdateTaskStatusDto
{
    public TaskStatusEnum Status { get; set; }
}