using FlowDesk.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FlowDesk.API.DTOs.Tasks;

public class CreateTaskDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    public DateTime? DueDate { get; set; }
}