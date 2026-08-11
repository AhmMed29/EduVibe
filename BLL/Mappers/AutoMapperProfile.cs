using AutoMapper;
using EduVibe.Models.Entities;
using EduVibe.DTOs.Student;
using EduVibe.DTOs.Course;
using EduVibe.DTOs.Instructor;
using EduVibe.DTOs.Department;
using EduVibe.DTOs.Enrollment;
using EduVibe.DTOs.Shared;

namespace EduVibe.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<StudentCreateDto, Student>();

        CreateMap<StuAddress, AddressDto>();
        CreateMap<InsAddress, AddressDto>();

        CreateMap<Student, StudentDto>()
            .ForMember(dto => dto.FullName, opt => opt.MapFrom(e => $"{e.Fname} {e.Lname}"))
            .ForMember(dto => dto.DepartmentName, opt => opt.MapFrom(e => e.Department != null ? e.Department.Name : null));

        CreateMap<Enrollment, EnrollmentDto>()
            .ForMember(dto => dto.CourseTitle, opt => opt.MapFrom(e => e.Course != null ? e.Course.Title : string.Empty))
            .ForMember(dto => dto.Credits, opt => opt.MapFrom(e => e.Course != null ? e.Course.Credits : 0))
            .ForMember(dto => dto.EnrolledAt, opt => opt.MapFrom(e => e.CreatedAt));

        CreateMap<CourseSchedule, CourseScheduleDto>();

        CreateMap<Course, CourseDto>()
            .ForMember(dto => dto.DepartmentName, opt => opt.MapFrom(e => e.Department != null ? e.Department.Name : null));

        CreateMap<Instructor, InstructorDto>()
            .ForMember(dto => dto.FullName, opt => opt.MapFrom(e => $"{e.Fname} {e.Lname}"))
            .ForMember(dto => dto.DepartmentName, opt => opt.MapFrom(e => e.Department != null ? e.Department.Name : null));

        CreateMap<Department, DepartmentDto>();
    }
}