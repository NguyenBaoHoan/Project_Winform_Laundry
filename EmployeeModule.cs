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
using Project1_Laundry.Models;
using System.Xml.Linq;
using TheArtOfDevHtmlRenderer.Adapters;
using System.IO;

namespace Project1_Laundry
{
    public partial class EmployeeModule : Form
    {
        private readonly LaundryContextDB context;
        bool check = false;
        string title = "Laundry Shop";
        Employee employee;

        public EmployeeModule(Employee employeeForm)
        {
            InitializeComponent();
            context = new LaundryContextDB(); // Khởi tạo DbContext
            employee = employeeForm; // Tham chiếu đến form Employer
            cbRole.SelectedIndex = 0;// Mặc định chọn Worker
        }
        /// <summary>
        /// tien anh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkField(); // Kiểm tra dữ liệu
                if (check)
                {
                    if (MessageBox.Show("Bạn có chắc chắn rằng bạn muốn đăng kí nhân viên này?",
                        "Employer Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var newEmployer = new tbEmployee
                        {
                            name = txtName.Text,
                            phone = int.Parse(txtPhone.Text),
                            address = txtAddress.Text,
                            dob = dtDob.Value,
                            gender = rdMale.Checked ? "Nam" : "Nữ",
                            role = cbRole.Text,
                            salary = txtSalary.Text,
                            password = txtPassword.Text,
                            Image = ConvertImageToBytes(ptbEmployee.Image),// Lưu hình ảnh
                            isAdmin = cbRole.SelectedItem.ToString() == "Quản Lý" // Gán isAdmin dựa trên vai trò
                        };

                        context.tbEmployees.Add(newEmployer); // Thêm vào DbSet
                        context.SaveChanges(); // Lưu vào CSDL

                        MessageBox.Show("Nhân viên đã được đăng kí thành công!", title);

                        Clear(); // Xóa các trường dữ liệu
                        employee.loadEmployee(); // Tải lại danh sách employer
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// tien anh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                checkField(); // Kiểm tra dữ liệu
                if (check)
                {
                    if (MessageBox.Show("Bạn có chắc rằng muốn thay đổi dòng dữ liệu này không?",
                        "Employer Editing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (int.TryParse(lblEid.Text, out int employerId))
                        {
                            var employer = context.tbEmployees.Find(employerId);
                            if (employer != null)
                            {
                                employer.name = txtName.Text;
                                employer.phone = int.Parse(txtPhone.Text);
                                employer.address = txtAddress.Text;
                                employer.dob = dtDob.Value;
                                employer.gender = rdMale.Checked ? "Nam" : "Nữ";
                                employer.role = cbRole.Text;
                                employer.salary = txtSalary.Text;
                                employer.password = txtPassword.Text;
                                employer.Image = ConvertImageToBytes(ptbEmployee.Image); // Cập nhật hình ảnh
                                employer.isAdmin = cbRole.SelectedIndex == 1; // Cập nhật isAdmin

                                context.SaveChanges(); // Lưu thay đổi

                                MessageBox.Show("Nhân viên đã được cập nhật thành công!", title);

                                Clear(); // Xóa các trường dữ liệu
                                employee.loadEmployee(); // Tải lại danh sách employer
                                this.Dispose(); // Đóng form
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy nhân viên!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear(); // Xóa dữ liệu và đặt lại form
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
        }

        public void Clear()
        {
            txtName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtSalary.Clear();
            txtPassword.Clear();
            dtDob.Value = DateTime.Now;
            cbRole.SelectedIndex = 0;
            ptbEmployee.Image = null; // Xóa hình ảnh hiển thị
        }

        public void checkField()
        {
            if (string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtPhone.Text) ||
                string.IsNullOrEmpty(txtAddress.Text) ||
                string.IsNullOrEmpty(txtSalary.Text))
            {
                MessageBox.Show("Bạn vui lòng nhập đầy đủ thông tin!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                check = false;
                return;
            }

            if (checkAge(dtDob.Value) < 18)
            {
                MessageBox.Show("Nhân viên dưới 18 tuổi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                check = false;
                return;
            }

            check = true;
        }

        private static int checkAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age--; // Trừ 1 nếu chưa đến ngày sinh nhật trong năm nay
            return age;
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và dấu chấm
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// tien anh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        

        public byte[] ConvertImageToBytes(Image img)
        {
            if (img == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private void btnSelectPicture_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ptbEmployee.Image = new Bitmap(openFileDialog.FileName);
            }
        }

    }
}
