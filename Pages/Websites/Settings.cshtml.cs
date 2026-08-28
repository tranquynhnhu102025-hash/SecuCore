using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SecuCore.Data; // Thay bằng namespace DbContext của bạn
using SecuCore.Models;
using System.Threading.Tasks;

namespace SecuCore.Pages.Websites
{
    public class SettingsModel : PageModel
    {
        private readonly SecuDbContext _context;

        public SettingsModel(SecuDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SystemSetting CurrentSetting { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            // Lấy dòng cấu hình đầu tiên trong Database (nếu chưa có thì tạo mẫu mặc định)
            CurrentSetting = await _context.SystemSettings.FirstOrDefaultAsync();

            if (CurrentSetting == null)
            {
                CurrentSetting = new SystemSetting
                {
                    Threads = 16,
                    Timeout = 5000,
                    ScanDepth = 3,
                    UserAgent = "SecuCore-Security-Scanner/1.0"
                };
                _context.SystemSettings.Add(CurrentSetting);
                await _context.SaveChangesAsync();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var settingInDb = await _context.SystemSettings.FirstOrDefaultAsync();

            if (settingInDb != null)
            {
                // Cập nhật thông số mới do người dùng vừa nhập trên giao diện
                settingInDb.Threads = CurrentSetting.Threads;
                settingInDb.Timeout = CurrentSetting.Timeout;
                settingInDb.AuthCookie = CurrentSetting.AuthCookie;
                settingInDb.UserAgent = CurrentSetting.UserAgent;
                settingInDb.ScanDepth = CurrentSetting.ScanDepth;
                settingInDb.EnableHeuristics = CurrentSetting.EnableHeuristics;
                settingInDb.WebhookUrl = CurrentSetting.WebhookUrl;
                settingInDb.TelegramToken = CurrentSetting.TelegramToken;
                settingInDb.UseTorProxy = CurrentSetting.UseTorProxy;
                settingInDb.IpWhitelist = CurrentSetting.IpWhitelist;
                settingInDb.Require2FA = CurrentSetting.Require2FA;
                settingInDb.AutoLockAccount = CurrentSetting.AutoLockAccount;

                await _context.SaveChangesAsync();
            }

            // Đồng bộ thành công thì load lại trang
            return RedirectToPage();
        }
    }
}