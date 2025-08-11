using Microsoft.AspNetCore.Mvc;
using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;

namespace ClockifyTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignedTaskController : ControllerBase
    {
        private readonly IAssignedTaskService _assignedTaskService;

        public AssignedTaskController(IAssignedTaskService assignedTaskService)
        {
            _assignedTaskService = assignedTaskService;
        }

        [HttpPost]
        public async Task<ActionResult<AssignedTaskDto>> Create(CreateAssignedTaskDto assignedTaskDto)
        {
            if (assignedTaskDto == null || string.IsNullOrEmpty(assignedTaskDto.Name))
            {
                return BadRequest("Invalid task data.");
            }

            var createdTask = await _assignedTaskService.CreateAsync(assignedTaskDto);
            return CreatedAtAction(nameof(Create), new { id = createdTask.Id }, createdTask);
        }
    }
}
