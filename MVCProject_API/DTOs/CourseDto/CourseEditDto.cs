using FluentValidation;
using MVCProject_API.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MVCProject_API.DTOs.CourseDto
{
    public class CourseEditDto
    {
        public string Name { get; set; }
        public List<IFormFile> NewImages { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public List<CourseImage> CourseImages { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int InstructorId { get; set; }
        public string InstructorFullName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CourseEditDtoValidator : AbstractValidator<CourseEditDto>
    {
        public CourseEditDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.CategoryName).NotEmpty();
            RuleFor(m => m.Price).NotEmpty();
            RuleFor(m => m.Rating).NotEmpty();
            RuleFor(m => m.InstructorFullName).NotEmpty();
            RuleFor(m => m.StartDate).NotEmpty();
            RuleFor(m => m.EndDate).NotEmpty();
        }
    }
}
