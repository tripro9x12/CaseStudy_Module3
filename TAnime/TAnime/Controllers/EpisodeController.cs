﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Episodes;
using TAnime.Models.ViewModels.Movies;
using TAnime.Repositories;
using TAnime.Services;

namespace TAnime.Controllers
{
    public class EpisodeController : Controller
    {
        private readonly IEpisodeRepository episodeRepository;
        private readonly IAnimeRepository animeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EpisodeController(IEpisodeRepository episodeRepository,
                                IAnimeRepository animeRepository,
                                IWebHostEnvironment webHostEnvironment)
        {
            this.episodeRepository = episodeRepository;
            this.animeRepository = animeRepository;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var episodes = episodeRepository.GetEpisodes();
            return View(episodes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Movies = GetMovies();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEpisodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var episodes = episodeRepository.GetEpisodes().ToList();
                foreach(var item in episodes)
                {
                    if(item.EpisodeCode == model.EpisodeCode)
                    {
                        ModelState.AddModelError("", "Mã phim đã tồn tại");
                        return View();
                    }
                }
                var episode = new Episode()
                {
                    EpisodeCode = model.EpisodeCode,
                    EpisodeMovie = model.EpisodeMovie,
                    _MovieId = model._MovieId,
                    DateTime = model.DateTime
                };
                var fileName = string.Empty;
                if(model.Video != null)
                {
                    var uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "Videos");
                    fileName = model.Video.FileName;
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using(var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Video.CopyTo(fs);
                    }
                }
                episode.VideoPath = fileName;
                if(episodeRepository.CreateEpisode(episode) > 0)
                {
                    return RedirectToAction("Index", "Episode");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Movies = GetMovies();
            var episode = episodeRepository.GetEpisode(id);
            var editEpisode = new EditEpisodeViewModel()
            {
                EpisodeId = episode.EpisodeId,
                EpisodeMovie = episode.EpisodeMovie,
                EpisodeCode = episode.EpisodeCode,
                DateTime = episode.DateTime,
                VideoPath = episode.VideoPath,
                _MovieId = episode._MovieId
            };
            return View(editEpisode);
        }
        [HttpPost]
        public IActionResult Edit(EditEpisodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var episodes = episodeRepository.GetEpisodes().ToList();
                foreach (var item in episodes)
                {
                    if (item.EpisodeCode == model.EpisodeCode)
                    {
                        ModelState.AddModelError("", "Mã phim đã tồn tại");
                        return View();
                    }
                }
                var episode = new Episode()
                {
                    EpisodeCode = model.EpisodeCode,
                    EpisodeMovie = model.EpisodeMovie,
                    _MovieId = model._MovieId,
                    DateTime = model.DateTime
                };
                var fileName = string.Empty;
                if (model.Video != null)
                {
                    var uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "Videos");
                    fileName = model.Video.FileName;
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Video.CopyTo(fs);
                    }
                }
                episode.VideoPath = fileName;
                if (!string.IsNullOrEmpty(model.VideoPath))
                {
                    var delFile = Path.Combine(webHostEnvironment.WebRootPath, "images", model.VideoPath);
                    System.IO.File.Delete(delFile);
                }
                if (episodeRepository.EditEpisode(episode) > 0)
                {
                    return RedirectToAction("Index", "Episode");
                }
            }
            return View();
        }
        [Route("Episode/Delete/{EpisodeId}")]
        public IActionResult Delete(int EpisodeId)
        {
            var delEpisode = episodeRepository.DeleteEpisode(EpisodeId);
            return Json(new { delEpisode });
        }

        public List<Movies> GetMovies()
        {
            return animeRepository.GetListMovies();
        }
    }
}
