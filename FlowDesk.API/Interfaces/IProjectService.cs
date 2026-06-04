using FlowDesk.API.DTOs.Projects;

namespace FlowDesk.API.Interfaces;

public interface IProjectService
{
    Task<ProjectResponseDto> CreateProjectAsync(CreateProjectDto dto, Guid userId);

    Task<List<ProjectResponseDto>> GetUserProjectsAsync(Guid userId);

    Task<bool> UserOwnsProjectAsync(Guid projectId, Guid userId);
}