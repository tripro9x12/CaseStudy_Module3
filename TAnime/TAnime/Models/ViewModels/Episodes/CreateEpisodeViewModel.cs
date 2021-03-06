﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.ViewModels.Episodes
{
    public class CreateEpisodeViewModel
    {
        [Display(Name = "Đường dẫn phim")]
        public string VideoPath { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name ="Mã Tập Phim")]
        public string EpisodeCode { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name ="Tập Phim")]
        public string EpisodeMovie { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name ="Tên Bộ Phim")]
        public int _MovieId { get; set; }
        [Display(Name = "Ngày Đăng")]
        [Required(ErrorMessage = "Không được để trống")]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
    }
}
