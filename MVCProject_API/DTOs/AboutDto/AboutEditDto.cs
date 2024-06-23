using FluentValidation;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MVCProject_API.DTOs.AboutDto
{
    public class AboutEditDto
    {
        public IFormFile NewImage { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public string Image { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
    }

    public class AboutEditDtoValidator : AbstractValidator<AboutEditDto>
    {
        public AboutEditDtoValidator()
        {
            RuleFor(m => m.Heading).NotNull().NotEmpty().WithMessage("Heading is required");
            RuleFor(m => m.Description).NotNull().NotEmpty().WithMessage("Description is required");
        }
    }
}
