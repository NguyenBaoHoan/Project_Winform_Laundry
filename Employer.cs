using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;

namespace Project1_Laundry
{
    public partial class Employer : Form
    {
        // Khai báo các biến cần thiết
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        string title = "Laundry Management System"; // Tên hệ thống quản lý
        SqlDataReader dr;

        public Employer()
        {
            InitializeComponent();
            loadEmployer(); // Gọi hàm loadEmployer() khi bắt đầu form
        }

        // Hàm load dữ liệu nhân viên từ cơ sở dữ liệu
        public void loadEmployer()
        {
            try
            {
                int i = 0; // Đếm số lượng nhân viên
                dgvEmployer.Rows.Clear(); // Xoá các hàng cũ trong DataGridView

                // Truy vấn SQL để lấy danh sách nhân viên theo tìm kiếm từ textbox
                cm = new SqlCommand("SELECT * FROM tbEmployer WHERE CONCAT(id, name, address, role) LIKE N'%" + txtSearch.Text + "%'", dbcon.connect());
                dbcon.open();
                dr = cm.ExecuteReader();

                // Duyệt qua từng hàng dữ liệu và thêm vào DataGridView
                while (dr.Read())
                {
                    i++;
                    dgvEmployer.Rows.Add(i,
                        dr[0].ToString(), // id
                        dr[1].ToString(), // name
                        dr[2].ToString(), // phone
                        dr[3].ToString(), // address
                        DateTime.Parse(dr[4].ToString()).ToShortDateString(), // dob (ngày sinh)
                        dr[5].ToString(), // gender
                        dr[6].ToString(), // role (chức vụ)
                        dr[7].ToString(), // salary
                        dr[8].ToString()); // password
                }
                dbcon.close(); // Đóng kết nối với cơ sở dữ liệu

            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có vấn đề xảy ra
                MessageBox.Show(ex.Message, title);
            }
        }

        // Sự kiện click vào nút Thêm mới nhân viên
        private void btnAdd_Click(object sender, EventArgs e)
        {
            EmployeeModule module = new EmployeeModule(this); // Mở form EmployeeModule
            module.btnUpdate.Enabled = false; // Ẩn nút Cập nhật, chỉ cho phép lưu mới
            module.ShowDialog(); // Hiển thị form
        }

        // Sự kiện khi click vào các ô trong DataGridView
        private void dgvEmployer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvEmployer.Columns[e.ColumnIndex].Name; // Lấy tên cột được click

            if (colName == "Edit") // Nếu người dùng click vào nút Sửa
            {
                // Lấy dữ liệu nhân viên từ hàng đã chọn và đưa vào form sửa
                EmployeeModule module = new EmployeeModule(this);
                module.lblEid.Text = dgvEmployer.Rows[e.RowIndex].Cells[1].Value.ToString(); // id
                module.txtName.Text = dgvEmployer.Rows[e.RowIndex].Cells[2].Value.ToString(); // name
                module.txtPhone.Text = dgvEmployer.Rows[e.RowIndex].Cells[3].Value.ToString(); // phone
                module.txtAddress.Text = dgvEmployer.Rows[e.RowIndex].Cells[4].Value.ToString(); // address
                module.dtDob.Text = dgvEmployer.Rows[e.RowIndex].Cells[5].Value.ToString(); // dob
                // Giới tính (Male/Female)
                module.rdMale.Checked = dgvEmployer.Rows[e.RowIndex].Cells[6].Value.ToString() == "Male" ? true : false;
                module.cbRole.Text = dgvEmployer.Rows[e.RowIndex].Cells[7].Value.ToString(); // role
                module.txtSalary.Text = dgvEmployer.Rows[e.RowIndex].Cells[8].Value.ToString(); // salary
                module.txtPassword.Text = dgvEmployer.Rows[e.RowIndex].Cells[9].Value.ToString(); // password

                module.btnSave.Enabled = false; // Vô hiệu nút Lưu, chỉ cho phép cập nhật
                module.ShowDialog(); // Hiển thị form
            }
            else if (colName == "Delete") // Nếu người dùng click vào nút Xoá
            {
                try
                {
                    // Xác nhận việc xoá bản ghi
                    if (MessageBox.Show("Bạn có chắc muốn xoá dòng này không?", "Xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Truy vấn SQL để xoá nhân viên theo id
                        cm = new SqlCommand("DELETE FROM tbEmployer WHERE id LIKE '" + dgvEmployer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dbcon.connect());
                        dbcon.open();
                        cm.ExecuteNonQuery(); // Thực thi truy vấn xoá
                        dbcon.close(); // Đóng kết nối
                        MessageBox.Show("Nhân viên đã được xoá thành công!", title, MessageBoxButtons.OK, MessageBoxIcon.Information); // Thông báo xoá thành công
                        loadEmployer(); // Tải lại danh sách nhân viên
                    }
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu có vấn đề xảy ra
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // Sự kiện thay đổi văn bản trong ô tìm kiếm (guna2TextBox1)
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            loadEmployer(); // Tải lại danh sách nhân viên theo từ khoá tìm kiếm
        }
    }
}
