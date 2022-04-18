using ChieuTour.Models;
using PagedList;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChieuTour.Controllers
{
    public class TinTucController : Controller
    {
        private ChieuTourDBContext db = new ChieuTourDBContext();
        // GET: TinTuc
        public ActionResult Index(int? page)
        {
            var tinTucs = db.TinTucs.Select(t => t);
            tinTucs = tinTucs.OrderBy(t => t.MaTinTuc);

            int pageSize = 6;
            int pageNumber = page ?? 1;

            return View(tinTucs.ToPagedList(pageNumber, pageSize));           
        }
        public ActionResult Detail(int id)
        {
            var tinTuc = db.TinTucs.Find(id);

            return View(tinTuc);
        }
    }
}