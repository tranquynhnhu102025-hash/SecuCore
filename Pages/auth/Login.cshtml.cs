using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecuCore.Pages.auth
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            // Lệnh này ra chỉ thị: "Đẩy người dùng về trang chủ (Index)"
            return RedirectToPage("/Index");
        }
    }
}
