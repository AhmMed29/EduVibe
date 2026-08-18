using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EduVibe.Data;
using EduVibe.DTOs.Instructor;
using EduVibe.Interfaces;
using EduVibe.Models.Entities;
using EduVibe.Models.Exceptions;
using EduVibe.Models.Response;

namespace EduVibe.Services;

public class InstructorService : IInstructorService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public InstructorService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResponse<InstructorDto>> GetAllAsync(InstructorFilterRequest request)
    {
        var query = _context.Instructors.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            query = query.Where(i =>
                i.Fname.Contains(request.SearchTerm) ||
                i.Lname.Contains(request.SearchTerm) ||
                i.Email.Contains(request.SearchTerm) ||
                i.PhoneNumber.Contains(request.SearchTerm));

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(i =>
                i.Fname.Contains(request.Name) ||
                i.Lname.Contains(request.Name));

        if (!string.IsNullOrWhiteSpace(request.Email))
            query = query.Where(i => i.Email.Contains(request.Email));

        if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            query = query.Where(i => i.PhoneNumber.Contains(request.PhoneNumber));

        query = request.SortDirection.ToLower() == "desc"
            ? request.SortBy?.ToLower() switch
            {
                "lname" => query.OrderByDescending(i => i.Lname),
                "email" => query.OrderByDescending(i => i.Email),
                "salary" => query.OrderByDescending(i => i.Salary),
                _ => query.OrderByDescending(i => i.Fname)
            }
            : request.SortBy?.ToLower() switch
            {
                "lname" => query.OrderBy(i => i.Lname),
                "email" => query.OrderBy(i => i.Email),
                "salary" => query.OrderBy(i => i.Salary),
                _ => query.OrderBy(i => i.Fname)
            };

        var totalRecords = await query.CountAsync();

        var instructors = await query
            .Include(a => a.Address)
            .Include(d => d.Department)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync();

        return new PagedResponse<InstructorDto>
        {
            Data = _mapper.Map<List<InstructorDto>>(instructors),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize)
        };
    }

    public async Task<InstructorDto> GetByIdAsync(int id)
    {
        var instructor = await _context.Instructors
            .Include(s => s.Courses)
            .Include(a => a.Address)
            .Include(d => d.Department)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (instructor == null)
            throw new NotFoundException($"Instructor with ID {id} not found.");

        return _mapper.Map<InstructorDto>(instructor);
    }

    public async Task<Instructor> CreateAsync(Instructor instructor)
    {
        _context.Instructors.Add(instructor);
        await _context.SaveChangesAsync();
        return instructor;
    }

    public async Task UpdateAsync(int id, Instructor _instructor)
    {
        var instructor = await _context.Instructors.FindAsync(id);
        if (instructor == null)
            throw new NotFoundException($"Instructor with ID {id} not found.");

        _instructor.UpdatedAt = DateTime.UtcNow;
        instructor.Fname = _instructor.Fname;
        instructor.Lname = _instructor.Lname;
        instructor.Email = _instructor.Email;
        instructor.PhoneNumber = _instructor.PhoneNumber;
        instructor.DepartmentId = _instructor.DepartmentId;
        instructor.Address.City = _instructor.Address.City;
        instructor.Address.Country = _instructor.Address.Country;
        instructor.DateOfBirth = _instructor.DateOfBirth;
        instructor.Salary = _instructor.Salary;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var instructor = await _context.Instructors.FindAsync(id);
        if (instructor == null)
            throw new NotFoundException($"Instructor with ID {id} not found.");

        _context.Instructors.Remove(instructor);
        await _context.SaveChangesAsync();
    }
}
