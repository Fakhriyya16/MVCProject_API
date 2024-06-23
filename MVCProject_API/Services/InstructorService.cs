using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVCProject_API.Data;
using MVCProject_API.DTOs.InstructorDto;
using MVCProject_API.Helpers.Extensions;
using MVCProject_API.Models;
using MVCProject_API.Services.Interfaces;

namespace MVCProject_API.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public InstructorService(AppDbContext context, IWebHostEnvironment env,IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }
        public async Task Create(InstructorCreateDto instructor)
        {
            string fileName = instructor.ImageFile.FileName.FileNameGenerator();

            string path = _env.GenerateFilePath("img", fileName);

            await instructor.ImageFile.SaveToFileAsync(path);
            instructor.Image = fileName;

            await _context.Instructors.AddAsync(_mapper.Map<Instructor>(instructor));
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Instructor instructor)
        {
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Instructor instructor, InstructorEditDto request)
        {
            if (request.ImageFile is not null)
            {
                string oldPath = _env.GenerateFilePath("img", instructor.Image);
                oldPath.DeleteImage();

                string fileName = request.ImageFile.FileName.FileNameGenerator();
                string path = _env.GenerateFilePath("img", fileName);

                await request.ImageFile.SaveToFileAsync(path);

                instructor.Image = fileName;
            }
            _mapper.Map(request, instructor);
            _context.Instructors.Update(instructor);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistEmail(string email)
        {
            return await _context.Instructors.AnyAsync(x => x.Email == email);
        }

        public async Task<List<InstructorDto>> GetAll()
        {
            return _mapper.Map<List<InstructorDto>>(await _context.Instructors.Include(m=>m.Courses).ToListAsync());
        }

        public async Task<Instructor> GetById(int id)
        {
            return await _context.Instructors.Include(m => m.Courses).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
