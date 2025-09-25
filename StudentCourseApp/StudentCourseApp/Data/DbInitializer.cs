using StudentCourseApp.Models;

namespace StudentCourseApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
          

            if (!context.Courses.Any())
            {
                var courses = new List<Course>
                {
                    new Course { Name = "Mathematics 101", Description = "Basic Math" },
                    new Course { Name = "Physics 101", Description = "Intro to Physics" },
                    new Course { Name = "Computer Science", Description = "Programming fundamentals" },
                    new Course { Name = "English Literature", Description = "Classic literature" },
                    new Course { Name = "History", Description = "World history" }
                };
                context.Courses.AddRange(courses);
                context.SaveChanges();
            }
        }
    }
}
