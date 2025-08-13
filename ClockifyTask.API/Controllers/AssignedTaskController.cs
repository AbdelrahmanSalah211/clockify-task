using Microsoft.AspNetCore.Mvc;
using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ClockifyTask.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/assignedTasks")]
    public class AssignedTaskController : ControllerBase
    {
        private readonly IAssignedTaskService _assignedTaskService;
        private readonly ITimeEntryService _timeEntryService;

        public AssignedTaskController(IAssignedTaskService assignedTaskService, ITimeEntryService timeEntryService)
        {
            _assignedTaskService = assignedTaskService;
            _timeEntryService = timeEntryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignedTaskDto>>> GetAll()
        {
            var tasks = await _assignedTaskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpPost("{assignedTaskId:int}/timeEntries")]
        public async Task<ActionResult<TimeEntryDto>> Create(int assignedTaskId, CreateTimeEntryDto timeEntryDto)
        {
            if (timeEntryDto == null)
            {
                return BadRequest("Invalid time entry data.");
            }

            var createdTimeEntry = await _timeEntryService.CreateAsync(assignedTaskId, timeEntryDto);
            return CreatedAtAction(nameof(Create), new { id = createdTimeEntry.Id }, createdTimeEntry);
        }
    }
}
