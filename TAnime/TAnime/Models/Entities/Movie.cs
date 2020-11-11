using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        [MaxLength(300)]
        [Required]
        public string MovieName { get; set; }
        public string Content { get; set; }
        [MaxLength(300)]
        public string ImageOfVideo { get; set; }
        [Required]
        [DefaultValue("false")]
        public bool isFinish { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        [Required]
        [DefaultValue("false")]
        public bool IsDelete { get; set; }
        [Required]
        [DefaultValue("0")]
        public int View { get; set; }
        public ICollection<MovieCategory> movieCategories { get; set; }
        public ICollection<Episode> Episode { get; set; }
        public int _CountryId { get; set; }
        public Country _Country { get; set; }
    }
}
