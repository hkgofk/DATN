using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChieuTour.Models
{
    public class User
    {
        [Required(ErrorMessage = "Tên tài khoản không được để trống!")]
        public string TaiKhoan { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        public string MatKhau { get; set; }
    }
}