using FluentValidation;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MVCProject_API.DTOs.AboutDto
{
    public class AboutCreateDto
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public string Image {  get; set; }
    }

    public class AboutCreateDtoValidator : AbstractValidator<AboutCreateDto>
    {
        public AboutCreateDtoValidator()
        {
            RuleFor(m=>m.Heading).NotNull().NotEmpty().WithMessage("Heading is required");
            RuleFor(m=>m.Description).NotNull().NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.ImageFile).NotNull().WithMessage("Image is required.")
                                 .Must(file => file.Length <= 500 * 1024).WithMessage("Image size must be less than 5 MB.")
                                 .Must(file => file.ContentType.StartsWith("image/")).WithMessage("Invalid file type. Only image files are allowed.");
        }
    }
}
