using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCProject_API.DTOs.AboutDto;
using MVCProject_API.Helpers.Extensions;
using MVCProject_API.Models;
using MVCProject_API.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace MVCProject_API.Controllers.Admin
{
    public class AboutController : BaseAdminController
    {
        private readonly IAboutService _aboutService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public AboutController(IAboutService aboutService, IWebHostEnvironment env,IMapper mapper)
        {
            _aboutService = aboutService;
            _env = env;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var result = await _aboutService.GetById(id);

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AboutCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string fileName = request.ImageFile.FileName.FileNameGenerator();

            string path = _env.GenerateFilePath("img", fileName);

            await request.ImageFile.SaveToFileAsync(path);
            request.Image = fileName;

            await _aboutService.Create(request);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] AboutEditDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var about = await _aboutService.GetById((int)id);

            if (about is null) return NotFound();

            await _aboutService.Edit(_mapper.Map<About>(about), request);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var about = await _aboutService.GetById((int)id);

            if (about is null) return NotFound();

            await _aboutService.Delete(_mapper.Map<About>(about));
            return Ok();
        }
    }
}
