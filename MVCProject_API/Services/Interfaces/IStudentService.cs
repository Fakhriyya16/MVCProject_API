using MVCProject_API.DTOs.StudentDto;
using MVCProject_API.Models;

namespace MVCProject_API.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentDto>> GetAll();
        Task<Student> GetById(int id);
        Task Create(StudentCreateDto student);
        Task Edit(Student student, StudentEditDto request);
        Task Delete(Student student);
    }
}
