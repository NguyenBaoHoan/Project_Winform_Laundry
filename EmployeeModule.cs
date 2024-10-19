using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Policy;
using System.Drawing.Drawing2D;

namespace Project1_Laundry
{
    public partial class EmployeeModule : Form
    {
        // Khai báo các đối tượng kết nối và câu lệnh SQL
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        string title = "Hệ Thống Quản Lý Tiệm Sửa Chữa Đồ Gia Dụng"; // Tiêu đề hệ thống
        bool kiemTra = false; // Biến để kiểm tra dữ liệu đầu vào
        Employer employer; // Đối tượng Employer để kết nối với form chính

        // Hàm khởi tạo với tham số Employer
        public EmployeeModule(Employer emp)
        {
            InitializeComponent();
            employer = emp; // Gán đối tượng Employer
            cbRole.SelectedIndex = 3; // Chọn mặc định "Công nhân" trong ComboBox
        }

        // Sự kiện đóng form khi nhấn nút đóng
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        // Sự kiện khi nhấn nút Lưu (Thêm nhân viên vào cơ sở dữ liệu)
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                kiemTraDuLieu(); // Kiểm tra các trường dữ liệu
                if (kiemTra)
                {
                    // Xác nhận nếu người dùng muốn thêm nhân viên mới
                    if (MessageBox.Show("Bạn có chắc chắn muốn đăng ký nhân viên này không?", "Đăng ký nhân viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Câu truy vấn thêm dữ liệu nhân viên vào bảng tbEmployer
                        cm = new SqlCommand("INSERT INTO tbEmployer(ten,dienthoai,diachi,ngaysinh,gioitinh,vaitro,luong,matkhau)VALUES(@ten,@dienthoai,@diachi,@ngaysinh,@gioitinh,@vaitro,@luong,@matkhau)", dbcon.connect());
                        cm.Parameters.AddWithValue("@ten", txtName.Text);
                        cm.Parameters.AddWithValue("@dienthoai", txtPhone.Text);
                        cm.Parameters.AddWithValue("@diachi", txtAddress.Text);
                        cm.Parameters.AddWithValue("@ngaysinh", dtDob.Value);
                        cm.Parameters.AddWithValue("@gioitinh", rdMale.Checked ? "Nam" : "Nữ"); // Kiểm tra giới tính
                        cm.Parameters.AddWithValue("@vaitro", cbRole.Text);
                        cm.Parameters.AddWithValue("@luong", txtSalary.Text);
                        cm.Parameters.AddWithValue("@matkhau", txtPassword.Text);

                        // Mở kết nối và thực thi câu truy vấn
                        dbcon.open();
                        cm.ExecuteNonQuery();
                        dbcon.close();

                        // Thông báo khi lưu thành công
                        MessageBox.Show("Nhân viên đã được đăng ký thành công!", title);
                        kiemTra = false; // Reset biến kiểm tra
                        XoaForm(); // Xóa dữ liệu trong các ô sau khi lưu
                        employer.loadEmployer(); // Cập nhật danh sách nhân viên trên form chính
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có vấn đề xảy ra
                MessageBox.Show(ex.Message, title);
            }
        }

        // Sự kiện khi nhấn nút Cập nhật (Cập nhật thông tin nhân viên)
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                kiemTraDuLieu(); // Kiểm tra dữ liệu trước khi cập nhật
                if (kiemTra)
                {
                    // Xác nhận nếu người dùng muốn cập nhật thông tin nhân viên
                    if (MessageBox.Show("Bạn có chắc chắn muốn chỉnh sửa thông tin này không?", "Chỉnh sửa nhân viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Câu truy vấn cập nhật thông tin nhân viên trong cơ sở dữ liệu
                        cm = new SqlCommand("UPDATE tbEmployer SET ten=@ten, dienthoai=@dienthoai, diachi=@diachi, ngaysinh=@ngaysinh, gioitinh=@gioitinh, vaitro=@vaitro, luong=@luong, matkhau=@matkhau WHERE id=@id", dbcon.connect());
                        cm.Parameters.AddWithValue("@id", lblEid.Text);
                        cm.Parameters.AddWithValue("@ten", txtName.Text);
                        cm.Parameters.AddWithValue("@dienthoai", txtPhone.Text);
                        cm.Parameters.AddWithValue("@diachi", txtAddress.Text);
                        cm.Parameters.AddWithValue("@ngaysinh", dtDob.Value);
                        cm.Parameters.AddWithValue("@gioitinh", rdMale.Checked ? "Nam" : "Nữ"); // Kiểm tra giới tính
                        cm.Parameters.AddWithValue("@vaitro", cbRole.Text);
                        cm.Parameters.AddWithValue("@luong", txtSalary.Text);
                        cm.Parameters.AddWithValue("@matkhau", txtPassword.Text);

                        // Mở kết nối và thực thi câu truy vấn
                        dbcon.open();
                        cm.ExecuteNonQuery();
                        dbcon.close();

                        // Thông báo khi cập nhật thành công
                        MessageBox.Show("Thông tin nhân viên đã được cập nhật thành công!", title);
                        XoaForm(); // Xóa dữ liệu sau khi cập nhật
                        this.Dispose(); // Đóng form sau khi cập nhật
                        employer.loadEmployer(); // Cập nhật danh sách nhân viên trên form chính
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có vấn đề xảy ra
                MessageBox.Show(ex.Message, title);
            }
        }

        // Sự kiện khi nhấn nút Hủy (Xóa form)
        private void btnCancel_Click(object sender, EventArgs e)
        {
            XoaForm(); // Xóa sạch các trường dữ liệu
            btnUpdate.Enabled = false; // Vô hiệu hóa nút Cập nhật
            btnSave.Enabled = true; // Kích hoạt nút Lưu
        }

        // Kiểm soát đầu vào trong ô lương (chỉ cho phép nhập số)
        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và dấu chấm
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true; // Không cho phép nhập
            }
           
        }


