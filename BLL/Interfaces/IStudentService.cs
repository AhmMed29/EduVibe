using EduVibe.DTOs.Student;
using EduVibe.Models.Entities;
using EduVibe.Models.Response;

namespace EduVibe.Interfaces;

public interface IStudentService
{
    Task<PagedResponse<StudentDto>> GetAllAsync(StudentFilterRequest request);
    Task<StudentDto> GetByIdAsync(int id);
    Task<StudentDto> CreateAsync(StudentCreateDto dto);
    Task UpdateAsync(int id, StudentUpdateDto dto);
    Task DeleteAsync(int id);
}
