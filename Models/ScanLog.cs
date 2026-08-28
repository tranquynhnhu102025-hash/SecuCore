using System;

namespace SecuCore.Models
{
    public class ScanLog
    {
        public int Id { get; set; }
        public DateTime ScanTime { get; set; }
        public string TargetUrl { get; set; } = null!;
        public string VulnerabilityType { get; set; } = null!;
        public bool IsFixed { get; set; }
    }
}