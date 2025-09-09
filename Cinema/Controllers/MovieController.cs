using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Cinema.Models;

namespace Cinema.Controllers
{
    public class MovieController : Controller
    {
        private List<Movie> movies = new List<Movie>
        {
            new Movie
            {
                Id=1,
                Title="Avengers: Endgame",
                Description="Trận chiến cuối cùng chống lại Thanos.",
                Price=120000,
                Director="Anthony & Joe Russo",
                ImageUrl="/Images/images.jpg",
                TrailerUrl="https://youtu.be/TcMBFSGVi1c",
                WatchUrl="https://www.rophim.me/xem-phim/avengers-hoi-ket.DBGp2MwB?ver=1"
            },
           
            new Movie
            {
                Id=2,
                Title="Interstellar",
                Description="Hành trình vượt không gian tìm hành tinh mới.",
                Price=130000,
                Director="Christopher Nolan",
                ImageUrl="/Images/Hay.jpg",
                TrailerUrl="https://youtu.be/zSWdZVtXT7E",
                WatchUrl="https://www.rophim.me/xem-phim/giua-cac-vi-sao.1b92Ub4P?ver=1"
            },
            new Movie
            {
                Id=3,
                Title="The Dark Knight",
                Description="Batman đối đầu Joker.",
                Price=110000,
                Director="Christopher Nolan",
                ImageUrl="/Images/batman.jpg",
                TrailerUrl="https://youtu.be/EXeTwQWrcwY",
                WatchUrl="https://www.rophim.me/xem-phim/batman-dai-chien-superman-anh-sang-cong-ly.e2YHnqKG?ver=1"
            },
            new Movie
            {
                Id=4,
                Title="Spider-Man: No Way Home",
                Description="Peter Parker gặp các Spider-Man từ vũ trụ khác.",
                Price=125000,
                Director="Jon Watts",
                ImageUrl="/Images/NoMay.jpg",
                TrailerUrl="https://youtu.be/JfVOs4VSpmA",
                WatchUrl="https://www.rophim.me/xem-phim/nguoi-nhen-khong-con-nha.MzsLr11d?ver=1"
            }
        };

        [Route("")]
        [Route("movie")]
        public IActionResult Index()
        {
            return View(movies);
        }

        [Route("movie/detail/{id}")]
        public IActionResult Detail(int id)
        {
            var movie = movies.FirstOrDefault(m => m.Id == id);
            if (movie == null) return NotFound();
            return View(movie);
        }
    }
}
