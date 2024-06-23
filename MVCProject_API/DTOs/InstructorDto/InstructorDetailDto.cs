using MVCProject_API.Models;

namespace MVCProject_API.DTOs.InstructorDto
{
    public class InstructorDetailDto
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public List<string> Courses { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
