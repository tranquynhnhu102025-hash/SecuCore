using System.ComponentModel.DataAnnotations;

namespace SecuCore.Models
{
    public class ScanLog
    {
        [Key]
        public int Id { get; set; }

        public string TargetUrl { get; set; } // Web bị quét

        public string VulnerabilityType { get; set; } // SQL Injection, XSS...

        public string Severity { get; set; } // Nguy hiểm, Cảnh báo...

        public DateTime ScanTime { get; set; } = DateTime.Now;

        public bool IsFixed { get; set; } = false; // Trạng thái: Đã vá hay chưa
    }
}