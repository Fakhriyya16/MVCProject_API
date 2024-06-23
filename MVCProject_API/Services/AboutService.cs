using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVCProject_API.Data;
using MVCProject_API.DTOs.AboutDto;
using MVCProject_API.Helpers.Extensions;
using MVCProject_API.Models;
using MVCProject_API.Services.Interfaces;

namespace MVCProject_API.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public AboutService(AppDbContext context,IMapper mapper,IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task Create(AboutCreateDto request)
        {
            await _context.Abouts.AddAsync(_mapper.Map<About>(request));
            await _context.SaveChangesAsync();
        }

        public async Task Delete(About request)
        {
            _context.Abouts.Remove(request);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(About about, AboutEditDto request)
        {
            if(request.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", about.Image);

                oldPath.DeleteImage();

                string fileName = request.NewImage.FileName.FileNameGenerator();
                
                string path = _env.GenerateFilePath("img", fileName);
                
                await request.NewImage.SaveToFileAsync(path);
                request.Image = fileName;
            }
            _context.Abouts.Update(_mapper.Map(request,about));
            await _context.SaveChangesAsync();
        }

        public async Task<List<AboutDto>> GetAll()
        {
            return _mapper.Map<List<AboutDto>>(await _context.Abouts.AsNoTracking().ToListAsync());
        }

        public async Task<AboutDetailDto> GetById(int id)
        {
            return _mapper.Map<AboutDetailDto>(await _context.Abouts.AsNoTracking().FirstOrDefaultAsync(m=>m.Id == id));
        }
    }
}
