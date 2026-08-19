namespace EduVibe.DTOs.Course;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int? DurationInHours { get; set; }
    public string? DepartmentName { get; set; }
    public string CourseLevel { get; set; } = string.Empty;
}
