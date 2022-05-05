using ChieuTour.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChieuTour.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        private ChieuTourDBContext db = new ChieuTourDBContext();
        // GET: Admin/DonHang
        public ActionResult Index()
        {
            var donHang = db.DonHangs.Include(d => d.NguoiDung);
            donHang = donHang.OrderByDescending(d => d.NgayDat);

            return View(donHang.ToList());
        }
        public ActionResult Detail(int MaDon)
        {
            List<NguoiDung> nguoidung = db.NguoiDungs.ToList();
            List<ChiTietDonHang> ctdh = db.ChiTietDonHangs.ToList();
            List<DonHang> donhang = db.DonHangs.ToList();
            List<Tour> tour = db.Tours.ToList();
            var main = from d in donhang
                       join n in nguoidung on d.MaNguoiDung equals n.MaNguoiDung
                       where (d.MaDon == MaDon)
                       select new ViewModel
                       {
                           donhang = d,
                           nguoidung = n
                       };
            var sub = ctdh.Where(c => c.MaDon == MaDon).Join(tour, c => c.MaTour, t => t.MaTour, (c, t) => new ViewModel
            {
                ctdh = c,
                tour = t,
                thanhTien = c.SoLuongNL * (float)t.GiaNL * (1 - (float)t.GiamGia/100) + c.SoLuongTE * (float)t.GiaTE * (1 - (float)t.GiamGia / 100)
            });
            ViewBag.Main = main;
            ViewBag.Sub = sub;
            ViewBag.maDon = MaDon;

            return View();
        }
        public ActionResult XacNhan(int MaDon, string text)
        {
            var donHang = db.DonHangs.Where(d => d.MaDon == MaDon).FirstOrDefault();
            donHang.TinhTrang = text;
            db.Entry(donHang).State = EntityState.Modified;
            db.SaveChanges();
            TempData["success"] = "Cập nhật thành công!";

            return RedirectToAction("Index");

        }
    }
}