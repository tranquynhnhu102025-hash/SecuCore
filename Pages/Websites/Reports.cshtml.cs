using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace SecuCore.Pages.Websites
{
    public class ReportsModel : PageModel
    {
        // Khai báo các biến để truyền ra giao diện HTML
        public string ThoiGianXuat { get; set; } = "";
        public int TongSoMucTieu { get; set; }
        public int TongSoSuKien { get; set; }
        public int SoCritical { get; set; }
        public int SoHigh { get; set; }
        public int SoMedium { get; set; }
        public int SoLow { get; set; }

        public void OnGet()
        {
            // Gán cứng số liệu tĩnh để trang web có dữ liệu hiển thị và xuất file
            ThoiGianXuat = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            TongSoMucTieu = 5;
            TongSoSuKien = 1245;

            SoCritical = 3;
            SoHigh = 15;
            SoMedium = 42;
            SoLow = 1185;
        }
    }
}