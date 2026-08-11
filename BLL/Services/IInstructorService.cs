using EduVibe.DTOs.Instructor;
using EduVibe.Models.Entities;
using EduVibe.Models.Response;

namespace EduVibe.Services;

public interface IInstructorService
{
    Task<PagedResponse<InstructorDto>> GetAllAsync(InstructorFilterRequest request);
    Task<InstructorDto> GetByIdAsync(int id);
    Task<Instructor> CreateAsync(Instructor instructor);
    Task UpdateAsync(int id, Instructor instructor);
    Task DeleteAsync(int id);
}
