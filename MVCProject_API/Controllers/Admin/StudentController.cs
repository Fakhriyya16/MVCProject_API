using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject_API.Data;
using MVCProject_API.DTOs.StudentDto;
using MVCProject_API.Services.Interfaces;

namespace MVCProject_API.Controllers.Admin
{
    public class StudentController : BaseAdminController
    {
        private readonly IStudentService _studentService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public StudentController(IStudentService studentService,AppDbContext context,IMapper mapper)
        {
            _studentService = studentService;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] StudentCreateDto request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if(!await _context.Courses.AnyAsync(m=>m.Name == request.Course))
            {
                return BadRequest("Course does not exists");
            }

            await _studentService.Create(request);
          
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] StudentEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _context.Courses.AnyAsync(m => m.Name == request.Course))
            {
                return BadRequest("Course does not exists");
            }

            var student = await _studentService.GetById(id);

            if (student is null) return NotFound();

            await _studentService.Edit(student, request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var student = await _studentService.GetById(id);

            if (student is null) return NotFound();

            await _studentService.Delete(student);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var student = await _studentService.GetById(id);

            if (student is null) return NotFound();

            return Ok(_mapper.Map<StudentDetailDto>(student));
        }
    }
}
