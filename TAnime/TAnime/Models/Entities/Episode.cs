using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.Entities
{
    public class Episode
    {
        public int EpisodeId { get; set; }
        [MaxLength(200)]
        [Required]
        public string VideoPath { get; set; }
        [MaxLength(50)]
        public string EpisodeCode { get; set; }
        [MaxLength(50)]
        public string EpisodeMovie { get; set; }
        [Display(Name ="Ngày Đăng")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
        [Required]
        public int _MovieId { get; set; }
        public Movie _Movie { get; set; }
    }
}
