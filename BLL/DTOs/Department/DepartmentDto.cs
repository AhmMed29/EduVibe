using EduVibe.DTOs.Course;
using EduVibe.DTOs.Instructor;

namespace EduVibe.DTOs.Department;

public class DepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    
    public ICollection<CourseDto> Courses { get; set; } = new List<CourseDto>();
    public ICollection<InstructorDto> Instructors { get; set; } = new List<InstructorDto>();
}
