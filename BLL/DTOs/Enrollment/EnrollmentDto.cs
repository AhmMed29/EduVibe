namespace EduVibe.DTOs.Enrollment;

public class EnrollmentDto
{
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = null!;
    public DateTime? EnrolledAt { get; set; }
    public string CourseLevel { get; set; } = string.Empty;
}

