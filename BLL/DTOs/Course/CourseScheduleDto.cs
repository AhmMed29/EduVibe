namespace EduVibe.DTOs.Course;

public class CourseScheduleDto
{
    public int Id { get; set; }
    public string DayOfWeek { get; set; } = null!;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Room { get; set; } = null!;
}
