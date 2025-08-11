using Microsoft.AspNetCore.Mvc;
using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;

namespace ClockifyTask.API.Controllers
{
    [ApiController]
    [Route("api/timeEntries")]
    public class TimeEntriesController : ControllerBase
    {
        private readonly ITimeEntryService _timeEntryService;

        public TimeEntriesController(ITimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }

        [HttpPost("projects/{projectId:int}/assignedTasks/{assignedTaskId:int}/users/{userId:int}")]
        public async Task<ActionResult<TimeEntryDto>> Create(int projectId, int assignedTaskId, int userId, CreateTimeEntryDto timeEntryDto)
        {
            if (timeEntryDto == null)
            {
                return BadRequest("Invalid time entry data.");
            }

            var createdTimeEntry = await _timeEntryService.CreateAsync(projectId, assignedTaskId, userId, timeEntryDto);
            return CreatedAtAction(nameof(Create), new { id = createdTimeEntry.Id }, createdTimeEntry);
        }
    }
}
