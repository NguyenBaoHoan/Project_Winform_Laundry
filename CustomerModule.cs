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

namespace Project1_Laundry
{
    public partial class CustomerModule : Form
    {
        SqlCommand cm = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        string title = "Hệ Thống Quản Lý Tiệm Sửa Chữa Đồ Gia Dụng";
        bool check = false;
        public int vid = 0;
        Customer customer;
        public CustomerModule(Customer cust)
        {
            InitializeComponent();
            customer = cust;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn đăng ký khách hàng này?", "Đăng ký khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbCustomer(vid, name, phone, no, model, address, points) VALUES(@vid, @name, @phone, @no, @model, @address, @points)", dbcon.connect());
                        cm.Parameters.AddWithValue("@vid", cbType.SelectedValue); // Lưu id của loại thiết bị
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@no", txtNo.Text); // Mã thiết bị
                        cm.Parameters.AddWithValue("@model", txtModel.Text); // Model thiết bị
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@points", udPoints.Value); // Sử dụng Value thay vì Text cho điểm số

                        dbcon.open(); // Mở kết nối
                        cm.ExecuteNonQuery();
                        dbcon.close(); // Đóng kết nối
                        MessageBox.Show("Khách hàng đã được đăng ký thành công!", title);
                        check = false;
                        Clear(); // Xóa các trường dữ liệu sau khi đã thêm thành công
                    }
                }
                customer.loadCustomer();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn chỉnh sửa thông tin khách hàng này?", "Chỉnh sửa thông tin khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tbCustomer SET vid = @vid, name = @name, phone = @phone, no = @no, model = @model, address = @address, points = @points WHERE id = @id", dbcon.connect());
                        cm.Parameters.AddWithValue("@id", lblCid.Text); // ID khách hàng
                        cm.Parameters.AddWithValue("@vid", cbType.SelectedValue); // Lưu id của loại thiết bị
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@no", txtNo.Text); // Mã thiết bị
                        cm.Parameters.AddWithValue("@model", txtModel.Text); // Model thiết bị
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@points", udPoints.Value); // Sử dụng Value thay vì Text cho điểm số

                        dbcon.open(); // Mở kết nối
                        cm.ExecuteNonQuery();
                        dbcon.close(); // Đóng kết nối
                        MessageBox.Show("Khách hàng đã được chỉnh sửa thành công!", title);
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        // Tải khi form bắt đầu
        private void CustomerModule_Load(object sender, EventArgs e)
        {
            // Thêm danh sách loại thiết bị vào combobox
            cbType.DataSource = loaiThietBi();
            cbType.DisplayMember = "name"; // Giữ nguyên tên trường
            cbType.ValueMember = "id";     // Giữ nguyên tên trường
            if (vid > 0)
                cbType.SelectedValue = vid;
        }

        #region method
        // Hàm để lấy danh sách loại thiết bị trả về bảng dữ liệu
        public DataTable loaiThietBi()
        {
            cm = new SqlCommand("SELECT * FROM tbType", dbcon.connect());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            adapter.SelectCommand = cm;
            adapter.Fill(dataTable);

            return dataTable;
        }

        // Hàm để xóa các trường dữ liệu
        public void Clear()
        {
            txtAddress.Clear();
            txtModel.Clear();
            txtNo.Clear();
            txtName.Clear();
            txtPhone.Clear();

            cbType.SelectedIndex = 0;
            udPoints.Value = 0;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        public void checkField()
        {
            if (txtAddress.Text == "" || txtName.Text == "" || txtPhone.Text == "" || txtNo.Text == "" || txtModel.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo");
                return; // Quay lại form để điền thông tin còn thiếu
            }

            check = true;
        }
        #endregion method
    }
}
