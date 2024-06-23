using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVCProject_API.Data;
using MVCProject_API.DTOs.CategoryDto;
using MVCProject_API.Helpers.Extensions;
using MVCProject_API.Models;
using MVCProject_API.Services.Interfaces;

namespace MVCProject_API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public CategoryService(AppDbContext context, IWebHostEnvironment env,IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }
        public async Task Create(CategoryCreateDto category)
        {
            string fileName = category.ImageFile.FileName.FileNameGenerator();

            string path = _env.GenerateFilePath("img", fileName);

            await category.ImageFile.SaveToFileAsync(path);
            category.Image = fileName;

            await _context.Categories.AddAsync(_mapper.Map<Category>(category));
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Category category, CategoryEditDto request)
        {
            if (request.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", category.Image);
                oldPath.DeleteImage();

                string fileName = request.NewImage.FileName.FileNameGenerator();
                string path = _env.GenerateFilePath("img", fileName);

                await request.NewImage.SaveToFileAsync(path);

                category.Image = fileName;
            }
            _mapper.Map(request,category);
            _context.Categories.Update(category);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistCategory(string name)
        {
            return await _context.Categories.AnyAsync(c => c.Name == name);
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            return _mapper.Map<List<CategoryDto>>(await _context.Categories.AsNoTracking().ToListAsync());
        }

        public async Task<List<Category>> GetAllCategoriesWithCourses()
        {
            return await _context.Categories.Include(m=>m.Courses).AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Categories.Include(m => m.Courses).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
