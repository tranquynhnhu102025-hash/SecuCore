using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using SecuCore.Models;

namespace SecuCore.Data
{
    public class SecuDbContext : DbContext
    {
        public SecuDbContext(DbContextOptions<SecuDbContext> options) : base(options) { }

        // Tạo bảng Users trong SQL Server
        public DbSet<User> Users { get; set; }
        public DbSet<ScanLog> ScanLogs { get; set; } // Thêm dòng này để tạo bảng Nhật ký
        public DbSet<WebsiteTarget> WebsiteTargets { get; set; }
        public DbSet<VulnerabilityLog> VulnerabilityLogs { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tự động tạo tài khoản Admin thật vào Database khi khởi tạo
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Email = "admin@ntt.edu.vn",
                // Mật khẩu "Admin@123" đã được băm bằng SHA-256
                PasswordHash = "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9",
                FullName = "Trần Quỳnh Như",
                Role = "SystemAdmin"
            });
        }
    }
}