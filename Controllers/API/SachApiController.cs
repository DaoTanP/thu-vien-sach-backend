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

        // GET: api/book/topBorrow
        [Route("api/book/topBorrow")]
        public IHttpActionResult GetTopBorrow()
        {
            var data = db.ThongTinMuonSaches
                        .Include(t => t.Sach)
                        .Select(t => t.Sach)
                        .Select(t => new
                        {
                            Sach = t,
                            LuotMuon = db.ThongTinMuonSaches.Select(tt => tt.Sach_Id == t.Id).Count()
                        }).OrderBy(s => s.LuotMuon)
                        .Take(10)
                        .Select(s => s.Sach)
                        .ToList();

            data.All(s =>
            {
                s.AnhBia = Url.Content("~/") + "Content/images/Sach/" + s.AnhBia;
                return true;
            });

            return Ok(data);
        }

        // GET: api/book/randomRecommendation
        [Route("api/book/randomRecommendation")]
        public IHttpActionResult GetRandomRecommendation()
        {
            var data = db.Saches.OrderBy(x => Guid.NewGuid()).Take(10).ToList();

            data.All(s =>
            {
                s.AnhBia = Url.Content("~/") + "Content/images/Sach/" + s.AnhBia;
                return true;
            });

            return Ok(data);
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
        [ResponseType(typeof(NhaXuatBan))]
        public IHttpActionResult GetNhaXuatBan()
        {
            var result = db.NhaXuatBans.ToList();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("api/book/borrow")]
        [HttpPost]
        public IHttpActionResult Borrow(ThongTinMuonSach borrowInfo)
        {
            if (borrowInfo == null ||
                db.TheThuViens.Where(t => t.SoThe == borrowInfo.SoThe) == null ||
                db.Saches.Find(borrowInfo.Sach_Id) == null ||
                !IsAvailableToBorrow(borrowInfo.Sach_Id))
                return StatusCode(HttpStatusCode.BadRequest);

            borrowInfo.Id = Guid.NewGuid().ToString("n");
            borrowInfo.TrangThaiMuon_Id = db.TrangThaiMuonSaches.Where(t => t.TrangThaiMuon == "Đang chờ duyệt").First().Id;

            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            db.ThongTinMuonSaches.Add(borrowInfo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return Conflict();
                throw;
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IsAvailableToBorrow(string bookId)
        {
            var borrowing = db.ThongTinMuonSaches.Include(t => t.TrangThaiMuonSach)
                                .Where(t => t.TrangThaiMuonSach.TrangThaiMuon == "Đang mượn")
                                .Where(t => t.Sach_Id == bookId)
                                .Count();

            var totalInStock = db.Saches.Find(bookId).SoLuong;

            if (totalInStock - borrowing > 0)
                return true;

            return false;
        }

        private bool SachExists(string id)
        {
            return db.Saches.Count(e => e.Id == id) > 0;
        }
    }
}