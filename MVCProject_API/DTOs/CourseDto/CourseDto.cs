namespace MVCProject_API.DTOs.CourseDto
{
    public class CourseDto
    {
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public string Name { get; set; }
        public string Instructor { get; set; }
        public string Category { get; set; }
        public int StudentCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
