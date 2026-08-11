using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EduVibe.Data;
using EduVibe.DTOs.Department;
using EduVibe.Models.Entities;
using EduVibe.Models.Exceptions;
using EduVibe.Models.Response;

namespace EduVibe.Services;

public class DepartmentService : IDepartmentService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public DepartmentService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResponse<DepartmentDto>> GetAllAsync(DepartmentFilterRequest request)
    {
        var query = _context.Departments.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            query = query.Where(d =>
                d.Name.Contains(request.SearchTerm) ||
                (d.Description != null && d.Description.Contains(request.SearchTerm)));

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(d => d.Name.Contains(request.Name));

        query = request.SortDirection.ToLower() == "desc"
            ? query.OrderByDescending(d => d.Name)
            : query.OrderBy(d => d.Name);

        var totalRecords = await query.CountAsync();

        var departments = await query
            .Include(c => c.Courses)
            .Include(s => s.Instructors)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync();

        return new PagedResponse<DepartmentDto>
        {
            Data = _mapper.Map<List<DepartmentDto>>(departments),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize)
        };
    }

    public async Task<DepartmentDto> GetByIdAsync(int id)
    {
        var department = await _context.Departments
            .Include(c => c.Courses)
            .Include(s => s.Instructors)
            .FirstOrDefaultAsync(i => id == i.Id);

        if (department == null)
            throw new NotFoundException($"Department with ID {id} not found.");

        return _mapper.Map<DepartmentDto>(department);
    }

    public async Task<Department> CreateAsync(Department department)
    {
        department.CreatedAt = DateTime.Now;
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return department;
    }

    public async Task UpdateAsync(int id, Department department)
    {
        var existingDepartment = await _context.Departments
            .Include(i => i.Instructors)
            .Include(c => c.Courses)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingDepartment == null)
            throw new NotFoundException($"Department with ID {id} not found.");

        existingDepartment.Name = department.Name;
        existingDepartment.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
            throw new NotFoundException($"Department with ID {id} not found.");

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }
}
