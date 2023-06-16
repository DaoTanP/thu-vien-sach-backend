using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class HomeController : Controller
    {
        private ThuVien db = new ThuVien();
        public ActionResult Index()
        {
            var theLoaiId = db.TheLoais.OrderBy(t => t.TenTheLoai).Select(t => t.Id).ToList();
            List<object> sachTheoTheLoai = new List<object>();
            foreach (var id in theLoaiId)
            {
                sachTheoTheLoai.Add(new { name = db.TheLoais.Find(id).TenTheLoai, value = db.Saches.Where(s => s.TheLoai_Id == id).Count() });
            }

            var thoiGianMuonSach = db.ThongTinMuonSaches.OrderBy(t => t.NgayMuon).Select(t => t.NgayMuon).Distinct().ToList();

            List<int> sachMuonTheoThoiGian = new List<int>();
            foreach (var tg in thoiGianMuonSach)
            {
                sachMuonTheoThoiGian.Add(db.ThongTinMuonSaches.Where(t => DateTime.Compare(t.NgayMuon, tg) == 0).Count());
            }

            thoiGianMuonSach = thoiGianMuonSach.Select(t => t.Date).ToList();

            List<string> sachMuonNhieu = db.ThongTinMuonSaches
                                  .Include(t => t.Sach)
                                  .Select(t => t.Sach)
                                  .Select(t => new
                                  {
                                      Sach = t.TieuDe,
                                      LuotMuon = db.ThongTinMuonSaches.Select(tt => tt.Sach_Id == t.Id).Count()
                                  }).OrderBy(s => s.LuotMuon)
                                  .Take(10)
                                  .Select(s => s.Sach)
                                  .ToList();

            ViewBag.SoLuongSach = db.Saches.Count();
            ViewBag.SoLuongSachDangMuon = db.ThongTinMuonSaches.Include(t => t.TrangThaiMuonSach).Where(t => t.TrangThaiMuonSach.TrangThaiMuon == "Đang mượn").Count();
            ViewBag.SachTheoTheLoai = JsonConvert.SerializeObject(sachTheoTheLoai);
            ViewBag.ThoiGianMuonSach = JsonConvert.SerializeObject(thoiGianMuonSach);
            ViewBag.SachMuonTheoThoiGian = JsonConvert.SerializeObject(sachMuonTheoThoiGian);
            ViewBag.SachMuonNhieu = sachMuonNhieu;

            return View();
        }

    }
}