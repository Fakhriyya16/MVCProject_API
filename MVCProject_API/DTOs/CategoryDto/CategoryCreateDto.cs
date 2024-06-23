using FluentValidation;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MVCProject_API.DTOs.CategoryDto
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public string Image {  get; set; }
    }

    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto> 
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(m=>m.Name).NotEmpty().NotNull().WithMessage("Name is required");
            RuleFor(m=>m.ImageFile).NotEmpty().NotNull().WithMessage("Image is required");
        }
    }
}
