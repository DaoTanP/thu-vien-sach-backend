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
    public class SachesController : Controller
    {
        private ThuVien db = new ThuVien();

        // GET: Saches
        public ActionResult Index()
        {
            string query = Request.QueryString["q"];
            if (query == null)
                query = "";

            var saches = db.Saches.Where(s => s.TieuDe.Contains(query))
                                .Include(s => s.NhaXuatBan)
                                .Include(s => s.TacGia)
                                .Include(s => s.TheLoai)
                                .ToList();
            return View(saches);
        }

        // GET: Saches/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Include(s => s.NhaXuatBan).Include(s => s.TacGia).Include(s => s.TheLoai).Where(s => s.Id == id).First();
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // GET: Saches/Create
        public ActionResult Create()
        {
            ViewBag.NhaXuatBan_Id = new SelectList(db.NhaXuatBans, "Id", "TenNhaXuatBan");
            ViewBag.TacGia_Id = new SelectList(db.TacGias, "Id", "Ten");
            ViewBag.TheLoai_Id = new SelectList(db.TheLoais, "Id", "TenTheLoai");
            return View();
        }

        // POST: Saches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TieuDe,TheLoai_Id,TacGia_Id,NhaXuatBan_Id,NgayXuatBan,SoTrang,GioiThieu,AnhBia,SoLuong")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                sach.Id = Guid.NewGuid().ToString("n");
                HttpPostedFileBase file = Request.Files["AnhBia"];
                if(file != null && file.ContentLength > 0)
                {
                    file.SaveAs(Server.MapPath($"~/Content/images/Sach/{sach.Id}.{file.ContentType.Split('/')[1]}"));
                    sach.AnhBia = $"{ sach.Id}.{ file.ContentType.Split('/')[1]}";
                }
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NhaXuatBan_Id = new SelectList(db.NhaXuatBans, "Id", "TenNhaXuatBan", sach.NhaXuatBan_Id);
            ViewBag.TacGia_Id = new SelectList(db.TacGias, "Id", "Ten", sach.TacGia_Id);
            ViewBag.TheLoai_Id = new SelectList(db.TheLoais, "Id", "TenTheLoai", sach.TheLoai_Id);
            return View(sach);
        }

        // GET: Saches/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.NhaXuatBan_Id = new SelectList(db.NhaXuatBans, "Id", "TenNhaXuatBan", sach.NhaXuatBan_Id);
            ViewBag.TacGia_Id = new SelectList(db.TacGias, "Id", "Ten", sach.TacGia_Id);
            ViewBag.TheLoai_Id = new SelectList(db.TheLoais, "Id", "TenTheLoai", sach.TheLoai_Id);
            return View(sach);
        }

        // POST: Saches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TieuDe,TheLoai_Id,TacGia_Id,NhaXuatBan_Id,NgayXuatBan,SoTrang,GioiThieu,AnhBia,SoLuong")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["AnhBia"];
                if (file != null && file.ContentLength > 0)
                {
                    file.SaveAs(Server.MapPath($"~/Content/images/Sach/{sach.Id}.{file.ContentType.Split('/')[1]}"));
                    sach.AnhBia = $"{ sach.Id}.{ file.ContentType.Split('/')[1]}";
                }
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NhaXuatBan_Id = new SelectList(db.NhaXuatBans, "Id", "TenNhaXuatBan", sach.NhaXuatBan_Id);
            ViewBag.TacGia_Id = new SelectList(db.TacGias, "Id", "Ten", sach.TacGia_Id);
            ViewBag.TheLoai_Id = new SelectList(db.TheLoais, "Id", "TenTheLoai", sach.TheLoai_Id);
            return View(sach);
        }

        // GET: Saches/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/Views/Shared/Delete.cshtml", new DeleteViewModel { Id = sach.Id, Name = sach.TieuDe });
        }

        // POST: Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
            db.SaveChanges();
            string imagePath = Server.MapPath($"~/Content/images/Sach/{sach.AnhBia}");
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
