<div align="center">

# 🧺 Laundry Management System

![Language](https://img.shields.io/badge/LANGUAGE-C%23-blue?style=for-the-badge&logo=csharp)
![Platform](https://img.shields.io/badge/PLATFORM-Windows%20Forms-lightgrey?style=for-the-badge&logo=windows)
![Database](https://img.shields.io/badge/DATABASE-SQL%20Server-red?style=for-the-badge&logo=microsoft-sql-server)
![IDE](https://img.shields.io/badge/IDE-Visual%20Studio%202022-purple?style=for-the-badge&logo=visual-studio)

**A comprehensive desktop solution designed to streamline operations for small to medium-sized laundry businesses.** *Optimize workflows from customer reception to service processing, billing, and financial reporting.*

[Report Bug](https://github.com/NguyenBaoHoan/Project_Winform_Laundry/issues) · [Request Feature](https://github.com/NguyenBaoHoan/Project_Winform_Laundry/issues)

</div>

---

## 📸 App Showcase

A visual tour of the application interface, featuring the Dashboard, POS, Management tools, and Reporting modules.

<div align="center">
  <img src="Screenshot 2025-11-25 142853.png" width="32%" alt="Dashboard" />
  <img src="Screenshot 2025-11-25 143034.png" width="32%" alt="Feature" />
  <img src="Screenshot 2025-11-25 143026.png" width="32%" alt="Feature" />
  <br/><br/>
  <img src="Screenshot 2025-11-25 143019.png" width="32%" alt="Feature" />
  <img src="Screenshot 2025-11-25 143010.png" width="32%" alt="Feature" />
  <img src="Screenshot 2025-11-25 143002.png" width="32%" alt="Feature" />
  <br/><br/>
  <img src="Screenshot 2025-11-25 142954.png" width="32%" alt="Feature" />
  <img src="Screenshot 2025-11-25 142942.png" width="32%" alt="Feature" />
  <img src="Screenshot 2025-11-25 142935.png" width="32%" alt="Feature" />
  <br/><br/>
  <img src="Screenshot 2025-11-25 142924.png" width="32%" alt="Feature" />
  <img src="Screenshot 2025-11-25 142912.png" width="32%" alt="Feature" />
  <img src="Screenshot 2025-11-25 142903.png" width="32%" alt="Feature" />
</div>

---

## 🚀 Key Features

### 1. 👔 Service & Inventory Management
* **Flexible Catalog:** Manage various services (Dry Clean, Wash & Fold, Ironing, etc.).
* **Pricing Engine:** Configure prices based on item type (clothing material, weight, or unit).

### 2. 👥 Customer Relationship Management (CRM)
* **Profiles:** Store detailed customer contact information.
* **History:** Track transaction history, order status, and outstanding debts.

### 3. 💰 Point of Sale (POS) & Payments
* **Intuitive UI:** Fast and user-friendly sales interface for quick checkout.
* **Automated Billing:** Auto-calculate totals, discounts, and generate receipts.
* **Digital Payment:** Integrated **MoMo QR Code generation** for seamless cashless payments (`CreateQRMoMo`).

### 4. 📊 Analytics & Reporting
* **Financial Insights:** View revenue by day, month, or year.
* **Cost Control:** Manage operational expenses (`CostManagement`).
* **Exportable Reports:** High-quality reports powered by **Microsoft RDLC Report Viewer**.

### 5. 🔐 Administration & Security
* **Secure Access:** Login system with encrypted credentials (`LoginForm`).
* **Staff Management:** Manage employee profiles (`Employee`) and assign role-based permissions.
* **System Settings:** Global configuration for shop details and preferences.

---

## 🛠 Tech Stack

This project is built using the robust Microsoft ecosystem:

* **Language:** C# (.NET Framework)
* **GUI Framework:** Windows Forms (WinForms)
* **Database:** Microsoft SQL Server (LocalDB `.mdf` file based)
* **Reporting:** Microsoft RDLC Report
* **Libraries:** * *QRCoder* (For payment integration)
    * *Guna UI* (For modern UI components)

---

## ⚙️ Installation & Setup

Follow these steps to run the project locally:

1.  **Clone the Repository**
    ```bash
    git clone [https://github.com/NguyenBaoHoan/Project_Winform_Laundry.git](https://github.com/NguyenBaoHoan/Project_Winform_Laundry.git)
    ```

2.  **Open the Solution**
    * Launch **Visual Studio 2022**.
    * Open the `Project1_Laundry.sln` file.

3.  **Database Configuration**
    * Ensure **SQL Server** (or SQL Server Express LocalDB) is installed.
    * Check the `App.config` or `dbConnect.cs` file. Update the **Connection String** to point to the local path of the `DBLaundryWash.mdf` file included in the project folder.

4.  **Build & Run**
    * Press `F5` or click **Start** to compile and run the application.

---

## 👤 Author

**NguyenBaoHoan**

* **Github:** [@NguyenBaoHoan](https://github.com/NguyenBaoHoan)

---
*This project was developed for educational purposes and as a practical solution for managing laundry service businesses.*
