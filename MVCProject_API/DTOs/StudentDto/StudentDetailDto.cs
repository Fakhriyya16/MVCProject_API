using MVCProject_API.Models;

namespace MVCProject_API.DTOs.StudentDto
{
    public class StudentDetailDto
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public List<string> Courses { get; set; }
    }
}
