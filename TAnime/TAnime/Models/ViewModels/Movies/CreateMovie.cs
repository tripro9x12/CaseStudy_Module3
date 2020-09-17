using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.ViewModels.Movies
{
    public class CreateMovie
    {
        public string MovieName { get; set; }
        public string Imagepath { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public string Country { get; set; }
        public List<int> categories { get; set; }
    }
}
