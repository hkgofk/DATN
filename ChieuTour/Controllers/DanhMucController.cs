using ChieuTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChieuTour.Controllers
{
    public class DanhMucController : Controller
    {
        private ChieuTourDBContext db = new ChieuTourDBContext();
        // GET: DanhMuc
        public ActionResult Index(int id)
        {
            var listTours = db.Tours.Where(t => t.MaDanhMuc == id);
            ViewBag.Tours = listTours;

            return View();
        }
        
        public ActionResult ListDanhMuc()
        {
            var danhMucs = db.DanhMucs.ToList();
            ViewBag.danhMucTour = danhMucs;

            return PartialView("_DanhMuc");
        }

    }
}