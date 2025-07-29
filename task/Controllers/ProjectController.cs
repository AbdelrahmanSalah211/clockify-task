using Microsoft.AspNetCore.Mvc;
using task.Models;
using task.Services;

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
    public async Task<IActionResult> CreateProject([FromBody] Project project)
    {
      if (project == null || string.IsNullOrEmpty(project.Name))
      {
        return BadRequest("Invalid Project data.");
      }

      var createdProject = await _projectService.CreateProjectAsync(project);
      return CreatedAtAction(nameof(CreateProject), new { id = createdProject.Id }, createdProject);
    }
  }
}