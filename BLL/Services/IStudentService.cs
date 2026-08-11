using EduVibe.DTOs.Student;
using EduVibe.Models.Entities;
using EduVibe.Models.Response;

namespace EduVibe.Services;

public interface IStudentService
{
    Task<PagedResponse<StudentDto>> GetAllAsync(StudentFilterRequest request);
    Task<StudentDto> GetByIdAsync(int id);
    Task<Student> CreateAsync(Student student);
    Task UpdateAsync(int id, Student student);
    Task DeleteAsync(int id);
}
