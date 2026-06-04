using FlowDesk.API.DTOs.Tasks;

namespace FlowDesk.API.Interfaces;

public interface ITaskService
{
    Task<TaskResponseDto> CreateTaskAsync(Guid projectId, CreateTaskDto dto);

    Task<List<TaskResponseDto>> GetTasksByProjectAsync(Guid projectId);
    Task<TaskResponseDto> UpdateTaskStatusAsync(
    Guid taskId,
    UpdateTaskStatusDto dto);
    Task<TaskResponseDto> AssignTaskAsync(
    Guid taskId,
    AssignTaskDto dto);
}