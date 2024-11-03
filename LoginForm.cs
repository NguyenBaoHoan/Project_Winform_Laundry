using Project1_Laundry.Models;
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
    public partial class LoginForm : Form
    {
        private readonly LaundryContextDB context;
        public LoginForm()
        {
            InitializeComponent();
            context = new LaundryContextDB();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtPassword.Clear();
            txtName.Focus();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtPassword.UseSystemPasswordChar = false;
            else
                txtPassword.UseSystemPasswordChar = true;
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            try
            {


                // Tìm nhân viên với tên và mật khẩu được nhập
                var employer = context.tbEmployees
                    .FirstOrDefault(emp => emp.name == txtName.Text && emp.password == txtPassword.Text);

                if (employer != null)
                {
                    MessageBox.Show("Chào mừng " + employer.name + " ", "Bạn đã đăng nhập thành công!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Kiểm tra quyền admin từ cột isAdmin
                    bool isAdmin = employer.isAdmin.HasValue && employer.isAdmin.Value;

                    this.Hide();
                    MainForm main = new MainForm(isAdmin);  // Truyền quyền admin vào MainForm
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu của bạn sai!", "ERROR",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
