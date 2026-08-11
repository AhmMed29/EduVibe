using EduVibe.DTOs.Shared;
using EduVibe.DTOs.Enrollment;

namespace EduVibe.DTOs.Student;

public class StudentDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string? Gender { get; set; }
    
    public string? DepartmentName { get; set; }
    public AddressDto? Address { get; set; }
    public ICollection<EnrollmentDto> Enrollments { get; set; } = new List<EnrollmentDto>();
}