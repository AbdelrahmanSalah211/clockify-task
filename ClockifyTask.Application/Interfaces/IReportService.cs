namespace ClockifyTask.Application.Interfaces
{
    public interface IReportService
    {
        Task<string> GenerateTimeReportCsvAsync();
    }
}
