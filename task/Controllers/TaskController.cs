using Microsoft.AspNetCore.Mvc;
using task.Models;
using task.Services;
using Task = task.Models.Task;
using task.DTOs;

namespace task.Controllers
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
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
    {
      if (dto == null || string.IsNullOrEmpty(dto.Name))
      {
        return BadRequest("Invalid task data.");
      }

      var task = new Task
      {
        Name = dto.Name,
        EstimatedHours = dto.EstimatedHours,
        UserId = dto.UserId,
        ProjectId = dto.ProjectId
      };

      var createdTask = await _taskService.CreateTaskAsync(task);
      return CreatedAtAction(nameof(CreateTask), new { id = createdTask.Id }, createdTask);
    }
  }
}