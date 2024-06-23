using FluentValidation;
using MVCProject_API.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MVCProject_API.DTOs.StudentDto
{
    public class StudentCreateDto
    {
        public string FullName { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Bio { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int CourseId { get; set; }
        public string Course { get; set; }
    }

    public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
    {
        public StudentCreateDtoValidator()
        {
            RuleFor(m => m.FullName).NotEmpty();
            RuleFor(m => m.ImageFile).NotEmpty();
            RuleFor(m => m.Bio).NotEmpty();
            RuleFor(m => m.Course).NotEmpty();
        }
    }
}
