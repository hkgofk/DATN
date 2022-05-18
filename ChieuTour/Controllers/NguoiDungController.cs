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
                var nguoiDung = db.Database.SqlQuery<User>($"loginCustomer '{tendn}', '{matkhau}'").ToList();
                if (nguoiDung.Count() > 0)
                {
                    NguoiDung kh = db.NguoiDungs.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
                    TempData["success"] = "Đăng nhập thành công";
                    Session["TaiKhoan"] = kh;
                    Session["Tendangnhap"] = kh.HoTen;
                    return RedirectToAction("Index", "Home");
                }
                else TempData["error"] = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        public ActionResult Detail()
        {
            NguoiDung nguoiDung = (NguoiDung)Session["TaiKhoan"];

            return View(nguoiDung);
        }
        public ActionResult Edit()
        {
            NguoiDung nguoiDung = (NguoiDung)Session["TaiKhoan"];

            return View(nguoiDung);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NguoiDung nguoiDung, string matKhauXacNhan)
        {
            if (nguoiDung.MatKhau == matKhauXacNhan)
            {
                db.Entry(nguoiDung).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Cập nhật thông tin thành công!";
                Session["TaiKhoan"] = nguoiDung;
                Session["Tendangnhap"] = nguoiDung.HoTen;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Mật khẩu bạn nhập không đúng!";
            }
            return View(nguoiDung);
        }

        public ActionResult logout()
        {
            Session["Tendangnhap"] = null;

            return RedirectToAction("Index", "Home");
        }
        public ActionResult changePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult changePassword(string oldPass, string newPass, string confirmPass)
        {
            NguoiDung nguoiDungChange = (NguoiDung)Session["TaiKhoan"];
            NguoiDung nguoiDung = db.NguoiDungs.Where(n => n.MaNguoiDung == nguoiDungChange.MaNguoiDung).FirstOrDefault();

            if (oldPass != nguoiDung.MatKhau)
            {
                TempData["error"] = "Mật khẩu cũ không chính xác!";
                return View();
            }
            else if (newPass != confirmPass)
            {
                TempData["error"] = "Mật khẩu mới bạn nhập không trùng nhau!";
                return View();
            }
            else if (nguoiDung.MatKhau == oldPass)
            {
                nguoiDung.MatKhau = newPass;
                db.Entry(nguoiDung).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Session["TaiKhoan"] = nguoiDung;
                TempData["success"] = "Đổi mật khẩu thành công!";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}