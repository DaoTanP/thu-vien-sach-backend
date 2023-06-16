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
    public class DanhSachYeuThichesController : Controller
    {
        private ThuVien db = new ThuVien();

        // GET: DanhSachYeuThiches
        public ActionResult Index()
        {
            var danhSachYeuThiches = db.DanhSachYeuThiches
                .Include(d => d.NguoiDung)
                .Include(d => d.Sach)
                .ToList();

            return View(danhSachYeuThiches);
        }

        // GET: DanhSachYeuThiches/Create
        public ActionResult Create()
        {
            ViewBag.NguoiDung_Id = new SelectList(db.NguoiDungs, "Id", "TenDangNhap");
            ViewBag.Sach_Id = new SelectList(db.Saches, "Id", "TieuDe");
            return View();
        }

        // POST: DanhSachYeuThiches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NguoiDung_Id,Sach_Id")] DanhSachYeuThich danhSachYeuThich)
        {
            if (ModelState.IsValid)
            {
                danhSachYeuThich.Id = Guid.NewGuid().ToString("n");
                db.DanhSachYeuThiches.Add(danhSachYeuThich);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NguoiDung_Id = new SelectList(db.NguoiDungs, "Id", "TenDangNhap", danhSachYeuThich.NguoiDung_Id);
            ViewBag.Sach_Id = new SelectList(db.Saches, "Id", "TieuDe", danhSachYeuThich.Sach_Id);
            return View(danhSachYeuThich);
        }

        // GET: DanhSachYeuThiches/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhSachYeuThich danhSachYeuThich = db.DanhSachYeuThiches.Find(id);
            if (danhSachYeuThich == null)
            {
                return HttpNotFound();
            }
            ViewBag.NguoiDung_Id = new SelectList(db.NguoiDungs, "Id", "TenDangNhap", danhSachYeuThich.NguoiDung_Id);
            ViewBag.Sach_Id = new SelectList(db.Saches, "Id", "TieuDe", danhSachYeuThich.Sach_Id);
            return View(danhSachYeuThich);
        }

        // POST: DanhSachYeuThiches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NguoiDung_Id,Sach_Id")] DanhSachYeuThich danhSachYeuThich)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhSachYeuThich).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NguoiDung_Id = new SelectList(db.NguoiDungs, "Id", "TenDangNhap", danhSachYeuThich.NguoiDung_Id);
            ViewBag.Sach_Id = new SelectList(db.Saches, "Id", "TieuDe", danhSachYeuThich.Sach_Id);
            return View(danhSachYeuThich);
        }

        // GET: DanhSachYeuThiches/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhSachYeuThich danhSachYeuThich = db.DanhSachYeuThiches.Find(id);
            if (danhSachYeuThich == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/Views/Shared/Delete.cshtml", new DeleteViewModel { Id = danhSachYeuThich.Id, Name = "" });
        }

        // POST: DanhSachYeuThiches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DanhSachYeuThich danhSachYeuThich = db.DanhSachYeuThiches.Find(id);
            db.DanhSachYeuThiches.Remove(danhSachYeuThich);
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
