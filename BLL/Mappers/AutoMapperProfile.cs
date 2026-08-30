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
        CreateMap<StuAddress, AddressDto>().ReverseMap();
        CreateMap<InsAddress, AddressDto>().ReverseMap();

        CreateMap<StudentCreateDto, Student>()
            .ForMember(d=>d.Fname, o=>o.MapFrom(f=>f.FirstName))
            .ForMember(d=>d.Lname, o=>o.MapFrom(f=>f.LastName))
            .ForMember(d=>d.PhoneNumber, o=>o.MapFrom(f=>f.Phone));

            
        CreateMap<Enrollment, EnrollmentDto>()
            .ForMember(dto => dto.CourseTitle, opt => opt.MapFrom(e => e.Course != null ? e.Course.Title : string.Empty))
            .ForMember(dto => dto.EnrolledAt, opt => opt.MapFrom(e => e.CreatedAt))
            .ForMember(dto => dto.CourseLevel, opt => opt.MapFrom(e => e.Course != null ? e.Course.CourseLevel : string.Empty));
        
        CreateMap<Course, CourseDto>()
            .ForMember(dto => dto.DepartmentName, opt => opt.MapFrom(e => e.Department != null ? e.Department.Name : null));

        CreateMap<Instructor, InstructorDto>()
            .ForMember(dto => dto.FullName, opt => opt.MapFrom(e => $"{e.Fname} {e.Lname}"))
            .ForMember(dto => dto.DepartmentName, opt => opt.MapFrom(e => e.Department != null ? e.Department.Name : null));

        CreateMap<Department, DepartmentDto>();
    }
}