using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.Identities
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        [Display(Name ="Mật khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Xác nhận Mật khẩu không giống")]
        [Display(Name = "Xác Nhận Mật khẩu")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Thành phố không được để trống")]
        [Display(Name = "Thành phố")]
        public string City { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        public IFormFile ImagePath { get; set; }
    }
}
