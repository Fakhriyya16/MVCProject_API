using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProject_API.Data;
using MVCProject_API.DTOs.CourseDto;
using MVCProject_API.Models;
using MVCProject_API.Services;
using MVCProject_API.Services.Interfaces;

namespace MVCProject_API.Controllers.Admin
{
    public class CourseController : BaseAdminController
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public CourseController(ICourseService courseService, IMapper mapper,AppDbContext context)
        {
            _courseService = courseService;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CourseCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _courseService.ExistCourse(request.Name))
            {
                return BadRequest("Course with this name already exists");
            }

            if (!await _context.Categories.AnyAsync(m => m.Name == request.CategoryName))
            {
                return BadRequest("Category does not exists");
            }

            if(request.InstructorFullName is not null)
            {
                if (!await _context.Instructors.AnyAsync(m => m.FullName == request.InstructorFullName))
                {
                    return BadRequest("Instructor does not exists");
                }
            }

            await _courseService.Create(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var course = await _courseService.GetById((int)id);

            if (course is null) return NotFound();

            await _courseService.Delete(course);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var course = await _courseService.GetById((int)id);

            if (course is null) return NotFound();

            return Ok(_mapper.Map<CourseDetailDto>(course));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id,CourseEditDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _courseService.ExistCourse(request.Name))
            {
                return BadRequest("Course with this name already exists");
            }

            if (!await _context.Categories.AnyAsync(m => m.Name == request.CategoryName))
            {
                return BadRequest("Category does not exists");
            }

            if (!await _context.Instructors.AnyAsync(m => m.FullName == request.InstructorFullName))
            {
                return BadRequest("Instructor does not exists");
            }

            var course = await _courseService.GetById((int)id);

            if (course is null) return NotFound();

            await _courseService.Edit(course,request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> CheckIfExists(string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest(new { error = "Input cannot be empty" });
            return Ok(await _courseService.ExistCourse(name));
        }
    }
}
