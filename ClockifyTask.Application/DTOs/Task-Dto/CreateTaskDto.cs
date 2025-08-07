namespace ClockifyTask.Application.DTOs.Task_Dto;
    public record CreateTaskDto
    {
        public required string Name { get; init; }
        public required int EstimatedHours { get; init; }
        public int UserId { get; init; }
        public int ProjectId { get; init; }
    }