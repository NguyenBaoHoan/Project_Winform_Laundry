using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1_Laundry
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            openChildForm(new Home());
        }
        internal void loadGrossProfit()
        {
            throw new NotImplementedException();
        }



        // Khai báo biến Form đang hoạt động, khởi tạo bằng null
        private Form activeForm = null;

        // Phương thức mở Form con (childForm) trong Form chính
        public void openChildForm(Form childForm)
        {
            // Nếu đã có Form con đang mở, thì đóng nó lại
            if (activeForm != null)
                activeForm.Close();

            // Gán Form con mới cho biến activeForm để theo dõi
            activeForm = childForm;

            // Thiết lập Form con không phải là cửa sổ cấp cao nhất
            childForm.TopLevel = false;

            // Loại bỏ viền và tiêu đề của Form con để nó hòa vào giao diện
            childForm.FormBorderStyle = FormBorderStyle.None;

            // Form con sẽ chiếm toàn bộ không gian của panelChild
            childForm.Dock = DockStyle.Fill;

            // Thêm Form con vào danh sách điều khiển của panelChild
            panelChild.Controls.Add(childForm);

            // Gán Form con cho thuộc tính Tag của panel để lưu thông tin cần thiết
            panelChild.Tag = childForm;

            // Đưa Form con lên trên cùng để đảm bảo nó được hiển thị đầy đủ
            childForm.BringToFront();

            // Hiển thị Form con
            childForm.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            openChildForm(new Home());
        }

        private void btnEmployer_Click(object sender, EventArgs e)
        {
            openChildForm(new Employee());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new Customer());
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            openChildForm(new Service());
        }

        private void btncash_Click(object sender, EventArgs e)
        {
            openChildForm(new Cash(this));
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

        }
    }
}
