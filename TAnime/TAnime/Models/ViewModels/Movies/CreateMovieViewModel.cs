using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Categories;

namespace TAnime.Models.ViewModels.Movies
{
    public class CreateMovieViewModel
    {
       
        [MaxLength(300)]
        [Required(ErrorMessage ="Trường này không được để trống")]
        [Display(Name ="Tên Phim")]
        public string MovieName { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        [Display(Name = "Quốc gia")]
        public int Country { get; set; }
        public List<int> categories { get; set; }
        [Display(Name = "Tệp Đính Kèm")]
        public IFormFile Image { get; set; }
        public string ImageOfVideo { get; set; }
        [DataType(DataType.Time)]
        [Display(Name ="Ngày Đăng")]
        public DateTime Time { get; set; }
    }
}
