using AutoMapper;
using MVCProject_API.DTOs.AboutDto;
using MVCProject_API.DTOs.CategoryDto;
using MVCProject_API.DTOs.CourseDto;
using MVCProject_API.DTOs.InstructorDto;
using MVCProject_API.DTOs.StudentDto;
using MVCProject_API.Models;
using System.Globalization;

namespace MVCProject_API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<About, AboutDto>();
            CreateMap<AboutCreateDto, About>();
            CreateMap<AboutEditDto, About>().ForMember(dest => dest.Image, opt => opt.Condition(src => src.Image is not null));
            CreateMap<About, AboutDetailDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ForMember(dest => dest.CourseCount,opt => opt.MapFrom(src => src.Courses.Count));
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryEditDto, Category>().ForMember(dest => dest.Image, opt => opt.Condition(src => src.Image is not null));
            CreateMap<Category, CategoryDetailDto>().ReverseMap();

            CreateMap<CourseCreateDto, Course>()
                     .ForMember(dest => dest.InstructorId, opt => opt.Condition(src => src.InstructorId is not null));
            CreateMap<Course, CourseDetailDto>()
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                    .ForMember(dest => dest.InstructorName, opt => opt.Condition(src => src.InstructorId is not null))
                    .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.CourseStudents.Count))
                    .ForMember(dest => dest.CourseImages, opt => opt.MapFrom(src => src.CourseImages.Select(m => m.Name)));
            CreateMap<CourseEditDto, Course>();
            CreateMap<Course,CourseDto>()
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.CourseImages.FirstOrDefault(m=>m.IsMain).Name))
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                    .ForMember(dest => dest.Instructor, opt => opt.MapFrom(src => src.Instructor.FullName))
                    .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.CourseStudents.Count));

            CreateMap<Instructor, InstructorDto>()
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses.Select(m => m.Name))); 
            CreateMap<InstructorCreateDto, Instructor>();
            CreateMap<InstructorEditDto, Instructor>().ForMember(dest => dest.Image, opt => opt.Condition(src => src.Image is not null));
            CreateMap<Instructor, InstructorDetailDto>()
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses.Select(m=>m.Name))); 

            CreateMap<Student, StudentDto>()
                        .ForMember(dest => dest.Courses, 
                         opt => opt.MapFrom(src => src.CourseStudents.Select(cs => cs.Course.Name).ToList()));
            CreateMap<StudentCreateDto, Student>();
            CreateMap<StudentEditDto, Student>()
           .ForMember(dest => dest.Image, opt => opt.Condition(src => src.ImageFile is not null));
            CreateMap<Student, StudentDetailDto>()
                         .ForMember(dest => dest.Courses, 
                         opt => opt.MapFrom(src => src.CourseStudents.Select(cs => cs.Course.Name).ToList()));
        }

    }
}
