# 🧺 Laundry Management System (Hệ Thống Quản Lý Giặt Ủi)

![Language](https://img.shields.io/badge/LANGUAGE-C%23-blue?style=for-the-badge&logo=csharp)
![Platform](https://img.shields.io/badge/PLATFORM-Windows%20Forms-lightgrey?style=for-the-badge&logo=windows)
![Database](https://img.shields.io/badge/DATABASE-SQL%20Server-red?style=for-the-badge&logo=microsoft-sql-server)
![IDE](https://img.shields.io/badge/IDE-Visual%20Studio%202022-purple?style=for-the-badge&logo=visual-studio)

> **Dự án phần mềm quản lý tiệm giặt ủi toàn diện, được xây dựng trên nền tảng .NET Framework (WinForms).** 
> Ứng dụng giúp tối ưu hóa quy trình vận hành, từ quản lý khách hàng, dịch vụ, nhân viên đến thanh toán và báo cáo doanh thu chi tiết.

---

## 📸 Giao Diện Phần Mềm (Screenshots)

Dưới đây là một số hình ảnh thực tế từ ứng dụng:

<div align="center">
  <img src="Screenshot%202025-11-25%20142853.png" width="45%" alt="Dashboard Main"/>
  <img src="Screenshot%202025-11-25%20143034.png" width="45%" alt="Feature 2"/>
  <br/>
  <img src="Screenshot%202025-11-25%20143026.png" width="45%" alt="Feature 3"/>
  <img src="Screenshot%202025-11-25%20143019.png" width="45%" alt="Feature 4"/>
  <br/>
  <!-- Thêm các hình ảnh khác nếu cần -->
  <img src="Screenshot%202025-11-25%20143010.png" width="30%" />
  <img src="Screenshot%202025-11-25%20143002.png" width="30%" />
  <img src="Screenshot%202025-11-25%20142954.png" width="30%" />
</div>

---

## 🚀 Tính Năng Chính (Key Features)

Dựa trên cấu trúc mã nguồn, phần mềm cung cấp các chức năng mạnh mẽ:

### 1. 👔 Quản Lý Dịch Vụ & Sản Phẩm
*   Thiết lập danh sách dịch vụ (Giặt sấy, Giặt hấp, Ủi đồ...).
*   Quản lý giá cả và các loại quần áo/đồ giặt khác nhau.

### 2. 👥 Quản Lý Khách Hàng (CRM)
*   Lưu trữ thông tin khách hàng chi tiết.
*   Theo dõi lịch sử giao dịch và công nợ.

### 3. 💰 Thu Ngân & Thanh Toán (POS)
*   Giao diện bán hàng trực quan, dễ sử dụng.
*   Tự động tính toán tổng tiền, in hóa đơn.
*   **Tích hợp thanh toán hiện đại**: Hỗ trợ tạo mã QR thanh toán (MoMo) (`CreateQRMoMo`).

### 4. 📊 Báo Cáo & Thống Kê
*   Báo cáo doanh thu theo ngày, tháng, năm.
*   Quản lý chi phí vận hành (`CostManagement`).
*   Xuất báo cáo chi tiết (Sử dụng Report Viewer/RDLC).

### 5. 🔐 Quản Trị Hệ Thống
*   Đăng nhập bảo mật (`LoginForm`).
*   Quản lý nhân viên (`Employee`) và phân quyền sử dụng.
*   Cài đặt hệ thống (`Setting`).

---

## 🛠 Công Nghệ Sử Dụng

*   **Ngôn ngữ lập trình**: C#
*   **Framework**: .NET Windows Forms
*   **Cơ sở dữ liệu**: Microsoft SQL Server (LocalDB `.mdf`)
*   **Công cụ báo cáo**: Microsoft RDLC Report
*   **Thư viện bên thứ 3**: Guna UI (nếu có dùng cho giao diện đẹp), QRCoder (cho MoMo).

---

## ⚙️ Cài Đặt (Installation)

1.  **Clone dự án**:
    ```bash
    git clone https://github.com/NguyenBaoHoan/Project_Winform_Laundry.git
    ```
2.  **Mở dự án**: Khởi động Visual Studio và mở file `Project1_Laundry.sln`.
3.  **Cấu hình CSDL**: 
    *   Đảm bảo SQL Server đã được cài đặt.
    *   Kiểm tra chuỗi kết nối (Connection String) trong file `App.config` hoặc `dbConnect.cs` để trỏ đúng đến file `DBLaundryWash.mdf`.
4.  **Build & Run**: Nhấn `F5` để chạy ứng dụng.

---

## 👤 Tác Giả

**NguyenBaoHoan**

*   Github: [@NguyenBaoHoan](https://github.com/NguyenBaoHoan)

---
*Dự án được phát triển nhằm mục đích học tập và áp dụng vào thực tế quản lý cửa hàng giặt ủi nhỏ và vừa.*
