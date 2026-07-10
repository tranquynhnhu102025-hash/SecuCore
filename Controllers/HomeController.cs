using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace SecuCore.Controllers
{
    // Tạo 1 Model ảo ngay trong file này cho nhanh
    public class ScanLog
    {
        public int Id { get; set; }
        public string TargetUrl { get; set; }
        public string VulnerabilityType { get; set; }
        public string Severity { get; set; }
        public string ScanTime { get; set; }
    }

    public class HomeController : Controller
    {
        // Tạo 1 list tĩnh để lưu dữ liệu tạm thời (Không cần Database)
        private static List<ScanLog> scanLogs = new List<ScanLog>
        {
            new ScanLog { Id = 1, TargetUrl = "qlsv.ntt.edu.vn", VulnerabilityType = "Cross-Site Scripting (XSS)", Severity = "Trung bình", ScanTime = DateTime.Now.AddMinutes(-30).ToString("HH:mm:ss dd/MM/yyyy") },
            new ScanLog { Id = 2, TargetUrl = "lms.ntt.edu.vn", VulnerabilityType = "SQL Injection", Severity = "Nghiêm trọng", ScanTime = DateTime.Now.AddMinutes(-120).ToString("HH:mm:ss dd/MM/yyyy") }
        };

        public IActionResult Index()
        {
            // Truyền danh sách lỗi ra ngoài giao diện
            return View(scanLogs);
        }

        [HttpPost]
        public IActionResult StartScan()
        {
            // Logic khi bấm nút: Tạo ra 1 lỗi mới và nhét lên đầu danh sách
            Random rnd = new Random();
            string[] mockErrors = { "DDoS Attempt", "SQL Injection", "Path Traversal", "XSS" };
            string[] mockSeverity = { "Thấp", "Trung bình", "Nghiêm trọng", "Nguy kịch" };

            var newLog = new ScanLog
            {
                Id = scanLogs.Count + 1,
                TargetUrl = "demo.ntt.edu.vn",
                VulnerabilityType = mockErrors[rnd.Next(mockErrors.Length)],
                Severity = mockSeverity[rnd.Next(mockSeverity.Length)],
                ScanTime = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy")
            };

            // Thêm vào vị trí đầu tiên
            scanLogs.Insert(0, newLog);

            // Xử lý xong thì load lại trang chủ để hiện dữ liệu mới
            return RedirectToAction("Index");
        }
    }
}