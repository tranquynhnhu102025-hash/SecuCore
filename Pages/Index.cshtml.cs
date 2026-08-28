using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SecuCore.Data;
using SecuCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuCore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SecuDbContext _context;

        public IndexModel(SecuDbContext context)
        {
            _context = context;
        }

        public int TongSoMucTieu { get; set; }
        public int SoLoHongNguyHiem { get; set; }
        public int SoCanhBao { get; set; }
        public IList<VulnerabilityLog> DanhSachNhatKy { get; set; }

        public async Task OnGetAsync()
        {
            // Lấy dữ liệu đếm số lượng hiển thị lên Dashboard
            TongSoMucTieu = await _context.WebsiteTargets.CountAsync();
            SoLoHongNguyHiem = await _context.VulnerabilityLogs.CountAsync(v => v.Severity == "CRITICAL" || v.Severity == "HIGH");
            SoCanhBao = 3;

            // Lấy 5 bản ghi mới nhất cho bảng Nhật ký
            DanhSachNhatKy = await _context.VulnerabilityLogs
                .OrderByDescending(v => v.Id)
                .Take(5)
                .ToListAsync();
        }

        // HÀM XỬ LÝ KHI BẤM NÚT QUÉT
        public async Task<IActionResult> OnPostRunScanAsync()
        {
            // 1. Giả lập tiến trình quét
            await Task.Delay(1000);

            // 2. Tạo danh sách các lỗ hổng ngẫu nhiên
            var loaiLoHong = new[] {
                "SQL Injection (Time-based)",
                "Cross-Site Scripting (XSS Stored)",
                "Directory Traversal / LFI",
                "Zero-Day Remote Code Execution (RCE)",
                "DDoS / UDP Flood Attack"
            };
            var mucTieu = new[] {
                "/api/v1/users/login",
                "/comments/post_id=128",
                "/download?file=../../etc/passwd",
                "/api/v1/system/execute-shell",
                "http://demo-bank.vn"
            };
            var mucDo = new[] { "CRITICAL", "HIGH", "MEDIUM", "CRITICAL", "HIGH" };

            // Chọn ngẫu nhiên 1 lỗi
            Random rnd = new Random();
            int index = rnd.Next(loaiLoHong.Length);

            // 3. Đẩy lỗi ngẫu nhiên vào Database
            var newLog = new VulnerabilityLog
            {
                Severity = mucDo[index],
                ScanTime = DateTime.Now.ToString("HH:mm"),
                VulnName = loaiLoHong[index],
                TargetPath = mucTieu[index],
                Status = "Chưa vá"
            };

            _context.VulnerabilityLogs.Add(newLog);
            await _context.SaveChangesAsync();

            // 4. Load lại trang Dashboard
            return RedirectToPage();
        }
    }
}