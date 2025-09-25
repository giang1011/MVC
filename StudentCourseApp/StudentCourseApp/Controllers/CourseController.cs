using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseApp.Data;
using StudentCourseApp.Models;

namespace StudentCourseApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _db;
        public CourseController(AppDbContext db) { _db = db; }

        public async Task<IActionResult> Index()
        {
            var list = await _db.Courses.ToListAsync();
            return View(list);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid) return View(course);
            _db.Courses.Add(course);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var c = await _db.Courses.FindAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.Id) return BadRequest();
            if (!ModelState.IsValid) return View(course);
            _db.Courses.Update(course);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var c = await _db.Courses
                .Include(x => x.Enrollments)
                .ThenInclude(e => e.Student)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (c == null) return NotFound();
            return View(c);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var c = await _db.Courses.FindAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            var enrolls = _db.Enrollments.Where(e => e.CourseId == id);
            _db.Enrollments.RemoveRange(enrolls);

            var c = await _db.Courses.FindAsync(id);
            if (c != null) _db.Courses.Remove(c);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
