using System.ComponentModel.DataAnnotations;

namespace FlowDesk.API.DTOs.Projects;

public class CreateProjectDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
}