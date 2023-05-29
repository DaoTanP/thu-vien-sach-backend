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
    public class ThuVienApiController : ApiController
    {
        private ThuVien db = new ThuVien();

        // GET: api/SachApi
        [Route("api/sach")]
        public IHttpActionResult GetSaches()
        {
            var data = db.Saches.ToList();

            return Ok(data);
        }

        // GET: api/SachApi/5
        [Route("api/sach/{id}")]
        [ResponseType(typeof(Sach))]
        public IHttpActionResult GetSach(string id)
        {
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return NotFound();
            }

            return Ok(sach);
        }

        // PUT: api/SachApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSach(string id, Sach sach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sach.Id)
            {
                return BadRequest();
            }

            db.Entry(sach).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SachExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SachApi
        [ResponseType(typeof(Sach))]
        public IHttpActionResult PostSach(Sach sach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Saches.Add(sach);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SachExists(sach.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sach.Id }, sach);
        }

        // DELETE: api/SachApi/5
        [ResponseType(typeof(Sach))]
        public IHttpActionResult DeleteSach(string id)
        {
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return NotFound();
            }

            db.Saches.Remove(sach);
            db.SaveChanges();

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