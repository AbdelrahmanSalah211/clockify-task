namespace task.Services
{
  public interface IReportService
  {
    Task<string> GenerateTimeReportCsvAsync();
  }
}