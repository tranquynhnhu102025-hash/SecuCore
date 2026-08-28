using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SecuCore.Data;
using SecuCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecuCore.Pages.Websites
{
    public class CanhBaoLoiModel : PageModel
    {
        private readonly SecuDbContext _context;

        public CanhBaoLoiModel(SecuDbContext context)
        {
            _context = context;
        }

        public IList<VulnerabilityLog> DanhSachLoi { get; set; } = default!;

        public async Task OnGetAsync()
        {
            DanhSachLoi = await _context.VulnerabilityLogs.ToListAsync();
        }
    }
}