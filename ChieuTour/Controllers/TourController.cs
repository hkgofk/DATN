using ChieuTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChieuTour.Controllers
{
    public class TourController : Controller
    {
        private ChieuTourDBContext db = new ChieuTourDBContext();
        // GET: Tour
        public ActionResult Detail(int id)
        {
            Tour tour = db.Tours.Find(id);

            return View(tour);
        }
    }
}