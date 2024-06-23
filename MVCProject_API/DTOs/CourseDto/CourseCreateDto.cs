using FluentValidation;
using MVCProject_API.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MVCProject_API.DTOs.CourseDto
{
    public class CourseCreateDto
    {
        public string Name { get; set; }
        public List<IFormFile> ImageFiles { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public List<CourseImage> CourseImages { get; set; }
        public string CategoryName { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int CategoryId { get; set; }
        public string Price { get; set; }
        public int Rating { get; set; }
        public string InstructorFullName { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int? InstructorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CourseCreateDtoValidator : AbstractValidator<CourseCreateDto>
    {
        public CourseCreateDtoValidator()
        {
            RuleFor(m=>m.Name).NotEmpty();
            RuleFor(m=>m.ImageFiles).NotEmpty();
            RuleFor(m=>m.CategoryName).NotEmpty();
            RuleFor(m=>m.Price).NotEmpty();
            RuleFor(m=>m.Rating).NotEmpty().InclusiveBetween(1,5);
            RuleFor(m=>m.StartDate).NotEmpty();
            RuleFor(m=>m.EndDate).NotEmpty();
        }
    }
}
