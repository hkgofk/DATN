using ChieuTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ChieuTour.Controllers
{
    public class DanhMucController : Controller
    {
        private ChieuTourDBContext db = new ChieuTourDBContext();
        // GET: DanhMuc
        public ActionResult Index(int id, int? page)
        {
            var listTours = db.Tours.Where(t => t.MaDanhMuc == id);
            listTours = listTours.OrderBy(t => t.MaTour);
            int pageSize = 6;
            int pageNumber = page ?? 1;

            return View(listTours.ToPagedList(pageNumber, pageSize));
        }
        
        public ActionResult ListDanhMuc()
        {
            var danhMucs = db.DanhMucs.ToList();
            ViewBag.danhMucTour = danhMucs;

            return PartialView("_DanhMuc");
        }

    }
}