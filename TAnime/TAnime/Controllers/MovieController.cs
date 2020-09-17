using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Categories;
using TAnime.Models.ViewModels.Movies;
using TAnime.Services;
using TAnime.Services_Repository;

namespace TAnime.Controllers
{
    public class MovieController : Controller
    {
        private readonly IAnimeService animeService;
        private readonly ICategoryRepository categoryRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MovieController(IAnimeService animeService, 
                               ICategoryRepository categoryRepository,
                               IWebHostEnvironment webHostEnvironment)
        {
            this.animeService = animeService;
            this.categoryRepository = categoryRepository;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<MovieViewModel> movies = new List<MovieViewModel>();
            movies = animeService.GetMovies().ToList();
            return View(movies);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategories();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateMovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = new CreateMovie()
                {
                    MovieName = model.MovieName,
                    Content = model.Content,
                    Time = model.Time,
                    Country = model.Country,
                    categories = model.categories
                };
                var fileName = string.Empty;
                if(model.Image != null)
                {
                    var UploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    fileName = $"{Guid.NewGuid()}_{model.Image.FileName}";
                    var filePath = Path.Combine(UploadFolder, fileName);
                    using(var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(fs);
                    }
                }
                movie.Imagepath = fileName;
                var newMovie = animeService.CreateMovie(movie);
                return RedirectToAction("Index", "Movie");

            }

            return View(model);
        }

        private List<CategoryViewModel> GetCategories()
        {
            return categoryRepository.Gets().ToList();
        }
    }
}
