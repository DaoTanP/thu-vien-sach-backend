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
    public class ThongTinMuonSachesController : Controller
    {
        private ThuVien db = new ThuVien();

        // GET: ThongTinMuonSaches
        public ActionResult Index()
        {
            string query = Request.QueryString["q"];
            if (query == null)
                query = "";
            var thongTinMuonSaches = db.ThongTinMuonSaches.Where(t => t.NguoiDung.HoDem.Contains(query) || t.NguoiDung.Ten.Contains(query) || t.Sach.TieuDe.Contains(query)).Include(t => t.NguoiDung).Include(t => t.Sach);
            return View(thongTinMuonSaches.ToList());
        }

        // GET: ThongTinMuonSaches/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinMuonSach thongTinMuonSach = db.ThongTinMuonSaches.Find(id);
            if (thongTinMuonSach == null)
            {
                return HttpNotFound();
            }
            return View(thongTinMuonSach);
        }

        // GET: ThongTinMuonSaches/Create
        public ActionResult Create()
        {
            ViewBag.NguoiMuon_Id = new SelectList(db.NguoiDungs, "Id", "TenDangNhap");
            ViewBag.Sach_Id = new SelectList(db.Saches, "Id", "TieuDe");
            return View();
        }

        // POST: ThongTinMuonSaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NguoiMuon_Id,Sach_Id,NgayMuon,NgayTra")] ThongTinMuonSach thongTinMuonSach)
        {
            if (ModelState.IsValid)
            {
                thongTinMuonSach.Id = Guid.NewGuid().ToString("n");
                db.ThongTinMuonSaches.Add(thongTinMuonSach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NguoiMuon_Id = new SelectList(db.NguoiDungs, "Id", "TenDangNhap", thongTinMuonSach.NguoiMuon_Id);
            ViewBag.Sach_Id = new SelectList(db.Saches, "Id", "TieuDe", thongTinMuonSach.Sach_Id);
            return View(thongTinMuonSach);
        }

        // GET: ThongTinMuonSaches/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinMuonSach thongTinMuonSach = db.ThongTinMuonSaches.Find(id);
            if (thongTinMuonSach == null)
            {
                return HttpNotFound();
            }
            ViewBag.NguoiMuon_Id = new SelectList(db.NguoiDungs, "Id", "TenDangNhap", thongTinMuonSach.NguoiMuon_Id);
            ViewBag.Sach_Id = new SelectList(db.Saches, "Id", "TieuDe", thongTinMuonSach.Sach_Id);
            return View(thongTinMuonSach);
        }

        // POST: ThongTinMuonSaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NguoiMuon_Id,Sach_Id,NgayMuon,NgayTra")] ThongTinMuonSach thongTinMuonSach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongTinMuonSach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NguoiMuon_Id = new SelectList(db.NguoiDungs, "Id", "TenDangNhap", thongTinMuonSach.NguoiMuon_Id);
            ViewBag.Sach_Id = new SelectList(db.Saches, "Id", "TieuDe", thongTinMuonSach.Sach_Id);
            return View(thongTinMuonSach);
        }

        // GET: ThongTinMuonSaches/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinMuonSach thongTinMuonSach = db.ThongTinMuonSaches.Find(id);
            if (thongTinMuonSach == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/Views/Shared/Delete.cshtml", new DeleteViewModel { Id = thongTinMuonSach.Id, Name = ""});
        }

        // POST: ThongTinMuonSaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ThongTinMuonSach thongTinMuonSach = db.ThongTinMuonSaches.Find(id);
            db.ThongTinMuonSaches.Remove(thongTinMuonSach);
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
