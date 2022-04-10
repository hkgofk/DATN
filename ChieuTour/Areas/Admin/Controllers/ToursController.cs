using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChieuTour.Models;

namespace ChieuTour.Areas.Admin.Controllers
{
    public class ToursController : Controller
    {
        private ChieuTourDBContext db = new ChieuTourDBContext();

        // GET: Admin/Tours
        public ActionResult Index()
        {
            var tours = db.Tours.Include(t => t.DanhMuc);
            return View(tours.ToList());
        }

        // GET: Admin/Tours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Admin/Tours/Create
        public ActionResult Create()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            return View();
        }

        // POST: Admin/Tours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTour,TenTour,MoTa,LichTrinh,GiaNL,GiaTE,PhuongTien,Anh,MaDanhMuc,GiamGia,HoatDong")] Tour tour, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                string fileName = System.IO.Path.GetFileName(image.FileName);
                string urlImage = Server.MapPath("~/wwwroot/images/" + fileName);
                image.SaveAs(urlImage);

                tour.Anh = fileName;
            }

            if (ModelState.IsValid)
            {
                db.Tours.Add(tour);
                db.SaveChanges();
                ViewData["success"] = "Thêm tour thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", tour.MaDanhMuc);
            return View(tour);
        }

        // GET: Admin/Tours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", tour.MaDanhMuc);
            return View(tour);
        }

        // POST: Admin/Tours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTour,TenTour,MoTa,LichTrinh,GiaNL,GiaTE,PhuongTien,Anh,MaDanhMuc,GiamGia,HoatDong")] Tour tour, HttpPostedFileBase image)
        {            
            if (ModelState.IsValid)
            {
                
                if (tour != null)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(image.FileName);
                        string urlImage = Server.MapPath("~/wwwroot/images/" + fileName);
                        image.SaveAs(urlImage);

                        tour.Anh = fileName;
                    }
                }

                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", tour.MaDanhMuc);
            return View(tour);
        }

        // GET: Admin/Tours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Admin/Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = db.Tours.Find(id);
            db.Tours.Remove(tour);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
