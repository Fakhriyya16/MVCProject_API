using FluentValidation;
using MVCProject_API.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MVCProject_API.DTOs.InstructorDto
{
    public class InstructorCreateDto
    {
        public string FullName { get; set; }
        public IFormFile ImageFile { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public string Image { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
    }

    public class InstructorCreateDtoValidator : AbstractValidator<InstructorCreateDto>
    {
        public InstructorCreateDtoValidator() 
        {
            RuleFor(m => m.FullName).NotEmpty();
            RuleFor(m => m.ImageFile).NotEmpty();
            RuleFor(m => m.Position).NotEmpty();
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
        }
    }
}
