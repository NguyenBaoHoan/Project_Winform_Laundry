using LiveCharts.Wpf.Charts.Base;
using Project1_Laundry.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Project1_Laundry
{
    public partial class CustomerModule : Form
    {
        private readonly LaundryContextDB context;
        private readonly Customer customerForm;  // Tham chiếu tới form Customer
        bool check = false;
        string title = "Laundry Shop";
        internal int vid;

        public CustomerModule(Customer cust)
        {
            InitializeComponent();
            context = new LaundryContextDB();  // Khởi tạo DbContext
            customerForm = cust;  // Gán tham chiếu tới form chính
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();  // Đóng form
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkField();  // Kiểm tra dữ liệu
                if (check)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn đăng ký khách hàng này?",
                        "Đăng ký khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var newCustomer = new tbCustomer
                        {
                            idType = (int)cbType.SelectedValue,
                            name = txtName.Text,
                            phone = txtPhone.Text,
                            no = txtNo.Text,
                            model = txtModel.Text,
                            address = txtAddress.Text,
                            points = (int)udPoints.Value
                        };

                        context.tbCustomers.Add(newCustomer);  // Thêm vào DbSet
                        context.SaveChanges();  // Lưu thay đổi vào CSDL

                        MessageBox.Show("Khách hàng đã được đăng ký thành công!", title);

                        Clear();  // Xóa các trường dữ liệu sau khi lưu thành công
                        customerForm.LoadCustomerData(context.tbCustomers.ToList());  // Cập nhật danh sách khách hàng
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                checkField();  // Kiểm tra dữ liệu
                if (check)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn chỉnh sửa thông tin khách hàng này?",
                        "Chỉnh sửa thông tin khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (int.TryParse(lblCid.Text, out int customerId))
                        {
                            var customer = context.tbCustomers.Find(customerId);
                            if (customer != null)
                            {
                                customer.idType = (int)cbType.SelectedValue;
                                customer.name = txtName.Text;
                                customer.phone = txtPhone.Text;
                                customer.no = txtNo.Text;
                                customer.model = txtModel.Text;
                                customer.address = txtAddress.Text;
                                customer.points = (int)udPoints.Value;

                                context.SaveChanges();  // Lưu thay đổi

                                MessageBox.Show("Khách hàng đã được chỉnh sửa thành công!", title);

                                Clear();  // Xóa các trường dữ liệu sau khi cập nhật
                                customerForm.LoadCustomerData(context.tbCustomers.ToList());  // Cập nhật danh sách khách hàng
                                this.Dispose();  // Đóng form
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy khách hàng!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Clear();  // Xóa dữ liệu và đặt lại form
        }

        // Tải dữ liệu loại thiết bị vào combobox khi form được mở
        private void CustomerModule_Load(object sender, EventArgs e)
        {
            cbType.DataSource = context.tbTypes.ToList();  // Lấy danh sách loại thiết bị
            cbType.DisplayMember = "name";
            cbType.ValueMember = "id";


        }

        public void Clear()
        {
            txtName.Clear();
            txtPhone.Clear();
            txtNo.Clear();
            txtModel.Clear();
            txtAddress.Clear();
            cbType.SelectedIndex = 0;
            udPoints.Value = 0;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        public void checkField()
        {
            if (string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtPhone.Text) ||
                string.IsNullOrEmpty(txtNo.Text) ||
                string.IsNullOrEmpty(txtModel.Text) ||
                string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                check = false;
                return;
            }

            check = true;
        }

        
    }
}
