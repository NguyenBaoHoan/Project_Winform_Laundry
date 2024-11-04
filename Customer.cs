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
using Project1_Laundry.Models;
using System.Windows.Controls;
using System.Data.Entity;

namespace Project1_Laundry
{
    public partial class Customer : Form
    {
        private readonly LaundryContextDB context;  // Khởi tạo DbContext cho Entity Framework
        public Customer()
        {
            InitializeComponent();
            context = new LaundryContextDB();  // Khởi tạo DbContext
        }

        public void LoadCustomerData(List<tbCustomer> customers)
        {
            dgvCustomer.Rows.Clear();  // Xóa dữ liệu cũ trong DataGridView
            int i = 0;

            foreach (var customer in customers)
            {
                int index = dgvCustomer.Rows.Add();
                dgvCustomer.Rows[index].Cells[0].Value = ++i;
                dgvCustomer.Rows[index].Cells[1].Value = customer.id;
                dgvCustomer.Rows[index].Cells[2].Value = customer.name;
                dgvCustomer.Rows[index].Cells[3].Value = customer.phone;
                dgvCustomer.Rows[index].Cells[4].Value = customer.no;
                dgvCustomer.Rows[index].Cells[5].Value = customer.model;
                dgvCustomer.Rows[index].Cells[6].Value = customer.tbType != null ? customer.tbType.name : "NULL";  // Kiểm tra null
                dgvCustomer.Rows[index].Cells[7].Value = customer.address;
                dgvCustomer.Rows[index].Cells[8].Value = customer.points;
            }
        }


        
        public int GetTypeIdByName(string TypeName)
        {
            var Type = context.tbTypes.FirstOrDefault(t => t.name == TypeName);
            return Type != null ? Type.id : 0;
        }

        private void dgvCustomer_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colName = dgvCustomer.Columns[e.ColumnIndex].Name;

                if (colName == "Edit")
                {
                    CustomerModule module = new CustomerModule(this);
                    module.lblCid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                    module.txtName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                    module.txtPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                    module.txtNo.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();
                    module.txtModel.Text = dgvCustomer.Rows[e.RowIndex].Cells[5].Value.ToString();
                    module.vid = GetTypeIdByName(dgvCustomer.Rows[e.RowIndex].Cells[6].Value.ToString());
                    module.txtAddress.Text = dgvCustomer.Rows[e.RowIndex].Cells[7].Value.ToString();
                    module.udPoints.Value = int.Parse(dgvCustomer.Rows[e.RowIndex].Cells[8].Value.ToString());

                    module.btnSave.Enabled = false;  // Vô hiệu hóa nút Lưu khi đang chỉnh sửa
                    module.udPoints.Enabled = true;
                    module.ShowDialog();
                }
                else if (colName == "Delete")
                {
                    try
                    {
                        if (MessageBox.Show("Bạn có chắc chắn muốn xoá dữ liệu dòng này không?",
                            "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int id = int.Parse(dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString());
                            var customer = context.tbCustomers.Find(id);

                            if(customer.points > 0) { MessageBox.Show("Không thể xoá vì ràng buộc dữ liệu"); return; }

                            if (customer != null)
                            {
                                context.tbCustomers.Remove(customer);  // Xóa khách hàng
                                context.SaveChanges();  // Lưu thay đổi vào CSDL

                                MessageBox.Show("Khách hàng đã được xoá thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadCustomerData(context.tbCustomers.ToList());
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy khách hàng!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            string search = txtSearch.Text.ToLower();
            var filteredList = context.tbCustomers
                .Include(c => c.tbType)  // Nạp dữ liệu liên quan
                .Where(c => (c.id + "" + c.name + " " + c.no + " " + c.model + " " + c.address)
                .ToLower().Contains(search))
                .ToList();

            LoadCustomerData(filteredList);
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

            CustomerModule module = new CustomerModule(this);  // Mở form thêm mới khách hàng
            module.btnUpdate.Enabled = false;  // Vô hiệu hóa nút Cập nhật khi thêm mới
            module.ShowDialog();  // Hiển thị form
        }

        private void Customer_Load_1(object sender, EventArgs e)
        {
            try
            {
                List<tbCustomer> customers = context.tbCustomers
                    .Include(c => c.tbType)  // Nạp dữ liệu loại (tbType)
                    .ToList();

                LoadCustomerData(customers);  // Đổ dữ liệu vào DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


