namespace EduVibe.DTOs.Department;

public class DepartmentFilterRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public string? Name { get; set; }
    public string? SortBy { get; set; } = "name";
    public string SortDirection { get; set; } = "asc";
}
