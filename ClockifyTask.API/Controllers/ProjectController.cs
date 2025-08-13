using Microsoft.AspNetCore.Mvc;
using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ClockifyTask.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IAssignedTaskService _assignedTaskService;

        public ProjectController(IProjectService projectService, IAssignedTaskService assignedTaskService)
        {
            _projectService = projectService;
            _assignedTaskService = assignedTaskService;
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

        [HttpPost("{projectId:int}/assignedTasks")]
        public async Task<ActionResult<AssignedTaskDto>> Create(int projectId, CreateAssignedTaskDto assignedTaskDto)
        {
            if (assignedTaskDto == null || string.IsNullOrEmpty(assignedTaskDto.Name))
            {
                return BadRequest("Invalid task data.");
            }

            var createdTask = await _assignedTaskService.CreateAsync(projectId, assignedTaskDto);
            return CreatedAtAction(nameof(Create), new { id = createdTask.Id }, createdTask);
        }
    }
}
