using System.ComponentModel.DataAnnotations;

namespace SecuCore.Models
{
    public class WebsiteTarget
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; } // Ví dụ: qlsv.ntt.edu.vn

        public string Status { get; set; } // Đang theo dõi, Đã bảo vệ...

        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}