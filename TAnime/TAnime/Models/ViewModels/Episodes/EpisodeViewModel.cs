using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.ViewModels.Episodes
{
    public class EpisodeViewModel
    {
        public int EpisodeId { get; set; }
        public string VideoPath { get; set; }
        public string EpisodeCode { get; set; }
        public string EpisodeMovie { get; set; }
        public string _MovieName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTime  { get; set; }
    }
}
