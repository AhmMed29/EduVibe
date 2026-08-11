namespace EduVibe.DTOs.Course;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Credits { get; set; }
    public int? DurationInHours { get; set; }
    public decimal? PricePerHour { get; set; }
    public string? DepartmentName { get; set; }
    public ICollection<CourseScheduleDto> CourseSchedules { get; set; } = new List<CourseScheduleDto>();
}
