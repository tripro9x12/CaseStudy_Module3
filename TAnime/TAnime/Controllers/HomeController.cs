using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TAnime.Models;
using TAnime.Models.ViewModels.Movies;
using TAnime.Repositories;
using TAnime.Services;
using TAnime.Services_Repository;
using X.PagedList;
namespace TAnime.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimeRepository animeRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ICountryRepository countryRepository;
        private static string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };
        public static string RemoveSign4VietnameseString(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        public HomeController(ILogger<HomeController> logger,
                              IAnimeRepository animeRepository,
                               ICategoryRepository categoryRepository,
                               ICountryRepository countryRepository)
        {
            _logger = logger;
            this.animeRepository = animeRepository;
            this.categoryRepository = categoryRepository;
            this.countryRepository = countryRepository;
        }

        public IActionResult Index(int? page)
        {           
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            var movies = animeRepository.GetMovies().ToList().ToPagedList(pageNumber, pageSize);        
            return View(movies);
        }

        public List<MovieViewModel> GetMovieIsNotFinish()
        {
            var isFinish = animeRepository.GetMovies().Where(m => m.IsFinish == false).ToList();
            List<MovieViewModel> newmovies = new List<MovieViewModel>();
            foreach (var item in isFinish)
            {
                if (newmovies.Count < 8)
                {
                    newmovies.Add(item);
                }
            }
            return newmovies;
        }
        [HttpGet]
        [Route("/Home/IndexOfSlides")]
        public JsonResult IndexOfSlides()
        {
            var movies = GetMovieIsNotFinish();
            return Json(new { data =  movies });
        }


        [Route("Home/IndexMovieOfCateGory/{categoryId}")]
        public IActionResult IndexMovieOfCateGory(int categoryId)
        {
            var movies = animeRepository.GetMoviesOfCategory(categoryId).ToList();
            return View(movies);
        }


        [Route("Home/IndexMovieOfCountry/{countryId}")]
        public IActionResult IndexMovieOfCountry(int countryId)
        {
            var movies = animeRepository.GetMoviesOfCountry(countryId).ToList();
            return View(movies);
        }

        [Route("Home/IndexMovieOfFinish/{finishId}")]
        public IActionResult IndexMovieOfFinish(int finishId)
        {
            var movies = animeRepository.GetMoviesOfFinish(finishId).ToList();
            return View(movies);
        }
        [Route("Home/IndexMovieOfView")]
        public IActionResult IndexMovieOfView(int? page)
        {
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            var movies = animeRepository.GetMoviesOfView().ToPagedList(pageNumber, pageSize);
            return View(movies);
        }

        public IActionResult Detail(int? id)
        {
            try
            {
                int.Parse(id.Value.ToString());
                var anime = animeRepository.GetMovieViewModel(id.Value);
                if (anime == null)
                {
                    return View();
                }
                return View(anime);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [Route("Home/DetailVideos/{movieId}")]
        public IActionResult DetailVideos(int movieId)
        {
            var movie = animeRepository.GetMovieViewModel(movieId);
            var moviesByCountry = animeRepository.GetMoviesOfCountry(movie.CountryId).ToList();
            var movies = new List<MovieViewModel>();
            if (moviesByCountry.Count >= 8)
            {
               foreach(var item in moviesByCountry)
                {
                    if(movies.Count < 8 && movie.MovieName != item.MovieName)
                    {
                        movies.Add(item);
                    }
                }             
            }
            else
            {
                foreach (var item in moviesByCountry)
                {
                    if (movie.MovieName != item.MovieName)
                    {
                        movies.Add(item);
                    }
                }
            }      
            ViewBag.MoviesByCountry = movies;
            return View(movie);
        }

        public IActionResult Search(string searchString)
        {
            var links = animeRepository.GetMovies().ToList();
            if (!string.IsNullOrEmpty(searchString))
            {

                //links = links.Where(s => s.MovieName.ToLower().Contains(searchString.ToLower())).ToList();
                List<MovieViewModel> newList = new List<MovieViewModel>();
                foreach(var item in links)
                {
                    string name = item.MovieName.Trim().ToLower();
                    name = RemoveSign4VietnameseString(name);
                    string search = searchString.Trim().ToLower();
                    search = RemoveSign4VietnameseString(search);
                    bool b = name.Contains(search);
                    if (b)
                    {
                        newList.Add(item);
                    }
                }
                return View(newList);
            }
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
