using Microsoft.AspNetCore.Mvc;
using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ClockifyTask.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/timeEntries")]
    public class TimeEntriesController : ControllerBase
    {
        private readonly ITimeEntryService _timeEntryService;

        public TimeEntriesController(ITimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeEntryDto>>> GetAll()
        {
            var timeEntries = await _timeEntryService.GetAllAsync();
            return Ok(timeEntries);
        }
    }
}
