using System.ComponentModel.DataAnnotations;

namespace EduVibe.DTOs.Course;

public class CourseCreateDto
{
    [Required]
    [StringLength(100, ErrorMessage = "Title must be less than 100 characters")]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(500, ErrorMessage = "Description must be less than 500 characters")]
    public string Description { get; set; } = null!;

    [Range(1, 500, ErrorMessage = "Duration in hours must be between 1 and 500")]
    public int? DurationInHours { get; set; }

    [Range(0, 10000, ErrorMessage = "Price per hour must be a positive value")]
    public decimal? Price { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Course Level must be less than 50 characters")]
    public string CourseLevel { get; set; } = string.Empty;
    
    [Required]
    public int DepartmentId { get; set; }
}
