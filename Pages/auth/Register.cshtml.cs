using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecuCore.Pages.auth
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {
        }
        // Bấm nút Đăng ký xong sẽ chuyển về trang Đăng nhập
        public IActionResult OnPost()
        {
            return RedirectToPage("/auth/Login");
        }
    }
}
