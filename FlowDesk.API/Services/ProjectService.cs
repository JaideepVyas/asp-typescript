using FlowDesk.API.Data;
using FlowDesk.API.DTOs.Projects;
using FlowDesk.API.Interfaces;
using FlowDesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.API.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectResponseDto> CreateProjectAsync(CreateProjectDto dto, Guid userId)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            OwnerId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Projects.Add(project);

        await _context.SaveChangesAsync();

        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            OwnerId = project.OwnerId,
            CreatedAt = project.CreatedAt
        };
    }

    public async Task<List<ProjectResponseDto>> GetUserProjectsAsync(Guid userId)
    {
        return await _context.Projects
            .Where(p => p.OwnerId == userId)
            .Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                OwnerId = p.OwnerId,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<bool> UserOwnsProjectAsync(Guid projectId, Guid userId)
    {
        return await _context.Projects
            .AnyAsync(project =>
                project.Id == projectId &&
                project.OwnerId == userId);
    }
}