using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EduVibe.Data;
using EduVibe.DTOs.Student;
using EduVibe.Interfaces;
using EduVibe.Models.Entities;
using EduVibe.Models.Exceptions;
using EduVibe.Models.Response;

namespace EduVibe.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public StudentService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResponse<StudentDto>> GetAllAsync(StudentFilterRequest request)
    {
        var query = _context.Students.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            query = query.Where(s =>
                s.Fname.Contains(request.SearchTerm) ||
                s.Lname.Contains(request.SearchTerm) ||
                s.Email.Contains(request.SearchTerm) ||
                s.PhoneNumber.Contains(request.SearchTerm));

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(s =>
                s.Fname.Contains(request.Name) ||
                s.Lname.Contains(request.Name));

        if (!string.IsNullOrWhiteSpace(request.Email))
            query = query.Where(s => s.Email.Contains(request.Email));

        if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            query = query.Where(s => s.PhoneNumber.Contains(request.PhoneNumber));

        query = request.SortDirection.ToLower() == "desc"
            ? request.SortBy?.ToLower() switch
            {
                "lname" => query.OrderByDescending(s => s.Lname),
                "email" => query.OrderByDescending(s => s.Email),
                "dateofbirth" => query.OrderByDescending(s => s.DateOfBirth),
                "createdat" => query.OrderByDescending(s => s.CreatedAt),
                _ => query.OrderByDescending(s => s.Fname)
            }
            : request.SortBy?.ToLower() switch
            {
                "lname" => query.OrderBy(s => s.Lname),
                "email" => query.OrderBy(s => s.Email),
                "dateofbirth" => query.OrderBy(s => s.DateOfBirth),
                "createdat" => query.OrderBy(s => s.CreatedAt),
                _ => query.OrderBy(s => s.Fname)
            };

        var totalRecords = await query.CountAsync();

        var students = await query
            .Include(a => a.Address)
            .Include(d => d.Department)
            .Include(e => e.Enrollments)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync();

        return new PagedResponse<StudentDto>
        {
            Data = _mapper.Map<List<StudentDto>>(students),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize)
        };
    }

    public async Task<StudentDto> GetByIdAsync(int id)
    {
        var student = await _context.Students
            .Include(s => s.Department)
            .Include(s => s.Address)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null)
            throw new NotFoundException($"Student with ID {id} not found.");

        return _mapper.Map<StudentDto>(student);
    }

    public async Task<Student> CreateAsync(Student student)
    {
        student.CreatedAt = DateTime.Now;
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task UpdateAsync(int id, Student student)
    {
        var existingStudent = await _context.Students
            .Include(s => s.Address)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (existingStudent == null)
            throw new NotFoundException($"Student with ID {id} not found.");

        existingStudent.Fname = student.Fname;
        existingStudent.Lname = student.Lname;
        existingStudent.Email = student.Email;
        existingStudent.UpdatedAt = student.UpdatedAt;
        existingStudent.DateOfBirth = student.DateOfBirth;
        existingStudent.PhoneNumber = student.PhoneNumber;

        if (student.Address != null)
        {
            if (existingStudent.Address == null)
                existingStudent.Address = new StuAddress();

            existingStudent.Address.City = student.Address.City;
            existingStudent.Address.Country = student.Address.Country;
        }
        else
        {
            existingStudent.Address = null;
        }

        existingStudent.DepartmentId = student.DepartmentId;
        existingStudent.Gender = student.Gender;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            throw new NotFoundException($"Student with ID {id} not found.");

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }
}
