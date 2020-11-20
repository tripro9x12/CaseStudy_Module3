using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.Identities
{
    public class EditUserViewModel
    {
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "FullName không được để trống")]
        [Display(Name = "Họ Tên")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Thành phố không được để trống")]
        [Display(Name = "Thành phố")]
        public string City { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        public IFormFile ImagePath { get; set; }
        public string Avatar { get; set; }
        [Display(Name ="Role Name")]
        public string RoleId { get; set; }
    }
}
