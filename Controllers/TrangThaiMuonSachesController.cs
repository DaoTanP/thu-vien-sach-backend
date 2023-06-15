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
    public class TrangThaiMuonSachesController : Controller
    {
        private ThuVien db = new ThuVien();

        // GET: TrangThaiMuonSaches
        public ActionResult Index()
        {
            return View(db.TrangThaiMuonSaches.ToList());
        }

        // GET: TrangThaiMuonSaches/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangThaiMuonSach trangThaiMuonSach = db.TrangThaiMuonSaches.Find(id);
            if (trangThaiMuonSach == null)
            {
                return HttpNotFound();
            }
            return View(trangThaiMuonSach);
        }

        // GET: TrangThaiMuonSaches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrangThaiMuonSaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrangThaiMuon")] TrangThaiMuonSach trangThaiMuonSach)
        {
            if (ModelState.IsValid)
            {
                db.TrangThaiMuonSaches.Add(trangThaiMuonSach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trangThaiMuonSach);
        }

        // GET: TrangThaiMuonSaches/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangThaiMuonSach trangThaiMuonSach = db.TrangThaiMuonSaches.Find(id);
            if (trangThaiMuonSach == null)
            {
                return HttpNotFound();
            }
            return View(trangThaiMuonSach);
        }

        // POST: TrangThaiMuonSaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrangThaiMuon")] TrangThaiMuonSach trangThaiMuonSach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trangThaiMuonSach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trangThaiMuonSach);
        }

        // GET: TrangThaiMuonSaches/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangThaiMuonSach trangThaiMuonSach = db.TrangThaiMuonSaches.Find(id);
            if (trangThaiMuonSach == null)
            {
                return HttpNotFound();
            }
            return View(trangThaiMuonSach);
        }

        // POST: TrangThaiMuonSaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TrangThaiMuonSach trangThaiMuonSach = db.TrangThaiMuonSaches.Find(id);
            db.TrangThaiMuonSaches.Remove(trangThaiMuonSach);
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
