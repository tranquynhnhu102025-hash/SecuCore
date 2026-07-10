using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecuCore.Pages
{
    // Tạo cấu trúc dữ liệu cho 1 dòng nhật ký
    public class ScanLog
    {
        public string TargetUrl { get; set; }
        public string VulnerabilityType { get; set; }
        public bool IsFixed { get; set; }
        public DateTime ScanTime { get; set; }
    }

    public class IndexModel : PageModel
    {
        // Các biến số hiển thị trên 3 cái thẻ Card
        public int TongSoMucTieu { get; set; } = 3;
        public int SoLoHongNguyHiem { get; set; }
        public int SoCanhBao { get; set; } = 12;

        // Danh sách tĩnh để lưu nhật ký ảo (không cần Database)
        public static List<ScanLog> DanhSachNhatKy { get; set; } = new List<ScanLog>();

        public void OnGet()
        {
            // Mỗi lần load trang, đếm xem có bao nhiêu lỗi chưa vá
            SoLoHongNguyHiem = DanhSachNhatKy.Count(x => !x.IsFixed);
        }

        // Hàm này sẽ chạy khi bạn bấm nút "KHỞI ĐỘNG QUÉT TOÀN DIỆN"
        public IActionResult OnPostStartScan()
        {
            Random rnd = new Random();
            string[] mockErrors = { "DDoS Attempt", "SQL Injection", "Path Traversal", "Cross-Site Scripting (XSS)" };
            string[] mockTargets = { "qlsv.ntt.edu.vn", "lms.ntt.edu.vn", "elearning.ntt.edu.vn" };

            // Tạo ra 1 lỗi ảo mới
            var newLog = new ScanLog
            {
                TargetUrl = mockTargets[rnd.Next(mockTargets.Length)],
                VulnerabilityType = mockErrors[rnd.Next(mockErrors.Length)],
                IsFixed = false,
                ScanTime = DateTime.Now
            };

            // Nhét lỗi mới lên đầu bảng
            DanhSachNhatKy.Insert(0, newLog);

            // Giới hạn bảng hiển thị tối đa 8 dòng cho khỏi bị tràn giao diện
            if (DanhSachNhatKy.Count > 8)
            {
                DanhSachNhatKy.RemoveAt(8);
            }

            // Load lại trang để cập nhật giao diện
            return RedirectToPage();
        }
    }
}