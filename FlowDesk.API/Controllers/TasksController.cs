using FlowDesk.API.DTOs.Tasks;
using FlowDesk.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlowDesk.API.Controllers;

[ApiController]
[Route("api/projects/{projectId}/tasks")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IProjectService _projectService;

    public TasksController(
        ITaskService taskService,
        IProjectService projectService)
    {
        _taskService = taskService;
        _projectService = projectService;
    }

    private Guid GetUserId()
    {
        return Guid.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(
        Guid projectId,
        CreateTaskDto dto)
    {
        var userId = GetUserId();

        var ownsProject = await _projectService
            .UserOwnsProjectAsync(projectId, userId);

        if (!ownsProject)
        {
            return Forbid();
        }

        var task = await _taskService.CreateTaskAsync(projectId, dto);

        return Ok(task);
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks(Guid projectId)
    {
        var userId = GetUserId();

        var ownsProject = await _projectService
            .UserOwnsProjectAsync(projectId, userId);

        if (!ownsProject)
        {
            return Forbid();
        }

        var tasks = await _taskService.GetTasksByProjectAsync(projectId);

        return Ok(tasks);
    }

    [HttpPut("{taskId}/status")]
public async Task<IActionResult> UpdateTaskStatus(
    Guid projectId,
    Guid taskId,
    UpdateTaskStatusDto dto)
{
    var userId = GetUserId();

    var ownsProject = await _projectService
        .UserOwnsProjectAsync(projectId, userId);

    if (!ownsProject)
    {
        return Forbid();
    }

    var updatedTask = await _taskService
        .UpdateTaskStatusAsync(taskId, dto);

    return Ok(updatedTask);
}

[HttpPut("{taskId}/assign")]
public async Task<IActionResult> AssignTask(
    Guid projectId,
    Guid taskId,
    AssignTaskDto dto)
{
    var userId = GetUserId();

    var ownsProject = await _projectService
        .UserOwnsProjectAsync(projectId, userId);

    if (!ownsProject)
    {
        return Forbid();
    }

    var updatedTask = await _taskService
        .AssignTaskAsync(taskId, dto);

    return Ok(updatedTask);
}
}