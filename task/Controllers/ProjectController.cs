using Microsoft.AspNetCore.Mvc;
using task.Models;
using task.Services;
using task.DTOs;

namespace task.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class ProjectController : ControllerBase
  {
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
      _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
    {
      if (dto == null || string.IsNullOrEmpty(dto.Name))
      {
        return BadRequest("Invalid Project data.");
      }

      var project = new Project
      {
        Name = dto.Name,
        ClockifyId = dto.ClockifyId
      };

      var createdProject = await _projectService.CreateProjectAsync(project);
      return CreatedAtAction(nameof(CreateProject), new { id = createdProject.Id }, createdProject);
    }
  }
}