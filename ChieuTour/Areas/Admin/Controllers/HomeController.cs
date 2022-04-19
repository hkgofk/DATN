using ChieuTour.Models;
using System;
using System.Collections.Generic;
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
            var notification = db.DonHangs.Where(d => d.TinhTrang.Equals("Đang xử lý")).Select(d => d);
            if (notification.Count()!= 0)
            {
                TempData["info"] = "Có " + notification.Count() + " đơn hàng mới đang chờ xử lý";

            }

            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
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
                var nguoiDungs = db.NguoiDungs.Where(n => n.TaiKhoan.Equals(user.TaiKhoan) && n.MatKhau.Equals(user.MatKhau) && n.IdQuyen == 1);
                if(nguoiDungs.Count() > 0)
                {
                    Session["HoTen"] = nguoiDungs.FirstOrDefault().HoTen;
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