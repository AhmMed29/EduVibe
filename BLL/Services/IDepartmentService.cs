using EduVibe.DTOs.Department;
using EduVibe.Models.Entities;
using EduVibe.Models.Response;

namespace EduVibe.Services;

public interface IDepartmentService
{
    Task<PagedResponse<DepartmentDto>> GetAllAsync(DepartmentFilterRequest request);
    Task<DepartmentDto> GetByIdAsync(int id);
    Task<Department> CreateAsync(Department department);
    Task UpdateAsync(int id, Department department);
    Task DeleteAsync(int id);
}
