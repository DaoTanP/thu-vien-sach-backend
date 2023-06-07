using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class SachApiController : ApiController
    {
        private ThuVien db = new ThuVien();

        // GET: api/book
        [Route("api/book")]
        public IHttpActionResult GetSaches()
        {
            var data = db.Saches
                .Include(s => s.TheLoai)
                .Include(s => s.TacGia)
                .Include(s => s.NhaXuatBan)
                .ToList();

            data.All(s =>
            {
                s.AnhBia = Url.Content("~/") + "Content/images/Sach/" + s.AnhBia;
                return true;
            });

            return Ok(data);
        }

        // GET: api/book/5ohwf4oh09f
        [Route("api/book/{id}")]
        [ResponseType(typeof(Sach))]
        public IHttpActionResult GetSach(string id)
        {
            Sach sach = db.Saches
                .Include(s => s.TheLoai)
                .Include(s => s.TacGia)
                .Include(s => s.NhaXuatBan)
                .ToList()
                .Find(s => s.Id == id);

            if (sach == null)
            {
                return NotFound();
            }

            sach.AnhBia = Url.Content("~/") + "Content/images/Sach/" + sach.AnhBia;

            return Ok(sach);
        }

        // GET: api/book/search?bookTitle=Hello&....
        [Route("api/book/search")]
        [HttpGet]
        [ResponseType(typeof(Sach))]
        public IHttpActionResult SearchSach([FromUri]BookQueryViewModel query)
        {
            List<Sach> sach;

            var sa = db.Saches
                .Include(s => s.TheLoai)
                .Include(s => s.TacGia)
                .Include(s => s.NhaXuatBan)
                .ToList();

            if (query == null)
                sach = sa;
            else
                sach = sa
                    .Where(s => s.TieuDe.ToLower().IndexOf((query.bookTitle ?? string.Empty).ToLower()) > -1)
                    .Where(s => query.category != null ? query.category.Any(c => c != null && s.TheLoai.TenTheLoai.ToLower().IndexOf(c.ToLower()) > -1) : true)
                    .Where(s => s.TacGia.Ten.ToLower().IndexOf((query.author ?? string.Empty).ToLower()) > -1)
                    .Where(s => s.NhaXuatBan.TenNhaXuatBan.ToLower().IndexOf((query.publisher ?? string.Empty).ToLower()) > -1)
                    .Where(s => (s.NgayXuatBan != null && query.publishedFrom != null) ?
                            (((DateTime)s.NgayXuatBan).Year >= query.publishedFrom)
                            : true)
                    .Where(s => (s.NgayXuatBan != null && query.publishedTo != null) ?
                            (((DateTime)s.NgayXuatBan).Year <= query.publishedFrom)
                            : true)
                    .ToList();

            /*var sach = db.Saches
                .Include(s => s.TheLoai)
                .Include(s => s.TacGia)
                .Include(s => s.NhaXuatBan)
                .Where(s =>
                    s.TieuDe.Contains(query.bookTitle ?? "") &&
                    s.TheLoai.TenTheLoai.Contains(query.category ?? "") &&
                    s.TacGia.Ten.Contains(query.author ?? "") &&
                    s.NhaXuatBan.TenNhaXuatBan.Contains(query.publisher ?? "") &&
                    (s.NgayXuatBan != null && query.publishedFrom != null && query.publishedTo != null ?
                        (DateTime.Compare((DateTime)s.NgayXuatBan, (DateTime)query.publishedFrom) >= 0 &&
                        DateTime.Compare((DateTime)s.NgayXuatBan, (DateTime)query.publishedFrom) <= 0)
                        : true))
                .ToList();*/

            if (sach == null)
            {
                return NotFound();
            }

            sach.All(s =>
            {
                s.AnhBia = Url.Content("~/") + "Content/images/Sach/" + s.AnhBia;
                return true;
            });

            return Ok(sach);
        }

        // GET: api/book/category
        [Route("api/book/category")]
        [HttpGet]
        [ResponseType(typeof(TheLoai))]
        public IHttpActionResult GetTheLoai()
        {
            var result = db.TheLoais.ToList();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        
        // GET: api/book/author
        [Route("api/book/author")]
        [HttpGet]
        [ResponseType(typeof(TacGia))]
        public IHttpActionResult GetTacGia()
        {
            var result = db.TacGias.ToList();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        
        // GET: api/book/publisher
        [Route("api/book/publisher")]
        [HttpGet]
        [ResponseType(typeof(TacGia))]
        public IHttpActionResult GetNhaXuatBan()
        {
            var result = db.NhaXuatBans.ToList();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SachExists(string id)
        {
            return db.Saches.Count(e => e.Id == id) > 0;
        }
    }
}