using MVCProject_API.DTOs.CourseDto;
using MVCProject_API.Models;

namespace MVCProject_API.Services.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAll();
        Task<bool> ExistCourse(string name);
        Task Create(CourseCreateDto request);
        Task<Course> GetById(int id);
        Task Delete(Course course);
        Task Edit(Course course, CourseEditDto request);
    }
}
