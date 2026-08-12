using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EduVibe.Data;
using EduVibe.DTOs.Course;
using EduVibe.Interfaces;
using EduVibe.Models.Entities;
using EduVibe.Models.Exceptions;
using EduVibe.Models.Response;

namespace EduVibe.Services;

public class CourseService : ICourseService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CourseService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResponse<CourseDto>> GetAllAsync(CourseFilterRequest request)
    {
        var query = _context.Courses.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            query = query.Where(c =>
                c.Title.Contains(request.SearchTerm) ||
                c.Description.Contains(request.SearchTerm));

        if (!string.IsNullOrWhiteSpace(request.Title))
            query = query.Where(c => c.Title.Contains(request.Title));

        query = request.SortDirection.ToLower() == "desc"
            ? request.SortBy?.ToLower() switch
            {
                "credits" => query.OrderByDescending(c => c.Credits),
                "priceperhour" => query.OrderByDescending(c => c.PricePerHour),
                "durationinhours" => query.OrderByDescending(c => c.DurationInHours),
                _ => query.OrderByDescending(c => c.Title)
            }
            : request.SortBy?.ToLower() switch
            {
                "credits" => query.OrderBy(c => c.Credits),
                "priceperhour" => query.OrderBy(c => c.PricePerHour),
                "durationinhours" => query.OrderBy(c => c.DurationInHours),
                _ => query.OrderBy(c => c.Title)
            };

        var totalRecords = await query.CountAsync();

        var courses = await query
            .Include(s => s.CourseSchedules)
            .Include(s => s.Department)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync();

        return new PagedResponse<CourseDto>
        {
            Data = _mapper.Map<List<CourseDto>>(courses),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize)
        };
    }

    public async Task<CourseDto> GetByIdAsync(int id)
    {
        var course = await _context.Courses
            .Include(s => s.CourseSchedules)
            .Include(s => s.Department)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
            throw new NotFoundException($"Course with ID {id} not found.");

        return _mapper.Map<CourseDto>(course);
    }

    public async Task<Course> CreateAsync(Course course)
    {
        course.CreatedAt = DateTime.Now;
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task UpdateAsync(int id, Course course)
    {
        var existingCourse = await _context.Courses.FindAsync(id);
        if (existingCourse == null)
            throw new NotFoundException($"Course with ID {id} not found.");

        course.UpdatedAt = DateTime.Now;
        existingCourse.Title = course.Title;
        existingCourse.DepartmentId = course.DepartmentId;
        existingCourse.Description = course.Description;
        existingCourse.PricePerHour = course.PricePerHour;
        existingCourse.CourseSchedules = course.CourseSchedules;
        existingCourse.DurationInHours = course.DurationInHours;
        existingCourse.Credits = course.Credits;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
            throw new NotFoundException($"Course with ID {id} not found.");

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
    }
}
