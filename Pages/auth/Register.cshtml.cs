using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SecuCore.Data;
using SecuCore.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecuCore.Pages.auth
{
    public class RegisterModel : PageModel
    {
        private readonly SecuDbContext _db;

        public RegisterModel(SecuDbContext db)
        {
            _db = db;
        }

        [BindProperty] public string FullName { get; set; }
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }
        [BindProperty] public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }

        // Đã cập nhật hàm băm mật khẩu theo chuẩn .NET hiện đại (Gọn gàng & Tối ưu hơn)
        private string HashPassword(string password)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLower();
        }

        public void OnGet()
        {
            // Trả về giao diện đăng ký bình thường
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // 1. Kiểm tra mật khẩu xác nhận
            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Mật khẩu xác nhận không khớp!";
                return Page();
            }

            // 2. Kiểm tra Email đã tồn tại trong Database chưa
            if (await _db.Users.AnyAsync(u => u.Email == Email))
            {
                ErrorMessage = "Email này đã được sử dụng!";
                return Page();
            }

            // 3. Khởi tạo User mới với quyền mặc định là Viewer
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FullName = FullName,
                Email = Email,
                PasswordHash = HashPassword(Password),
                Role = "Viewer"
            };

            // 4. Lưu vào cơ sở dữ liệu
            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();

            // 5. Thành công -> Đẩy thẳng về trang Đăng nhập
            return RedirectToPage("/auth/Login");
        }
    }
}