using Microsoft.AspNetCore.Mvc;
using task.Models;
using task.Services;
using task.DTOs;

namespace task.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class TimeEntryController : ControllerBase
  {
    private readonly ITimeEntryService _timeEntryService;

    public TimeEntryController(ITimeEntryService timeEntryService)
    {
      _timeEntryService = timeEntryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTimeEntry([FromBody] CreateTimeEntryDto dto)
    {
      if (dto == null)
      {
        return BadRequest("Invalid time entry data.");
      }

      var timeEntry = new TimeEntry
      {
        Start = dto.Start,
        End = dto.End,
        UserId = dto.UserId,
        TaskId = dto.TaskId,
        ProjectId = dto.ProjectId
      };

      var createdTimeEntry = await _timeEntryService.CreateTimeEntryAsync(timeEntry);
      return CreatedAtAction(nameof(CreateTimeEntry), new { id = createdTimeEntry.Id }, createdTimeEntry);
    }
  }
}