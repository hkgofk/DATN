using ChieuTour.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChieuTour.Controllers
{
    public class HomeController : Controller
    {
        private ChieuTourDBContext db = new ChieuTourDBContext();
        public ActionResult Index()
        {
            var kq = (from k in db.Tours
                      let tongsl = (from c in db.ChiTietDonHangs
                                    join t in db.Tours
                                    on c.MaTour equals t.MaTour
                                    where c.MaTour == k.MaTour
                                    select c.SoLuongTE + c.SoLuongNL).Sum()
                      orderby tongsl descending
                      select new ViewModel
                      {
                          tour = k
                      }).Take(6);

            ViewBag.top6 = kq;
            var listTT = db.TinTucs.Select(t => t).OrderBy(t => t.NgayDang).Take(12);
            ViewBag.TT = listTT;
            return View();

        }

        public ActionResult search(string searchString, int? page, string filter)
        {
            var result = db.Tours.Where(t => t.TenTour.Contains(searchString));
            result = result.OrderBy(r => r.MaTour);
            int pageSize = 6;
            int pageNumber = page ?? 1;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = filter;
            }
            ViewBag.Filter = searchString;

            return View(result.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}