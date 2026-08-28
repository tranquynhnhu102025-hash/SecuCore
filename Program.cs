using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SecuCore.Data;
using SecuCore.Models;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Kết nối SQL Server
builder.Services.AddDbContext<SecuDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cấu hình Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/Login";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// TỰ ĐỘNG TẠO TÀI KHOẢN ĐỂ TEST ĐĂNG NHẬP
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SecuDbContext>();
    db.Database.EnsureCreated();

    db.Database.EnsureDeleted(); // Mở khóa dòng này để nó đập DB cũ đi
    db.Database.EnsureCreated(); // Xây lại DB mới tinh có bảng ScanLogs

    // ... (Đoạn tạo tài khoản giữ nguyên)

    // Kiểm tra: Nếu chưa có tài khoản demo@gmail.com thì bắt buộc phải tạo mới
    if (!db.Users.Any(u => u.Email == "demo@gmail.com"))
    {
        var sha256 = System.Security.Cryptography.SHA256.Create();
        byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes("123456"));
        var hashBuilder = new System.Text.StringBuilder();
        for (int i = 0; i < bytes.Length; i++) hashBuilder.Append(bytes[i].ToString("x2"));

        db.Users.Add(new User
        {
            Id = Guid.NewGuid(),
            FullName = "Tài khoản Test Gấp",
            Email = "demo@gmail.com",
            PasswordHash = hashBuilder.ToString(),
            Role = "SystemAdmin"
        });
        // Bơm dữ liệu giả cho bảng ScanLogs để Dashboard nhảy số
        if (!db.ScanLogs.Any())
        {
            db.ScanLogs.AddRange(
                new ScanLog { ScanTime = DateTime.Now.AddMinutes(-30), TargetUrl = "http://vuln-site.com", VulnerabilityType = "SQL Injection", IsFixed = false },
                new ScanLog { ScanTime = DateTime.Now.AddHours(-2), TargetUrl = "http://test-target.net", VulnerabilityType = "XSS", IsFixed = true },
                new ScanLog { ScanTime = DateTime.Now.AddDays(-1), TargetUrl = "http://demo-bank.vn", VulnerabilityType = "DDoS", IsFixed = false }
            );
            db.SaveChanges();
        }
        if (!db.WebsiteTargets.Any())
        {
            db.WebsiteTargets.AddRange(
                new WebsiteTarget { TargetUrl = "qlsv.ntt.edu.vn", TargetName = "Hệ thống QL Sinh viên", IpAddress = "103.14.232.12", SecurityScore = 98, Status = "An toàn" },
                new WebsiteTarget { TargetUrl = "elearning.ntt.edu.vn", TargetName = "Cổng học trực tuyến", IpAddress = "103.14.232.45", SecurityScore = 65, Status = "Cảnh báo" },
                new WebsiteTarget { TargetUrl = "crm.doanhnghiep.vn", TargetName = "Máy chủ CRM", IpAddress = "192.168.1.100", SecurityScore = 20, Status = "Bị tấn công" }
            );
            db.SaveChanges();
        }
        if (!db.VulnerabilityLogs.Any())
        {
            db.VulnerabilityLogs.AddRange(
                new VulnerabilityLog { Severity = "CRITICAL", ScanTime = "Vừa xong", VulnName = "SQL Injection (Time-based)", TargetPath = "/api/v1/users/login", Status = "Đang tấn công" },
                new VulnerabilityLog { Severity = "HIGH", ScanTime = "10 phút trước", VulnName = "Cross-Site Scripting (XSS Stored)", TargetPath = "/comments/post_id=128", Status = "Nguy hiểm" }
            );
            db.SaveChanges();
        }
        db.SaveChanges();
    }

}
app.Run();