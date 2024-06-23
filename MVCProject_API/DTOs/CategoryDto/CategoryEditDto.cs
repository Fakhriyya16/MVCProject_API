using FluentValidation;
using Swashbuckle.AspNetCore.Annotations;

namespace MVCProject_API.DTOs.CategoryDto
{
    public class CategoryEditDto
    {
        public string Name { get; set; }
        public IFormFile NewImage { get; set; }
        [SwaggerSchema(ReadOnly =true)]
        public string Image { get; set; }
    }

    public class CategoryEditDtoValidator : AbstractValidator<CategoryEditDto>
    {
        public CategoryEditDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
