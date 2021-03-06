﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;

namespace TAnime.Models.ViewModels.Movies
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Content { get; set; }
        public string ImageOfVideo { get; set; }
        public string Country { get; set; }
        public DateTime Time { get; set; }
        public List<Category> Categories { get; set; }
        public List<Episode> Episodes { get; set; }
        public int CountryId { get; set; }
        public int View { get; set; }
        public bool IsFinish { get; set; }
    }
}
