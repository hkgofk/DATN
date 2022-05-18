using ChieuTour.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChieuTour.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private ChieuTourDBContext db = new ChieuTourDBContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var danhMuc = db.DanhMucs.Select(d => d).Count();
            var tour = db.Tours.Select(t => t).Count();
            var donHang = db.DonHangs.Where(d => d.TinhTrang == "Đã xác nhận").Select(d => d).Count();
            var tinTuc = db.TinTucs.Select(tt => tt).Count();

            ViewBag.DanhMuc = danhMuc;
            ViewBag.Tour = tour;
            ViewBag.DonHang = donHang;
            ViewBag.TinTuc = tinTuc;


            var notification = db.DonHangs.Where(d => d.TinhTrang.Equals("Đang xử lý")).Select(d => d);
            if (notification.Count()!= 0)
            {
                TempData["info"] = "Có " + notification.Count() + " đơn hàng mới đang chờ xử lý";

            }

            //var khaibao = new SqlParameter("@nam", 2022);


            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult GetReportByYear(int year)
        {
            var thongKe = db.Database.SqlQuery<ThongKe>($"thongKe {year}").ToList();

            return Json(thongKe, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                //var nguoiDungs = db.NguoiDungs.Where(n => n.TaiKhoan.Equals(user.TaiKhoan) && n.MatKhau.Equals(user.MatKhau) && n.IdQuyen == 1);
                var nguoiDung = db.Database.SqlQuery<User>($"loginAdmin '{user.TaiKhoan}', '{user.MatKhau}'").ToList();
                if (nguoiDung.Count() > 0)
                {
                    var nguoiDungR = db.NguoiDungs.Where(n => n.TaiKhoan.Equals(user.TaiKhoan));
                    Session["HoTen"] = nguoiDungR.FirstOrDefault().HoTen;
                    //TempData["success"] = "Đăng nhập thành công!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["HoTen"] = null;
            //TempData["success"] = "Đăng xuất thành công!";
            return RedirectToAction("Index");
        }

    }
}