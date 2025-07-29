using System.Text;
using Microsoft.EntityFrameworkCore;
using task.Data;
using task.Services;


public class ReportService : IReportService
{
    private readonly AppDbContext _db;

    public ReportService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<string> GenerateTimeReportCsvAsync()
    {
        var timeEntries = await _db.TimeEntries
            .Include(t => t.User)
            .Include(t => t.Task)
            .ThenInclude(task => task!.Project)
            .ToListAsync();

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
