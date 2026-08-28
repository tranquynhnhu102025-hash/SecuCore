using System.ComponentModel.DataAnnotations;

namespace SecuCore.Models
{
    public class SystemSetting
    {
        [Key]
        public int Id { get; set; }

        public int Threads { get; set; } = 16;

        public int Timeout { get; set; } = 5000;

        public string? AuthCookie { get; set; }

        public string? UserAgent { get; set; }

        public int ScanDepth { get; set; } = 3;

        public bool EnableHeuristics { get; set; } = true;

        public string? WebhookUrl { get; set; }

        public string? TelegramToken { get; set; }

        public bool UseTorProxy { get; set; } = false;

        public string? IpWhitelist { get; set; }

        public bool Require2FA { get; set; } = false;

        public bool AutoLockAccount { get; set; } = true;
    }
}