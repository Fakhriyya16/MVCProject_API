using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCProject_API.DTOs.CategoryDto;
using MVCProject_API.DTOs.InstructorDto;
using MVCProject_API.Services;
using MVCProject_API.Services.Interfaces;

namespace MVCProject_API.Controllers.Admin
{
    public class InstructorController : BaseAdminController
    {
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;
        public InstructorController(IInstructorService instructorService,IMapper mapper)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InstructorCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _instructorService.ExistEmail(request.Email))
            {
                return BadRequest("Email is already taken");
            }

            await _instructorService.Create(request);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] InstructorEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var instructor = await _instructorService.GetById(id);

            if (instructor is null) return NotFound();

            if (await _instructorService.ExistEmail(request.Email))
            {
                return BadRequest("Email is already taken");
            }

            await _instructorService.Edit(instructor, request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var instructor = await _instructorService.GetById(id);

            if (instructor is null) return NotFound();

            await _instructorService.Delete(instructor);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var instructor = await _instructorService.GetById(id);

            if (instructor is null) return NotFound();

            return Ok(_mapper.Map<InstructorDetailDto>(instructor));
        }
    }
}
