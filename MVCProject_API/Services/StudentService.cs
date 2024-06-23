using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVCProject_API.Data;
using MVCProject_API.DTOs.StudentDto;
using MVCProject_API.Helpers.Extensions;
using MVCProject_API.Models;
using MVCProject_API.Services.Interfaces;

namespace MVCProject_API.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public StudentService(AppDbContext context,IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public async Task Create(StudentCreateDto studentDto)
        {
            string fileName = studentDto.ImageFile.FileName.FileNameGenerator();
            string path = _env.GenerateFilePath("img", fileName);

            await studentDto.ImageFile.SaveToFileAsync(path);
            studentDto.Image = fileName;

            var course = await _context.Courses.Include(m=>m.CourseStudents).FirstOrDefaultAsync(m => m.Name == studentDto.Course);

            if (course == null)
            {
                throw new InvalidOperationException($"Course '{studentDto.Course}' not found.");
            }

            var student = _mapper.Map<Student>(studentDto);

            var courseStudent = new CourseStudent
            {
                CourseId = course.Id,
                Student = student 
            };

            await _context.CourseStudents.AddAsync(courseStudent);

            await _context.Students.AddAsync(student);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Student student, StudentEditDto request)
        {
            if (request.ImageFile is not null)
            {
                string oldPath = _env.GenerateFilePath("img", student.Image);
                oldPath.DeleteImage();

                string fileName = request.ImageFile.FileName.FileNameGenerator();
                string path = _env.GenerateFilePath("img", fileName);

                await request.ImageFile.SaveToFileAsync(path);

                request.Image = fileName;
            }

            var course = await _context.Courses.Include(m => m.CourseStudents).FirstOrDefaultAsync(m => m.Name == request.Course);

            if (course != null)
            {
                request.CourseId = course.Id;
            }

            _mapper.Map(request, student);
            _context.Students.Update(student);

            await _context.SaveChangesAsync();
        }

        public async Task<List<StudentDto>> GetAll()
        {
            return _mapper.Map<List<StudentDto>>(await _context.Students.Include(m => m.CourseStudents).ThenInclude(m => m.Course).ToListAsync());
        }

        public async Task<Student> GetById(int id)
        {
            return await _context.Students.Include(m => m.CourseStudents).ThenInclude(m => m.Course).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
