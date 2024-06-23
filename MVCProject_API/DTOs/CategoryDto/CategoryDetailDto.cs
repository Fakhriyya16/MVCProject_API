using MVCProject_API.Models;

namespace MVCProject_API.DTOs.CategoryDto
{
    public class CategoryDetailDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Course> Courses { get; set; }
        public string CreatedDate { get; set; }
    }
}
