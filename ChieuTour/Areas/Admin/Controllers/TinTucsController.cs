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
    public class TinTucsController : Controller
    {
        private ChieuTourDBContext db = new ChieuTourDBContext();

        // GET: Admin/TinTucs
        public ActionResult Index()
        {
            var tinTucs = db.TinTucs.Include(t => t.NguoiDung);
            return View(tinTucs.ToList());
        }

        // GET: Admin/TinTucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Create
        public ActionResult Create()
        {
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TaiKhoan");
            return View();
        }

        // POST: Admin/TinTucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTinTuc,TieuDe,NoiDung,HinhAnh,NgayDang,MaNguoiDung")] TinTuc tinTuc, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                string fileName = System.IO.Path.GetFileName(image.FileName);
                string urlImage = Server.MapPath("~/wwwroot/images/" + fileName);
                image.SaveAs(urlImage);

                tinTuc.HinhAnh = fileName;
            }

            if (ModelState.IsValid)
            {
                tinTuc.MaNguoiDung = 2;
                tinTuc.NgayDang = DateTime.Now;
                db.TinTucs.Add(tinTuc);
                db.SaveChanges();
                TempData["success"] = "Thêm tin tức thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TaiKhoan", tinTuc.MaNguoiDung);
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TaiKhoan", tinTuc.MaNguoiDung);
            return View(tinTuc);
        }

        // POST: Admin/TinTucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTinTuc,TieuDe,NoiDung,HinhAnh,NgayDang,MaNguoiDung")] TinTuc tinTuc, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (tinTuc != null)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(image.FileName);
                        string urlImage = Server.MapPath("~/wwwroot/images/" + fileName);
                        image.SaveAs(urlImage);

                        tinTuc.HinhAnh = fileName;
                    }
                }
                tinTuc.MaNguoiDung = 2;
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Sửa tin tức thành công!";
                return RedirectToAction("Index");
            }
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TaiKhoan", tinTuc.MaNguoiDung);
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: Admin/TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            db.TinTucs.Remove(tinTuc);
            db.SaveChanges();
            TempData["success"] = "Đã xóa tin tức!";
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
