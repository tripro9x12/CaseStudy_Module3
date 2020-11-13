using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Categories;
using TAnime.Models.ViewModels.Country;
using TAnime.Models.ViewModels.Movies;
using TAnime.Repositories;
using TAnime.Services;
using TAnime.Services_Repository;

namespace TAnime.Controllers
{
    
    public class MovieController : Controller
    {
        private readonly IAnimeRepository animeRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICountryRepository countryRepository;

        public MovieController(IAnimeRepository animeRepository, 
                               ICategoryRepository categoryRepository,
                               IWebHostEnvironment webHostEnvironment,
                               ICountryRepository countryRepository)
        {
            this.animeRepository = animeRepository;
            this.categoryRepository = categoryRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.countryRepository = countryRepository;
        }
        public IActionResult Index()
        {
            var movies = new List<MovieViewModel>();
            movies = animeRepository.GetMovies().ToList();
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
                    Country = model.CountryId,
                    categories = model.categories,
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
                var newMovie = animeRepository.CreateMovie(movie);
                return RedirectToAction("Index", "Movie");

            }

            return View(model);
        }
        public IActionResult Detail(int? id)
        {
            try
            {
                int.Parse(id.Value.ToString());
                var anime = animeRepository.GetMovieViewModel(id.Value);
                if(anime == null)
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
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var anime = animeRepository.GetMovieView(id);
            var editAnime = new EditMovieViewModel()
            {
                MovieId = anime.MovieId,
                MovieName = anime.MovieName,
                ImageOfVideo = anime.ImageOfVideo,
                Content = anime.Content,
                Time = anime.Time,
                CountryId = anime._CountryId,
                categories = anime.Categories,
                isFinish = anime.IsFinish,
                View = anime.View
            };
            ViewBag.Categories = GetCategories();
            ViewBag.Coutries = GetCoutries();
            return View(editAnime);
        }
        [HttpPost]
        public IActionResult Edit(EditMovieViewModel model)
        {
            model.isFinish = bool.Parse(model.isFinish.ToString());
            if (ModelState.IsValid)
            {
                var fileName = string.Empty;
                if(model.Image != null)
                {
                    if(model.ImageOfVideo != null)
                    {
                        var delFile = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ImageOfVideo);
                        System.IO.File.Delete(delFile);
                    }
                    var UploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    fileName = $"{Guid.NewGuid()}_{model.Image.FileName}";
                    var filePath = Path.Combine(UploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(fs);
                    }
                    model.ImageOfVideo = fileName;
                }
                if(animeRepository.EditMovie(model) > 0)
                {
                    return RedirectToAction("Index", "Movie");
                }
            }
            return View(model);
        }
        [Route("Movie/Delete/{MovieId}")]
        public JsonResult Delete(int MovieId)
        {

            var delMovie = animeRepository.DeleteMovie(MovieId);
            return Json(new { delMovie });

        }

        [Route("/Movie/ChangeIsFinish/{id}/{isFinish}")]
        public JsonResult ChangeIsFinish(int id, string isFinish)
        {
            bool isStatus;
            if(isFinish.ToLower() == "false")
            {
                isStatus = true;
            }else if(isFinish.ToLower() == "true")
            {
                isStatus = false;
            }
            else
            {
                isStatus = false;
            }
            
            var change = animeRepository.changeFinish(id, isStatus);
            return Json(new { change });
        }
        [Route("/Movie/UpdateView/{id}")]
        public JsonResult UpdateView(int id)
        {
            var updateMovie = animeRepository.UpdateView(id);
            return Json(new { updateMovie });
        }

        private List<CategoryViewModel> GetCategories()
        {
            return categoryRepository.Gets().ToList();
        }
        private List<CountryViewModel> GetCoutries()
        {
            return countryRepository.GetCountries().ToList();
        }
    }
}
