using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVCProject_API.Data;
using MVCProject_API.DTOs.CourseDto;
using MVCProject_API.Helpers.Extensions;
using MVCProject_API.Models;
using MVCProject_API.Services.Interfaces;

namespace MVCProject_API.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public CourseService(AppDbContext context,IMapper mapper,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }
        public async Task Create(CourseCreateDto request)
        {
            List<CourseImage> images = new();

            foreach (var item in request.ImageFiles)
            {
                string fileName = item.FileName.FileNameGenerator();

                string path = _env.GenerateFilePath("img", fileName);

                await item.SaveToFileAsync(path);

                images.Add(new CourseImage { Name = fileName });
            }

            images.FirstOrDefault().IsMain = true;
           
            request.CourseImages = images;
            request.CategoryId = _context.Categories.FirstOrDefault(m=>m.Name == request.CategoryName).Id;
           
            if(request.InstructorFullName is not null)
            {
                var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.FullName == request.InstructorFullName);

                if (instructor != null)
                {
                    request.InstructorId = instructor.Id;
                }
            }
            
            await _context.Courses.AddAsync(_mapper.Map<Course>(request));
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Course course, CourseEditDto request)
        {
            if(request.NewImages is not null)
            {
                foreach (var image in course.CourseImages)
                {
                    string oldPath = _env.GenerateFilePath("img", image.Name);

                    oldPath.DeleteImage();
                }

                List<CourseImage> images = new();

                foreach (var item in request.NewImages)
                {

                    string fileName = item.FileName.FileNameGenerator();

                    string path = _env.GenerateFilePath("img", fileName);

                    await item.SaveToFileAsync(path);

                    images.Add(new CourseImage { Name = fileName });
                }

                images.FirstOrDefault().IsMain = true;
                request.CourseImages = images;
            }
            else
            {
                request.CourseImages = course.CourseImages.ToList();
            }

            request.CategoryId = _context.Categories.FirstOrDefaultAsync(m => m.Name == request.CategoryName).Id;
            request.InstructorId = _context.Instructors.FirstOrDefaultAsync(m => m.FullName == request.InstructorFullName).Id;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistCourse(string name)
        {
            return await _context.Courses.AnyAsync(m => m.Name == name);
        }

        public async Task<List<CourseDto>> GetAll()
        {
            return _mapper.Map<List<CourseDto>>(await _context.Courses.Include(m=>m.Instructor).Include(m => m.Category).Include(m => m.CourseStudents).Include(m => m.CourseImages).ToListAsync());
        }

        public async Task<Course> GetById(int id)
        {
            return await _context.Courses.Include(m => m.Instructor).Include(m => m.Category).Include(m => m.CourseStudents).Include(m => m.CourseImages).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
