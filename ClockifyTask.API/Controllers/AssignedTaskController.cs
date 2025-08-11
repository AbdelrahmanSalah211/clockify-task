using Microsoft.AspNetCore.Mvc;
using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;

namespace ClockifyTask.API.Controllers
{
    [ApiController]
    [Route("api/assignedTasks")]
    public class AssignedTaskController : ControllerBase
    {
        private readonly IAssignedTaskService _assignedTaskService;

        public AssignedTaskController(IAssignedTaskService assignedTaskService)
        {
            _assignedTaskService = assignedTaskService;
        }

        [HttpPost("projects/{projectId:int}/users/{userId:int}")]
        public async Task<ActionResult<AssignedTaskDto>> Create(int projectId, int userId, CreateAssignedTaskDto assignedTaskDto)
        {
            if (assignedTaskDto == null || string.IsNullOrEmpty(assignedTaskDto.Name))
            {
                return BadRequest("Invalid task data.");
            }

            var createdTask = await _assignedTaskService.CreateAsync(projectId, userId, assignedTaskDto);
            return CreatedAtAction(nameof(Create), new { id = createdTask.Id }, createdTask);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignedTaskDto>>> GetAll()
        {
            var tasks = await _assignedTaskService.GetAllAsync();
            return Ok(tasks);
        }
    }
}
