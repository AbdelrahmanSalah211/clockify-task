using Microsoft.AspNetCore.Mvc;
using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;

namespace ClockifyTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> Create(CreateTaskDto taskDto)
        {
            if (taskDto == null || string.IsNullOrEmpty(taskDto.Name))
            {
                return BadRequest("Invalid task data.");
            }

            var createdTask = await _taskService.CreateAsync(taskDto);
            return CreatedAtAction(nameof(Create), new { id = createdTask.Id }, createdTask);
        }
    }
}
