using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using Project1_Laundry.Models;

namespace Project1_Laundry
{
    public partial class Employee : Form
    {
        private LaundryContextDB _context = new LaundryContextDB(); // Tạo instance của DbContext
        string title = "Laundry Management System"; // Tên hệ thống quản lý

        public Employee()
        {
            InitializeComponent();
            InitializeDataGridView();
            loadEmployee(); // Gọi hàm loadEmployee() khi khởi tạo form

        }
        /// <summary>
        /// tieng anh
        /// </summary>
        private void InitializeDataGridView()
        {
            // Đặt AutoSizeRowsMode là None để không tự động thay đổi chiều cao hàng
            dgvEmployee.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // Đặt chiều cao cố định cho hàng
            dgvEmployee.RowTemplate.Height = 100;

            // Đặt ImageLayout cho cột hình ảnh
            ((DataGridViewImageColumn)dgvEmployee.Columns["ImageColumn"]).ImageLayout = DataGridViewImageCellLayout.Zoom;

            // Đặt kích thước cố định cho cột hình ảnh
            dgvEmployee.Columns["ImageColumn"].Width = 100;
        }

        /// <summary>
        /// tien anh
        /// </summary>
        // Hàm load dữ liệu nhân viên từ cơ sở dữ liệu
        public void loadEmployee()
        {
            try
            {
                int i = 0; // Đếm số lượng nhân viên
                dgvEmployee.Rows.Clear(); // Xóa các hàng cũ trong DataGridView

                // Lấy danh sách nhân viên từ DbContext và tìm kiếm theo từ khóa
                var employees = _context.tbEmployees
                    .Where(e => (e.id.ToString() + e.name + e.address + e.role).Contains(txtSearch.Text))
                    .ToList();

                // Duyệt qua từng bản ghi và thêm vào DataGridView
                foreach (var emp in employees)
                {
                    i++;
                    Image empImage = null;
                    if (emp.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream(emp.Image))
                        {
                            empImage = Image.FromStream(ms);
                        }
                    }
                    dgvEmployee.Rows.Add(i, emp.id, emp.name, emp.phone, emp.Image,emp.address,
                        emp.dob?.ToShortDateString(), emp.gender, emp.role, emp.salary, emp.password);
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có
                MessageBox.Show(ex.Message, title);
            }
        }

        // Sự kiện click vào nút Thêm mới nhân viên
        private void btnAdd_Click(object sender, EventArgs e)
        {
            EmployeeModule module = new EmployeeModule(this); // Mở form EmployeeModule
            module.btnUpdate.Enabled = false; // Chỉ cho phép thêm mới
            module.ShowDialog(); // Hiển thị form
        }
        /// <summary>
        /// tien anh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // Sự kiện khi click vào ô trong DataGridView
        private void dgvEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvEmployee.Columns[e.ColumnIndex].Name;

            if (colName == "Edit") // Khi click vào nút Sửa
            {
                EmployeeModule module = new EmployeeModule(this);
                module.lblEid.Text = dgvEmployee.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtName.Text = dgvEmployee.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtPhone.Text = dgvEmployee.Rows[e.RowIndex].Cells[3].Value.ToString();
                module.txtAddress.Text = dgvEmployee.Rows[e.RowIndex].Cells[5].Value.ToString();

                // Lấy và hiển thị hình ảnh từ DataGridView
                var empId = int.Parse(dgvEmployee.Rows[e.RowIndex].Cells[1].Value.ToString());
                var employee = _context.tbEmployees.Find(empId);
                if (employee != null && employee.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream(employee.Image))
                    {
                        module.ptbEmployee.Image = Image.FromStream(ms);
                    }
                }
                module.dtDob.Text = dgvEmployee.Rows[e.RowIndex].Cells[6].Value.ToString();

                module.rdMale.Checked = dgvEmployee.Rows[e.RowIndex].Cells[7].Value.ToString() == "Male";
                module.cbRole.Text = dgvEmployee.Rows[e.RowIndex].Cells[8].Value.ToString();
                module.txtSalary.Text = dgvEmployee.Rows[e.RowIndex].Cells[9].Value.ToString();
                module.txtPassword.Text = dgvEmployee.Rows[e.RowIndex].Cells[10].Value.ToString();

                

                module.btnSave.Enabled = false; // Chỉ cho phép cập nhật
                module.ShowDialog(); // Hiển thị form
            }
            else if (colName == "Delete") // Khi click vào nút Xóa
            {
                try
                {
                    if (MessageBox.Show("Bạn có chắc muốn xoá dòng này không?", "Xoá",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int id = int.Parse(dgvEmployee.Rows[e.RowIndex].Cells[1].Value.ToString());
                        var employee = _context.tbEmployees.Find(id); // Tìm nhân viên theo id

                        if (employee != null)
                        {
                            _context.tbEmployees.Remove(employee); // Xóa nhân viên
                            _context.SaveChanges(); // Lưu thay đổi vào database
                            MessageBox.Show("Nhân viên đã được xoá thành công!", title,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadEmployee(); // Tải lại danh sách nhân viên
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); // Hiển thị thông báo lỗi nếu có
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadEmployee();
        }
    }
}
