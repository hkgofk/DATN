using ChieuTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChieuTour.Controllers
{
    public class GioHangController : Controller
    {
        private ChieuTourDBContext db = new ChieuTourDBContext();
        // GET: GioHang
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int maTour, string strURL)
        {
            List<GioHang> lstGioHang = Laygiohang();
            GioHang sanpham = lstGioHang.Find(n => n.maTour == maTour);
            if (sanpham == null)
            {
                sanpham = new GioHang(maTour);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.soLuongNL++;
                sanpham.soLuongTE++;
                return Redirect(strURL);
            }
        }
        private double Tongtien()
        {
            double iTongtien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongtien = lstGioHang.Sum(n => n.thanhTien);
            }
            return iTongtien;
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = Laygiohang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Tongtien = Tongtien();
            return View(lstGioHang);
        }
        public ActionResult XoaGioHang(int iMaSP)
        {
            List<GioHang> lstGioHang = Laygiohang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.maTour == iMaSP);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.maTour == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGioHang(int iMaSP, FormCollection f)
        {
            List<GioHang> lstGioHang = Laygiohang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.maTour == iMaSP);
            if (sanpham != null)
            {
                sanpham.soLuongNL = int.Parse(f["txtSoLuongNL"].ToString());
                sanpham.soLuongTE = int.Parse(f["txtSoLuongTE"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatcaGioHang()
        {
            List<GioHang> lstGioHang = Laygiohang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "NguoiDung");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = Laygiohang();
            ViewBag.Tongtien = Tongtien();
            return View(lstGioHang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            NguoiDung Kh = (NguoiDung)Session["TaiKhoan"];
            DonHang ddh = new DonHang();
            List<GioHang> gh = Laygiohang();
            ddh.MaNguoiDung = Kh.MaNguoiDung;
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrang = "Đang xử lý";
            db.DonHangs.Add(ddh);
            db.SaveChanges();
            foreach (var item in gh)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDon = ddh.MaDon;
                ctdh.MaTour = item.maTour;
                ctdh.SoLuongNL = item.soLuongNL;
                ctdh.SoLuongTE = item.soLuongTE;

                db.ChiTietDonHangs.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            TempData["success"] = "Đặt hàng thành công chúng tôi sẽ liên hệ với bạn trong thời gian sớm nhất";
            return RedirectToAction("Index", "Home");

        }
    }
}