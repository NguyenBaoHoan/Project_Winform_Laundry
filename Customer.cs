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
using System.Data.Common;
using System.Security.Cryptography;

namespace Project1_Laundry
{
    public partial class Customer : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect(); // Đối tượng kết nối với cơ sở dữ liệu
        SqlDataReader dr; // Đối tượng đọc dữ liệu từ cơ sở dữ liệu
        string title = "Hệ Thống Quản Lý Tiệm Sửa Chữa Đồ Gia Dụng"; // Tiêu đề của chương trình

        public Customer()
        {
            InitializeComponent();
            loadCustomer(); // Tải dữ liệu khách hàng khi form khởi động
        }

        // Sự kiện khi nhấn nút "Thêm" khách hàng
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CustomerModule module = new CustomerModule(this);
            module.btnUpdate.Enabled = false; // Vô hiệu hóa nút cập nhật khi thêm mới khách hàng
            module.ShowDialog(); // Mở form thêm mới khách hàng
        }

        // Xử lý sự kiện khi người dùng nhấp vào một ô trong bảng dữ liệu khách hàng
        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        // Tìm kiếm khách hàng khi người dùng nhập vào ô tìm kiếm
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadCustomer(); // Tải lại dữ liệu khách hàng với điều kiện tìm kiếm
        }

        #region method
        // Phương thức để tải danh sách khách hàng từ cơ sở dữ liệu
        public void loadCustomer()
        {
            int i = 0; // Biến để đánh số thứ tự cho khách hàng trong bảng
            dgvCustomer.Rows.Clear(); // Xóa dữ liệu cũ trong bảng
            cm = new SqlCommand("SELECT C.id, C.name, phone, no, model, V.name, address, points FROM tbCustomer AS C INNER JOIN tbType AS V ON C.vid = V.id WHERE CONCAT (C.name, no, model, address) LIKE '%" + txtSearch.Text + "%'", dbcon.connect());
            dbcon.open(); // Mở kết nối cơ sở dữ liệu
            dr = cm.ExecuteReader(); // Thực thi truy vấn và lấy kết quả
            while (dr.Read()) // Đọc dữ liệu từ cơ sở dữ liệu
            {
                i++;
                // Thêm từng dòng dữ liệu vào bảng DataGridView
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
            dbcon.close(); // Đóng kết nối cơ sở dữ liệu
        }

        // Phương thức để lấy ID của loại thiết bị dựa vào tên loại thiết bị
        public int loaiThietBiIdByName(string str)
        {
            int i = 0;
            cm = new SqlCommand("SELECT id FROM tbType WHERE name LIKE '" + str + "' ", dbcon.connect());
            dbcon.open();
            dr = cm.ExecuteReader(); // Thực thi truy vấn và lấy kết quả
            dr.Read();
            if (dr.HasRows)
            {
                i = int.Parse(dr["id"].ToString()); // Lấy ID của loại thiết bị
            }
            dbcon.close(); // Đóng kết nối cơ sở dữ liệu
            return i; // Trả về ID loại thiết bị
        }
        #endregion method

        private void dgvCustomer_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name; // Lấy tên của cột được nhấp
            if (colName == "Edit")
            {
                // Đưa dữ liệu của khách hàng được chọn vào form CustomerModule để chỉnh sửa
                CustomerModule module = new CustomerModule(this);
                module.lblCid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                module.txtNo.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString(); // mã thiết bị
                module.txtModel.Text = dgvCustomer.Rows[e.RowIndex].Cells[5].Value.ToString(); // model thiết bị
                module.vid = loaiThietBiIdByName(dgvCustomer.Rows[e.RowIndex].Cells[6].Value.ToString());
                module.txtAddress.Text = dgvCustomer.Rows[e.RowIndex].Cells[7].Value.ToString();
                module.udPoints.Text = dgvCustomer.Rows[e.RowIndex].Cells[8].Value.ToString();

                module.btnSave.Enabled = false; // Vô hiệu hóa nút "Lưu" khi đang chỉnh sửa
                module.udPoints.Enabled = true;
                module.ShowDialog(); // Mở form CustomerModule
            }
            else if (colName == "Delete") // Khi người dùng nhấn nút "Xóa"
            {
                try
                {
                    // Hiển thị hộp thoại xác nhận xóa
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này không?", "Xóa bản ghi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Câu truy vấn xóa khách hàng từ cơ sở dữ liệu
                        cm = new SqlCommand("DELETE FROM tbCustomer WHERE id LIKE'" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dbcon.connect());
                        dbcon.open();
                        cm.ExecuteNonQuery(); // Thực thi câu lệnh xóa
                        dbcon.close();
                        MessageBox.Show("Dữ liệu khách hàng đã được xóa thành công!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title); // Hiển thị thông báo lỗi nếu xảy ra
                }
            }
            loadCustomer(); // Tải lại dữ liệu khách hàng sau khi xóa
        }
    }
}
