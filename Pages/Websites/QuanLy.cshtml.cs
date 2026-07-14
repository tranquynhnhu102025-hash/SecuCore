using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace SecuCore.Pages
{
    // Cấu trúc dữ liệu cho 1 Website
    public class WebsiteItem
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int Health { get; set; }
        public string Status { get; set; }
    }

    // Cấu trúc dữ liệu cho 1 dòng Nhật ký
    public class LogItem
    {
        public string Time { get; set; }
        public string Content { get; set; }
        public string Target { get; set; }
        public string Type { get; set; }
    }

    public class QuanLyModel : PageModel
    {
        public List<WebsiteItem> DanhSachWebsite { get; set; }
        public List<LogItem> NhatKyQuet { get; set; }

        // Các biến thống kê trên cùng
        public int TongMucTieu { get; set; } = 24;
        public int SoAnToan { get; set; } = 18;
        public int SoRuiRo { get; set; } = 6;

        public void OnGet()
        {
            // 1. Khởi tạo dữ liệu ảo cho Bảng Danh sách Website
            DanhSachWebsite = new List<WebsiteItem>
            {
                new WebsiteItem { Url = "qlsv.ntt.edu.vn", Name = "Hệ thống QL Sinh viên", IpAddress = "103.14.232.12", Health = 98, Status = "An toàn" },
                new WebsiteItem { Url = "elearning.ntt.edu.vn", Name = "Cổng học trực tuyến", IpAddress = "103.14.232.45", Health = 65, Status = "Cảnh báo" },
                new WebsiteItem { Url = "crm.doanhnghiep.vn", Name = "Máy chủ CRM", IpAddress = "192.168.1.100", Health = 20, Status = "Bị tấn công" }
            };

            // 2. Khởi tạo dữ liệu ảo cho Cột Nhật ký Live
            NhatKyQuet = new List<LogItem>
            {
                new LogItem { Time = "13:10:30 (Vừa xong)", Content = "Đang phân tích gói tin HTTP Header tại mục tiêu", Target = "crm.doanhnghiep.vn", Type = "info" },
                new LogItem { Time = "13:08:15", Content = "Phát hiện cổng 3306 (MySQL) đang mở không mã hóa tại", Target = "elearning.ntt.edu.vn", Type = "warning" },
                new LogItem { Time = "12:55:02", Content = "Chặn đứng nỗ lực chèn mã độc XSS từ IP 171.244.x.x", Target = "", Type = "danger" },
                new LogItem { Time = "12:30:00", Content = "Hoàn tất kiểm tra bảo mật định kỳ cho hệ thống", Target = "qlsv.ntt.edu.vn", Type = "success" }
            };
        }
    }
}