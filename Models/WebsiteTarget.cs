using System.ComponentModel.DataAnnotations;

namespace SecuCore.Models
{
    public class WebsiteTarget
    {
        [Key]
        public int Id { get; set; } // Khóa chính cho CSDL
        public string Url { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int Health { get; set; }
        public string Status { get; set; }
    }
}