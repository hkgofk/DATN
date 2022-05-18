using ChieuTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChieuTour.Controllers
{
    public class DangKyController : Controller
    {
        private ChieuTourDBContext db = new ChieuTourDBContext();
        // GET: DangKy
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection, NguoiDung kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];

            try
            {
                if (String.IsNullOrEmpty(hoten))
                {
                    ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
                }
                else if (string.IsNullOrEmpty(tendn))
                {
                    ViewData["Loi2"] = "Phải nhập tên đăng nhập";
                }
                else if (string.IsNullOrEmpty(matkhau))
                {
                    ViewData["Loi3"] = "Phải nhập mật khẩu";
                }
                else if (string.IsNullOrEmpty(matkhaunhaplai))
                {
                    ViewData["Loi4"] = "Phải nhập lại mật khẩu";
                }
                else if (string.IsNullOrEmpty(diachi))
                {
                    ViewData["Loi5"] = "Địa chỉ không được để trống";
                }
                else if (string.IsNullOrEmpty(email))
                {
                    ViewData["Loi6"] = "Email không được để trống";
                }
                else if (string.IsNullOrEmpty(dienthoai))
                {
                    ViewData["Loi7"] = "Phải nhập số điện thoại";
                }
                else
                {
                    kh.HoTen = hoten;
                    kh.TaiKhoan = tendn;
                    kh.MatKhau = matkhau;
                    kh.Email = email;
                    kh.DiaChi = diachi;
                    kh.SDT = dienthoai;
                    kh.IdQuyen = 2;
                    db.NguoiDungs.Add(kh);
                    db.SaveChanges();
                    TempData["success"] = "Đăng ký thành công!";
                    return RedirectToAction("Index", "NguoiDung");
                }
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Đăng ký không thành công!";
            }
            return View();
        }
    }
}