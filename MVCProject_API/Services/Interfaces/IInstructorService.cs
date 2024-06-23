using MVCProject_API.DTOs.InstructorDto;
using MVCProject_API.Models;

namespace MVCProject_API.Services.Interfaces
{
    public interface IInstructorService
    {
        Task<List<InstructorDto>> GetAll();
        Task<Instructor> GetById(int id);
        Task<bool> ExistEmail(string email);
        Task Create(InstructorCreateDto instructor);
        Task Edit(Instructor instructor, InstructorEditDto request);
        Task Delete(Instructor instructor);
    }
}
