using System.Text;
using Microsoft.AspNetCore.Mvc;
using ClockifyTask.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ClockifyTask.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("csv")]
        public async Task<IActionResult> DownloadCsv()
        {
            var csvContent = await _reportService.GenerateTimeReportCsvAsync();
            var bytes = Encoding.UTF8.GetBytes(csvContent);
            return File(bytes, "text/csv", "time-report.csv");
        }
    }
}
