using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;

namespace TAnime.Models.ViewModels.Movies
{
    public class CreateMovie
    {
        public int MovieId { get; set; }

        public string MovieName { get; set; }
        public string Imagepath { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public int Country { get; set; }
        public List<int> categories { get; set; }
    }
}
