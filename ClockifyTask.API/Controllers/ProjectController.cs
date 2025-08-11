using Microsoft.AspNetCore.Mvc;
using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;

namespace ClockifyTask.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Create(CreateProjectDto projectDto)
        {
            if (projectDto == null || string.IsNullOrEmpty(projectDto.Name))
            {
                return BadRequest("Invalid Project data.");
            }

            var createdProject = await _projectService.CreateProjectAsync(projectDto);
            return CreatedAtAction(nameof(Create), new { id = createdProject.Id }, createdProject);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }
    }
}
