using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThuVien.Controllers
{
    public class AvatarUploadViewModel
    {
        public string username { get; set; }
        public byte[] imageBytes { get; set; }
    }
}