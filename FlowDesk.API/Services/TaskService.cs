using FlowDesk.API.Data;
using FlowDesk.API.DTOs.Tasks;
using FlowDesk.API.Interfaces;
using FlowDesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.API.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskResponseDto> CreateTaskAsync(Guid projectId, CreateTaskDto dto)
    {
        var projectExists = await _context.Projects
            .AnyAsync(project => project.Id == projectId);

        if (!projectExists)
        {
            throw new Exception("Project not found.");
        }

        var taskItem = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            ProjectId = projectId,
            CreatedAt = DateTime.UtcNow
        };

        _context.TaskItems.Add(taskItem);

        await _context.SaveChangesAsync();

        return new TaskResponseDto
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            Description = taskItem.Description,
            Status = taskItem.Status,
            Priority = taskItem.Priority,
            DueDate = taskItem.DueDate,
            ProjectId = taskItem.ProjectId,
            AssignedUserId = taskItem.AssignedUserId,
            CreatedAt = taskItem.CreatedAt
        };
    }

    public async Task<List<TaskResponseDto>> GetTasksByProjectAsync(Guid projectId)
    {
        return await _context.TaskItems
            .Where(task => task.ProjectId == projectId)
            .Select(task => new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate,
                ProjectId = task.ProjectId,
                AssignedUserId = task.AssignedUserId,
                CreatedAt = task.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<TaskResponseDto> UpdateTaskStatusAsync(
        Guid taskId,
        UpdateTaskStatusDto dto)
    {
        var task = await _context.TaskItems
            .FirstOrDefaultAsync(t => t.Id == taskId);

        if (task is null)
        {
            throw new Exception("Task not found.");
        }

        task.Status = dto.Status;

        await _context.SaveChangesAsync();

        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            DueDate = task.DueDate,
            ProjectId = task.ProjectId,
            AssignedUserId = task.AssignedUserId,
            CreatedAt = task.CreatedAt
        };
    }
    public async Task<TaskResponseDto> AssignTaskAsync(
    Guid taskId,
    AssignTaskDto dto)
{
    var task = await _context.TaskItems
        .FirstOrDefaultAsync(t => t.Id == taskId);

    if (task is null)
    {
        throw new Exception("Task not found.");
    }

    var userExists = await _context.Users
        .AnyAsync(user => user.Id == dto.UserId);

    if (!userExists)
    {
        throw new Exception("User not found.");
    }

    task.AssignedUserId = dto.UserId;

    await _context.SaveChangesAsync();

    return new TaskResponseDto
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        Status = task.Status,
        Priority = task.Priority,
        DueDate = task.DueDate,
        ProjectId = task.ProjectId,
        AssignedUserId = task.AssignedUserId,
        CreatedAt = task.CreatedAt
    };
}
}