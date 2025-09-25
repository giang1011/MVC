using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseApp.Data;
using StudentCourseApp.Models;

namespace StudentCourseApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _db;
        public StudentController(AppDbContext db) { _db = db; }

        // List all students + courses they registered
        public async Task<IActionResult> Index()
        {
            var students = await _db.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ToListAsync();
            return View(students);
        }

        // GET Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Courses = await _db.Courses.ToListAsync();
            return View();
        }

        // POST Create: bind student + selectedCourseIds from checkboxes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,MiddleName,LastName,Birthday,EClass,Phone,Email")] Student student, int[]? selectedCourseIds)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = await _db.Courses.ToListAsync();
                return View(student);
            }

            _db.Students.Add(student);
            await _db.SaveChangesAsync(); // get student.Id

            if (selectedCourseIds != null && selectedCourseIds.Length > 0)
            {
                foreach (var cid in selectedCourseIds.Distinct())
                {
                    _db.Enrollments.Add(new Enrollment { StudentId = student.Id, CourseId = cid });
                }
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET Edit
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _db.Students
                .Include(s => s.Enrollments)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound();

            ViewBag.Courses = await _db.Courses.ToListAsync();
            ViewBag.SelectedCourseIds = student.Enrollments.Select(e => e.CourseId).ToList();
            return View(student);
        }

        // POST Edit: update properties and enrollments
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,Birthday,EClass,Phone,Email")] Student model, int[]? selectedCourseIds)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = await _db.Courses.ToListAsync();
                ViewBag.SelectedCourseIds = selectedCourseIds ?? new int[0];
                return View(model);
            }

            var student = await _db.Students
                .Include(s => s.Enrollments)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound();

            // Update scalar props
            student.FirstName = model.FirstName;
            student.MiddleName = model.MiddleName;
            student.LastName = model.LastName;
            student.Birthday = model.Birthday;
            student.EClass = model.EClass;
            student.Phone = model.Phone;
            student.Email = model.Email;

            
            var selected = (selectedCourseIds ?? Array.Empty<int>()).Distinct().ToList();

            var toRemove = student.Enrollments.Where(e => !selected.Contains(e.CourseId)).ToList();
            _db.Enrollments.RemoveRange(toRemove);

            var existingCourseIds = student.Enrollments.Select(e => e.CourseId).ToList();
            var toAdd = selected.Except(existingCourseIds).ToList();
            foreach (var cid in toAdd)
            {
                _db.Enrollments.Add(new Enrollment { StudentId = student.Id, CourseId = cid });
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _db.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound();
            return View(student);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrolls = _db.Enrollments.Where(e => e.StudentId == id);
            _db.Enrollments.RemoveRange(enrolls);

            var s = await _db.Students.FindAsync(id);
            if (s != null) _db.Students.Remove(s);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
