# 🛡️ SecuCore - Hệ thống Giám sát & Phân tích Lỗ hổng Bảo mật Website

Chào mừng đến với kho lưu trữ mã nguồn của dự án **SecuCore**! Đây là một hệ thống giám sát an ninh mạng chủ động, cung cấp bảng điều khiển trực quan giúp phát hiện và cảnh báo sớm các rủi ro bảo mật (SQL Injection, XSS, DDoS...) trên các ứng dụng Web. 

## 👨‍💻 Thông tin đồ án
* **Đề tài:** Phát triển hệ thống giám sát và phân tích lỗ hổng bảo mật website (SecuCore)
* **Sinh viên thực hiện:** Trần Quỳnh Như - 2400008936
* **Giảng viên hướng dẫn:** ThS. Đỗ Gia Bảo
* **Đơn vị:** Khoa Công nghệ Thông tin - Trường Đại học Nguyễn Tất Thành

## 🚀 Công nghệ sử dụng
* **Backend:** C# ASP.NET Core (Razor Pages)
* **Cơ sở dữ liệu:** SQL Server & Entity Framework Core
* **Frontend:** HTML5, CSS3, Bootstrap 5, Javascript
* **Trực quan hóa dữ liệu:** Chart.js (Line Chart, Doughnut Chart)

## ⚙️ Các chức năng nổi bật
- [x] Quản lý danh sách các Website mục tiêu cần giám sát.
- [x] Giả lập tiến trình rà quét và tự động phát hiện lỗ hổng.
- [x] Giao diện Dashboard Dark/Cyber Theme chuyên nghiệp.
- [x] Trực quan hóa số liệu tài nguyên (CPU, RAM, Network) theo thời gian thực (Real-time).
- [x] Lưu trữ nhật ký quét lỗi và cảnh báo mức độ nguy hiểm.
- [ ] 
## 🎥 Video Demo
Kính mời Hội đồng đánh giá xem trước video demo trực tiếp các luồng chức năng cốt lõi của hệ thống tại đây: 
🔗 **https://drive.google.com/file/d/1mEjh_FQKvvEo8ndEp6P9vPk95nb9DVKH/view?usp=drive_link**
## 🛠️ Hướng dẫn chạy dự án (Dành cho Giảng viên/Người đánh giá)
Để chạy dự án này trên máy cá nhân (Local environment), vui lòng thực hiện các bước sau:

1. **Clone mã nguồn về máy:**
   git clone https://github.com/tranquynhnhu102025-hash/SecuCore.git

2. **Mở dự án:**
   Mở file Solution (`SecuCore.sln`) bằng phần mềm **Visual Studio 2022**.

3. **Cấu hình Cơ sở dữ liệu:**
   Mở file `appsettings.json` và thay đổi chuỗi kết nối `DefaultConnection` sao cho khớp với cấu hình SQL Server trên máy của bạn.

4. **Tạo Database (Cập nhật EF Core):**
   Mở cửa sổ **Package Manager Console** (Tools > NuGet Package Manager > Package Manager Console) và chạy lệnh: Update-Database

5. **Khởi động:**
   Nhấn `Ctrl + F5` hoặc nút Run (IIS Express) để khởi chạy trang web.
