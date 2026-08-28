using System.ComponentModel.DataAnnotations;

namespace SecuCore.Models
{
    public class WebsiteTarget
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TargetUrl { get; set; } // Ví dụ: qlsv.ntt.edu.vn

        public string TargetName { get; set; } // Ví dụ: Hệ thống QL Sinh viên

        public string IpAddress { get; set; }

        public int SecurityScore { get; set; } // Số % bảo mật (vd: 98)

        public string Status { get; set; } // "An toàn", "Cảnh báo", hoặc "Bị tấn công"
    }
}