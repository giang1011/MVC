using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace VietNam.Controllers
{
    public class VietnamController : Controller
    {
  
        [Route("Vietnam/thanhpho")]
        public IActionResult ThanhPho()
        {
            var cities = new List<string> { "Hà Nội", "TP Hồ Chí Minh", "Đà Nẵng", "Huế", "Cần Thơ" };
            return View(cities);
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.Info = "Việt Nam là một quốc gia ở Đông Nam Á với hơn 96 triệu dân, " +
                           "có vị trí địa lý thuận lợi, con người thân thiện, nền văn hóa đa dạng và phong phú.";
            return View();
        }
    }
}
