namespace EduVibe.DTOs.Course;

public class CourseFilterRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public string? Title { get; set; }
    public string? SortBy { get; set; } = "title";
    public string SortDirection { get; set; } = "asc";
}
