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
    public class NguoiDungApiController : ApiController
    {
        private ThuVien db = new ThuVien();

        // GET: api/userApi
        [Route("api/user")]
        public IHttpActionResult GetNguoiDungs()
        {
            var data = db.NguoiDungs
                .Include(u => u.DanhSachYeuThiches)
                .ToList();

            return Ok(data);
        }

        [Route("api/user/{id}")]
        // GET: api/user/5
        [ResponseType(typeof(NguoiDung))]
        public IHttpActionResult GetNguoiDung(string id)
        {
            NguoiDung nguoiDung = db.NguoiDungs
                .Include(u => u.DanhSachYeuThiches)
                .ToList()
                .Find(u => u.Id == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return Ok(nguoiDung);
        }

        // PUT: api/user/5ohwf4oh09f
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNguoiDung(string id, NguoiDung nguoiDung)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nguoiDung.Id)
            {
                return BadRequest();
            }

            db.Entry(nguoiDung).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NguoiDungExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/user
        [Route("api/user")]
        [ResponseType(typeof(NguoiDung))]
        public IHttpActionResult PostNguoiDung(AccountViewModel account)
        {
            if (NguoiDungExists(account.username))
            {
                return Conflict();
            }

            NguoiDung nguoiDung = new NguoiDung { 
                Id = Guid.NewGuid().ToString("n"),
                TenDangNhap = account.username,
                MatKhau = account.password,
                Ten = account.displayName
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NguoiDungs.Add(nguoiDung);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NguoiDungExists(nguoiDung.TenDangNhap))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            nguoiDung.MatKhau = null;
            return Ok(nguoiDung);
        }

        // DELETE: api/user/5ohwf4oh09f
        [Route("api/user/{id}")]
        [ResponseType(typeof(NguoiDung))]
        public IHttpActionResult DeleteNguoiDung(string id)
        {
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            db.NguoiDungs.Remove(nguoiDung);
            db.SaveChanges();

            return Ok(nguoiDung);
        }

        [Route("api/user/login")]
        [HttpPost]
        public IHttpActionResult Login([FromBody]AccountViewModel account)
        {
            var searchingUser = db.NguoiDungs
                                .Where(u => u.TenDangNhap == account.username)
                                .Where(u => u.MatKhau == account.password);

            if(searchingUser.Count() == 0)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.OK);
        }
        
        [Route("api/user/usernameExists")]
        [HttpPost]
        public bool usernameExists([FromBody]AccountViewModel account)
        {
            if (account != null && NguoiDungExists(account.username))
            {
                return true;
            }

            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NguoiDungExists(string tenDangNhap)
        {
            return db.NguoiDungs.Count(e => e.TenDangNhap == tenDangNhap) > 0;
        }
    }
}