using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.Identities
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Mật khẩu cũ không được để trống")]
        [DataType(DataType.Password)]
        [Display(Name ="Mật khẩu cũ")]
        public string oldPassword { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string newPassword { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu mới không được để trống")]
        [Display(Name = "Xác nhận Mật khẩu mới")]
        [DataType(DataType.Password)]
        [Compare("newPassword",ErrorMessage ="Xác nhận mật khẩu mới không giống")]
        public string ConfirmnewPassword { get; set; }
    }
}
