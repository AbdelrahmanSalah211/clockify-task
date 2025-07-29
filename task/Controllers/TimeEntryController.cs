using Microsoft.AspNetCore.Mvc;
using task.Models;
using task.Services;

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
    public async Task<IActionResult> CreateTimeEntry([FromBody] TimeEntry timeEntry)
    {
      if (timeEntry == null || string.IsNullOrEmpty(timeEntry.User.Name))
      {
        return BadRequest("Invalid time entry data.");
      }

      var createdTimeEntry = await _timeEntryService.CreateTimeEntryAsync(timeEntry);
      return CreatedAtAction(nameof(CreateTimeEntry), new { id = createdTimeEntry.Id }, createdTimeEntry);
    }
  }
}