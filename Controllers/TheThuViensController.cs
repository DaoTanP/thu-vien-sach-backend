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
    public class TheThuViensController : Controller
    {
        private ThuVien db = new ThuVien();

        // GET: TheThuViens
        public ActionResult Index()
        {
            return View(db.TheThuViens.ToList());
        }

        // GET: TheThuViens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TheThuViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoThe,MatKhauThe,NgayHetHan")] TheThuVien theThuVien)
        {
            if (ModelState.IsValid)
            {
                if(theThuVien.SoThe == "")
                    theThuVien.SoThe = Guid.NewGuid().ToString("n");

                theThuVien.NgayCap = DateTime.Now;

                db.TheThuViens.Add(theThuVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(theThuVien);
        }

        // GET: TheThuViens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheThuVien theThuVien = db.TheThuViens.Find(id);
            if (theThuVien == null)
            {
                return HttpNotFound();
            }
            return View(theThuVien);
        }

        // POST: TheThuViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoThe,MatKhauThe,NgayHetHan")] TheThuVien theThuVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theThuVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theThuVien);
        }

        // GET: TheThuViens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheThuVien theThuVien = db.TheThuViens.Find(id);
            if (theThuVien == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/Views/Shared/Delete.cshtml", new DeleteViewModel { Id = theThuVien.SoThe, Name = theThuVien.SoThe });
        }

        // POST: TheThuViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TheThuVien theThuVien = db.TheThuViens.Find(id);
            db.TheThuViens.Remove(theThuVien);
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
