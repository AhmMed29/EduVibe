namespace EduVibe.DTOs.Enrollment;

public class EnrollmentDto
{
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = null!;
    public int Credits { get; set; }
    public decimal Grades { get; set; }
    public DateTime? EnrolledAt { get; set; }
}
