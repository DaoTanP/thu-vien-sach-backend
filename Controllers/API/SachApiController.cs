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

            return Ok(sach);
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