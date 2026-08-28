using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SecuCore.Data; // Tên thư mục chứa SecuDbContext của bạn
using SecuCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecuCore.Pages.Websites
{
    public class QuanLyModel : PageModel
    {
        private readonly SecuDbContext _context;

        public QuanLyModel(SecuDbContext context)
        {
            _context = context;
        }

        // Chính là cái biến DanhSachWeb đang bị thiếu nè!
        public IList<WebsiteTarget> DanhSachWeb { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Lôi toàn bộ danh sách Web từ Database SQL ra
            DanhSachWeb = await _context.WebsiteTargets.ToListAsync();
        }
    }
}