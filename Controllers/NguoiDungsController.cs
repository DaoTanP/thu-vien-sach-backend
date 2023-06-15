using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class NguoiDungsController : Controller
    {
        private ThuVien db = new ThuVien();

        // GET: NguoiDungs
        public ActionResult Index()
        {
            var nguoiDungs = db.NguoiDungs.Include(u => u.TheThuVien).ToList();

            return View(nguoiDungs);
        }

        // GET: NguoiDungs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Include(u => u.TheThuVien).Where(u => u.Id == id).First();
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // GET: NguoiDungs/Create
        public ActionResult Create()
        {
            ViewBag.SoThe = new SelectList(db.TheThuViens, "SoThe", "SoThe");
            return View();
        }

        // POST: NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenDangNhap,MatKhau,HoDem,Ten,NgaySinh,GioiTinh,DiaChi,Email,SoDienThoai,AnhDaiDien,SoThe")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                nguoiDung.Id = Guid.NewGuid().ToString("n");
                HttpPostedFileBase file = Request.Files["AnhDaiDien"];
                if (file != null && file.ContentLength > 0)
                {
                    file.SaveAs(Server.MapPath($"~/Content/images/NguoiDung/{nguoiDung.Id}.{file.ContentType.Split('/')[1]}"));
                    nguoiDung.AnhDaiDien = $"{ nguoiDung.Id}.{ file.ContentType.Split('/')[1]}";
                }
                db.NguoiDungs.Add(nguoiDung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SoThe = new SelectList(db.TheThuViens, "SoThe", "SoThe", nguoiDung.SoThe);
            return View(nguoiDung);
        }

        // GET: NguoiDungs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }

            ViewBag.SoThe = new SelectList(db.TheThuViens, "SoThe", "SoThe", nguoiDung.SoThe);
            return View(nguoiDung);
        }

        // POST: NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenDangNhap,MatKhau,HoDem,Ten,NgaySinh,GioiTinh,DiaChi,Email,SoDienThoai,AnhDaiDien,SoThe")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["AnhDaiDien"];
                if (file != null && file.ContentLength > 0)
                {
                    file.SaveAs(Server.MapPath($"~/Content/images/NguoiDung/{nguoiDung.Id}.{file.ContentType.Split('/')[1]}"));
                    nguoiDung.AnhDaiDien = $"{ nguoiDung.Id}.{ file.ContentType.Split('/')[1]}";
                }
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SoThe = new SelectList(db.TheThuViens, "SoThe", "SoThe", nguoiDung.SoThe);
            return View(nguoiDung);
        }

        // GET: NguoiDungs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/Views/Shared/Delete.cshtml", new DeleteViewModel { Id = nguoiDung.Id, Name = nguoiDung.TenDangNhap });
        }

        // POST: NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            db.NguoiDungs.Remove(nguoiDung);
            db.SaveChanges();
            string imagePath = Server.MapPath($"~/Content/images/NguoiDung/{nguoiDung.AnhDaiDien}");
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
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
