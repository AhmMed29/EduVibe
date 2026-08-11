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

    [Required]
    [Range(1, 10, ErrorMessage = "Credits must be between 1 and 10")]
    public int Credits { get; set; }

    [Range(1, 500, ErrorMessage = "Duration in hours must be between 1 and 500")]
    public int? DurationInHours { get; set; }

    [Range(0, 10000, ErrorMessage = "Price per hour must be a positive value")]
    public decimal? PricePerHour { get; set; }

    [Required]
    public int DepartmentId { get; set; }
}
