using FlowDesk.API.DTOs.Projects;
using FlowDesk.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlowDesk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectDto dto)
    {
        var userId = GetUserId();

        var project = await _projectService.CreateProjectAsync(dto, userId);

        return Ok(project);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserProjects()
    {
        var userId = GetUserId();

        var projects = await _projectService.GetUserProjectsAsync(userId);

        return Ok(projects);
    }
}