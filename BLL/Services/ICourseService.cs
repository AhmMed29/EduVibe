using EduVibe.DTOs.Course;
using EduVibe.Models.Entities;
using EduVibe.Models.Response;

namespace EduVibe.Services;

public interface ICourseService
{
    Task<PagedResponse<CourseDto>> GetAllAsync(CourseFilterRequest request);
    Task<CourseDto> GetByIdAsync(int id);
    Task<Course> CreateAsync(Course course);
    Task UpdateAsync(int id, Course course);
    Task DeleteAsync(int id);
}
