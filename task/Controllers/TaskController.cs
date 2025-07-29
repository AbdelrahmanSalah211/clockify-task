using Microsoft.AspNetCore.Mvc;
using task.Models;
using task.Services;
using Task = task.Models.Task;

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
    public async Task<IActionResult> CreateTask([FromBody] Task task)
    {
      if (task == null || string.IsNullOrEmpty(task.Name))
      {
        return BadRequest("Invalid task data.");
      }

      var createdTask = await _taskService.CreateTaskAsync(task);
      return CreatedAtAction(nameof(CreateTask), new { id = createdTask.Id }, createdTask);
    }
  }
}