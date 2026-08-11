using EduVibe.DTOs.Shared;

namespace EduVibe.DTOs.Instructor;

public class InstructorDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public AddressDto Address { get; set; } = null!;
    public decimal? Salary { get; set; }
    public string? DepartmentName { get; set; }
}
