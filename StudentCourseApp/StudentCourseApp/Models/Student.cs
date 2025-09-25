using System.ComponentModel.DataAnnotations;

namespace StudentCourseApp.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; } = "";

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required, StringLength(20)]
        public string EClass { get; set; } = "";

        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        // Navigation
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
