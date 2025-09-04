using Microsoft.AspNetCore.Mvc;
using VietNam.Models;
using System.Collections.Generic;
using System.Linq;

namespace VietNam.Controllers
{
    public class AptechController : Controller
    {
        [Route("aptech")]
        public IActionResult Index()
        {
            ViewBag.Address = "Aptech Viet Nam - 285Doi Can, Ba Đinh, Ha Noi";
            return View();
        }

        [Route("aptech/student")]
        public IActionResult StudentList()
        {
            var students = new List<Student>
            {
                new Student{Id=1, Name="Hoang Duy Hieu", Age=20},
                new Student{Id=2, Name="Nguyen Duc Minh", Age=21},
                new Student{Id=3, Name="Luoi Qua Di", Age=22},
            };
            return View(students);
        }

        [Route("aptech/student/{id}")]
        public IActionResult StudentDetail(int id)
        {
            var students = new List<Student>
            {
                new Student{Id=1, Name="Hoang Duy Hieu", Age=20},
                new Student{Id=2, Name="Nguyen Duc Minh", Age=21},
                new Student{Id=3, Name="Luoi Qua Di", Age=22},
            };

            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();

            return View(student);
        }
    }
}
