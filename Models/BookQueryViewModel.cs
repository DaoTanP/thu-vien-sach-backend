using System.Collections.Generic;

namespace QuanLyThuVien.Models
{
    public class BookQueryViewModel
    {
        public string bookTitle { get; set; }
        public List<string> category { get; set; }
        public string author { get; set; }
        public string publisher { get; set; }
        public int? publishedFrom { get; set; }
        public int? publishedTo { get; set; }
    }
}