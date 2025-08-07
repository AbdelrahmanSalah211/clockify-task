using System.Text;
using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly ITimeEntryRepository _timeEntryRepository;

        public ReportService(ITimeEntryRepository timeEntryRepository)
        {
            _timeEntryRepository = timeEntryRepository;
        }

        public async Task<string> GenerateTimeReportCsvAsync()
        {
            var timeEntries = await _timeEntryRepository.GetAllForReportAsync();

            var records = timeEntries
                .Where(te => te.User != null && te.Task != null && te.Task.Project != null)
                .Select(te => new TimeEntryCsvRecord
                {
                    UserName = te.User!.Name,
                    ProjectName = te.Task!.Project!.Name,
                    TaskName = te.Task.Name,
                    OriginalEstimate = te.Task.EstimatedHours,
                    TimeSpent = Math.Round((te.End - te.Start).TotalHours, 2)
                })
                .ToList();

            var csv = new StringBuilder();
            csv.AppendLine("User,Project,Task,Original Estimate (Hrs),Time Spent (Hrs)");

            foreach (var r in records)
            {
                csv.AppendLine($"{r.UserName},{r.ProjectName},{r.TaskName},{r.OriginalEstimate},{r.TimeSpent}");
            }

            return csv.ToString();
        }
    }
}
