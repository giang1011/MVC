using System.ComponentModel.DataAnnotations;

namespace StudentCourseApp.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = "";

        public string? Description { get; set; }

       
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
