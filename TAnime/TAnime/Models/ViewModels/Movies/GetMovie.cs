using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;

namespace TAnime.Models.ViewModels.Movies
{
    public class GetMovie
    {
        public int MovieId { get; set; }
        [MaxLength(300)]
        [Required]
        public string MovieName { get; set; }
        public string Content { get; set; }
        [MaxLength(300)]
        public string ImageOfVideo { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public int _CountryId { get; set; }
        public int View { get; set; }
        public bool IsFinish { get; set; }
        public List<int> Categories { get; set; }
    }
}
