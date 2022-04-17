using ChieuTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChieuTour.Controllers
{
    public class NguoiDungController : Controller
    {
        private ChieuTourDBContext db = new ChieuTourDBContext();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                NguoiDung kh = db.NguoiDungs.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
                if (kh != null)
                {
                    TempData["success"] = "Đăng nhập thành công";
                    Session["TaiKhoan"] = kh;
                    Session["Tendangnhap"] = kh.HoTen;
                    return RedirectToAction("Index", "Home");
                }
                else TempData["error"] = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        public ActionResult logout()
        {
            Session["Tendangnhap"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}