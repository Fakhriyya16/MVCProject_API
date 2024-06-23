using MVCProject_API.Models;

namespace MVCProject_API.DTOs.CourseDto
{
    public class CourseDetailDto
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public List<string> CourseImages { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InstructorName { get; set; }
        public int StudentCount { get; set; }
    }
}
