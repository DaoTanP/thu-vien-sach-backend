using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThuVien.Models
{
    public class UserPasswordChangeViewModel
    {
        public string username { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}