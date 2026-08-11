namespace EduVibe.DTOs.Instructor;

public class InstructorFilterRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? SortBy { get; set; } = "fname";
    public string SortDirection { get; set; } = "asc";
}
