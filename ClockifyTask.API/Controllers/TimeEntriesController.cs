using Microsoft.AspNetCore.Mvc;
using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;

namespace ClockifyTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeEntriesController : ControllerBase
    {
        private readonly ITimeEntryService _timeEntryService;

        public TimeEntriesController(ITimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }

        [HttpPost]
        public async Task<ActionResult<TimeEntryDto>> Create(CreateTimeEntryDto timeEntryDto)
        {
            if (timeEntryDto == null)
            {
                return BadRequest("Invalid time entry data.");
            }

            var createdTimeEntry = await _timeEntryService.CreateAsync(timeEntryDto);
            return CreatedAtAction(nameof(Create), new { id = createdTimeEntry.Id }, createdTimeEntry);
        }
    }
}