        // Hàm để xóa sạch các trường dữ liệu trong form
        #region method
        public void XoaForm()
        {
            txtAddress.Clear();
            txtName.Clear();
            txtPassword.Clear();
            txtPhone.Clear();
            txtSalary.Clear();
            dtDob.Value = DateTime.Now; // Đặt lại giá trị ngày sinh về ngày hiện tại
            cbRole.SelectedIndex = 3; // Chọn mặc định "Công nhân"
        }

        // Hàm kiểm tra các trường dữ liệu và ngày sinh
        public void kiemTraDuLieu()
        {
            // Kiểm tra nếu các trường bắt buộc chưa được điền
            if (txtAddress.Text == "" || txtName.Text == "" || txtPhone.Text == "" || txtSalary.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ các trường dữ liệu!", "Cảnh báo");
                return; // Dừng lại và quay lại form để người dùng nhập dữ liệu
            }

            // Kiểm tra nếu nhân viên dưới 18 tuổi
            if (kiemTraTuoi(dtDob.Value) < 18)
            {
                MessageBox.Show("Nhân viên chưa đủ 18 tuổi!", "Cảnh báo");
                return;
            }
            kiemTra = true; // Đánh dấu kiểm tra thành công
        }

        // Hàm kiểm tra tuổi (tính nếu dưới 18 tuổi)
        private static int kiemTraTuoi(DateTime ngaysinh)
        {
            int tuoi = DateTime.Now.Year - ngaysinh.Year;
            if (DateTime.Now.DayOfYear < ngaysinh.DayOfYear)
                tuoi = tuoi - 1; // Trừ thêm 1 năm nếu chưa qua ngày sinh trong năm hiện tại
            return tuoi;
        }

        #endregion method

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRole.Text == "Quản Lý")
            {
                this.Height = 453 - 27;
                txtPassword.Clear();
                lblPass.Visible = false; // hide password
                txtPassword.Visible = false;
            }
            else
            {
                lblPass.Visible = true;
                txtPassword.Visible = true;
                this.Height = 453;
            }
        }

        
    }
}
