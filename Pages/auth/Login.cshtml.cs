using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SecuCore.Data;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecuCore.Pages.auth
{
    public class LoginModel : PageModel
    {
        private readonly SecuDbContext _db;
        public LoginModel(SecuDbContext db) { _db = db; }

        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }

        public string ErrorMessage { get; set; }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated) return RedirectToPage("/Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password)) return Page();

            string hashedInput = HashPassword(Password);
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == Email && u.PasswordHash == hashedInput);

            if (user == null)
            {
                ErrorMessage = "Email hoặc mật khẩu không chính xác!";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName ?? "User"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role ?? "Viewer")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToPage("/Index"); // Đăng nhập thành công bay vào Dashboard
        }
    }
}