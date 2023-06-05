using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
                .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach))
                .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach.TheLoai))
                .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach.TacGia))
                .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach.NhaXuatBan))
                .ToList();

            data = data.Select(d => {
                d.DanhSachYeuThiches.All(ds =>
                {
                    ds.Sach.AnhBia = Url.Content("~/") + "Content/images/Sach/" + ds.Sach.AnhBia;
                    return true;
                });
                return d;
            }).ToList();

            return Ok(data);
        }

        [Route("api/user/{id}")]
        // GET: api/user/5
        [ResponseType(typeof(NguoiDung))]
        public IHttpActionResult GetNguoiDung(string id)
        {
            NguoiDung nguoiDung = db.NguoiDungs
                .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach))
                .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach.TheLoai))
                .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach.TacGia))
                .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach.NhaXuatBan))
                .ToList()
                .Find(u => u.Id == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            nguoiDung.AnhDaiDien = Url.Content("~/") + "Content/images/NguoiDung/" + nguoiDung.AnhDaiDien;
            nguoiDung.DanhSachYeuThiches.All(d =>
            {
                d.Sach.AnhBia = Url.Content("~/") + "Content/images/Sach/" + d.Sach.AnhBia;
                return true;
            });

            return Ok(nguoiDung);
        }

        // PUT: api/user
        [Route("api/user")]
        [HttpPut]
        public IHttpActionResult PutNguoiDung(NguoiDung n)
        {
            if (!NguoiDungExists(n.TenDangNhap))
            {
                return Conflict();
            }

            var v = Validate(n.TenDangNhap, n.MatKhau);
            if (v == null)
            {
                return BadRequest(ModelState);
            }

            // Khong thay doi anh dai dien, co phuong thuc rieng de upload anh
            n.AnhDaiDien = v.First().AnhDaiDien;

            db.Entry(n).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            n.MatKhau = null;
            n.AnhDaiDien = Url.Content("~/") + "Content/images/NguoiDung/" + n.AnhDaiDien;
            n.DanhSachYeuThiches.All(d =>
            {
                d.Sach.AnhBia = Url.Content("~/") + "Content/images/Sach/" + d.Sach.AnhBia;
                return true;
            });

            return Ok(n);
        }

        // POST: api/user/uploadAvatar
        [Route("api/user/uploadAvatar")]
        [HttpPost]
        public IHttpActionResult UploadAvatar(AvatarUploadViewModel a)
        {
            if (!NguoiDungExists(a.username))
            {
                return Conflict();
            }

            NguoiDung n = db.NguoiDungs
                                .Where(u => u.TenDangNhap == a.username).First();
            var fileName = "";
            try
            {
                fileName = WriteImage(a.imageBytes, n.Id);
            }
            catch (Exception)
            {
                return Conflict();
                throw;
            }

            n.AnhDaiDien = fileName != "" ? fileName : n.AnhDaiDien;

            db.Entry(n).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
                throw;
            }

            return Ok(Url.Content("~/") + $"Content/images/NguoiDung/{n.AnhDaiDien}");
        }
        
        // POST: api/user/changePassword
        [Route("api/user/changePassword")]
        [HttpPost]
        public IHttpActionResult ChangePassword(UserPasswordChangeViewModel a)
        {
            if (!NguoiDungExists(a.username))
            {
                return Conflict();
            }

            var v = Validate(a.username, a.oldPassword);
            if (v == null)
            {
                return BadRequest(ModelState);
            }

            NguoiDung n = v.First();

            n.MatKhau = a.newPassword;

            db.Entry(n).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
                throw;
            }

            return Ok();
        }

        // POST: api/user
        [Route("api/user")]
        [ResponseType(typeof(NguoiDung))]
        public IHttpActionResult PostNguoiDung(NguoiDung n)
        {
            if (NguoiDungExists(n.TenDangNhap))
            {
                return Conflict();
            }

            NguoiDung nguoiDung = new NguoiDung { 
                Id = Guid.NewGuid().ToString("n"),
                TenDangNhap = n.TenDangNhap,
                MatKhau = n.MatKhau,
                Ten = n.Ten
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

        // DELETE: api/user/delete
        [Route("api/user/delete")]
        [HttpPost]
        [ResponseType(typeof(NguoiDung))]
        public IHttpActionResult DeleteNguoiDung(NguoiDung n)
        {
            var v = Validate(n.TenDangNhap, n.MatKhau);
            if (v == null)
            {
                return NotFound();
            }

            NguoiDung nguoiDung = v.First();

            db.NguoiDungs.Remove(nguoiDung);
            db.SaveChanges();

            return Ok();
        }

        [Route("api/user/login")]
        [HttpPost]
        public IHttpActionResult Login(NguoiDung n)
        {
            if (n == null)
                return BadRequest();

            var ketQua = Validate(n.TenDangNhap, n.MatKhau);

            if(ketQua == null)
            {
                return NotFound();
            }

            ketQua = ketQua
                    .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach))
                    .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach.TheLoai))
                    .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach.TacGia))
                    .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach.NhaXuatBan));

            NguoiDung nguoiDung = ketQua.First();
            nguoiDung.MatKhau = null;
            nguoiDung.AnhDaiDien = Url.Content("~/") + "Content/images/NguoiDung/" + nguoiDung.AnhDaiDien;
            nguoiDung.DanhSachYeuThiches.All(d =>
            {
                d.Sach.AnhBia = Url.Content("~/") + "Content/images/Sach/" + d.Sach.AnhBia;
                return true;
            });

            return Ok(nguoiDung);
        }
        
        [Route("api/user/usernameExists")]
        [HttpPost]
        public bool UsernameExists([FromBody]NguoiDung n)
        {
            if (n != null && NguoiDungExists(n.TenDangNhap))
            {
                return true;
            }

            return false;
        }
        
        [Route("api/user/favoriteBooks")]
        [HttpPost]
        public IHttpActionResult GetFavoriteBooks([FromBody]NguoiDung n)
        {
            if (n != null || !NguoiDungExists(n.TenDangNhap))
                return StatusCode(HttpStatusCode.BadRequest);

            var nguoiDung = Validate(n.TenDangNhap, n.MatKhau);

            if (nguoiDung.Count() == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            nguoiDung = nguoiDung.Include(u => u.DanhSachYeuThiches.Select(d => d.Sach))
                    .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach.TheLoai))
                    .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach.TacGia))
                    .Include(u => u.DanhSachYeuThiches.Select(d => d.Sach.NhaXuatBan));

            var favoriteBooks = nguoiDung.First().DanhSachYeuThiches;
            favoriteBooks.All(d =>
            {
                d.Sach.AnhBia = Url.Content("~/") + "Content/images/Sach/" + d.Sach.AnhBia;
                return true;
            });

            return Ok(favoriteBooks);
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
        
        private IQueryable<NguoiDung> Validate(string tenDangNhap, string matKhau)
        {
            var nguoiDung = db.NguoiDungs.Where(u => u.TenDangNhap == tenDangNhap).Where(u => u.MatKhau == matKhau);

            if (nguoiDung.Count() == 0)
                return null;

            return nguoiDung;
        }

        private string WriteImage(byte[] imageBytes, string imageName)
        {
            var filename = $"{imageName}";

            using (var im = Image.FromStream(new MemoryStream(imageBytes)))
            {
                ImageFormat format;
                if (ImageFormat.Png.Equals(im.RawFormat))
                {
                    filename += ".png";
                    format = ImageFormat.Png;
                }
                else
                {
                    filename += ".jpg";
                    format = ImageFormat.Jpeg;
                }
                string path = HttpContext.Current.Server.MapPath("~/Content/images/NguoiDung/") + filename;
                im.Save(path, format);
            }

            return filename;
        }
    }
}