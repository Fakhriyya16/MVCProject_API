using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCProject_API.DTOs.CategoryDto;
using MVCProject_API.Models;
using MVCProject_API.Services.Interfaces;

namespace MVCProject_API.Controllers.Admin
{
    public class CategoryController : BaseAdminController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var category = await _categoryService.GetById(id);
            
            return Ok(_mapper.Map<CategoryDetailDto>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if (await _categoryService.ExistCategory(request.Name))
            {
                return BadRequest("Category already exists");
            }

            await _categoryService.Create(request);
            
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] CategoryEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _categoryService.GetById(id);

            if (category is null) return NotFound();

            if (await _categoryService.ExistCategory(request.Name))
            {
                return BadRequest("Category already exists");
            }

            await _categoryService.Edit(category, request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var category = await _categoryService.GetById(id);

            if (category is null) return NotFound();

            await _categoryService.Delete(category);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> CheckIfExists(string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest(new { error = "Input cannot be empty" });
            return Ok(await _categoryService.ExistCategory(name));
        }
    }
}
