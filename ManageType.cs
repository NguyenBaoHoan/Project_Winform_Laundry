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
    public partial class ManageType : Form
    {
        private readonly LaundryContextDB context;
        Setting setting;
        public ManageType(Setting sett)
        {
            InitializeComponent();
            context = new LaundryContextDB();
            setting = sett;
            cbClass.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Yêu cầu nhập tên của chất liệu đồ!", "Warning");
                    return;
                }

                if (MessageBox.Show("Bạn có muốn nhập loại đồ này không?", "Đăng ký", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    var type = new tbType()
                    {
                        name = txtName.Text,
                        _class = cbClass.Text,

                    };
                    context.tbTypes.Add(type);
                    context.SaveChanges();

                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Clear();
                    setting.DoDuLieuVaoType(context.tbTypes.ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Clear();
        }

        public void Clear()
        {
            txtName.Clear();
            cbClass.SelectedIndex = 0;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Bạn vui lòng nhập tên!", "Warning");
                    return;
                }


                if (MessageBox.Show("Bạn có chắn rằng muốn chỉnh sửa dòng này không?", "Chỉnh sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int ma;
                    if (!int.TryParse(lblid.Text, out ma))
                    {
                        MessageBox.Show("ID không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var search = context.tbTypes.Find(ma);
                    if (search != null)
                    {
                        search.name = txtName.Text;
                        search._class = cbClass.Text;

                        context.SaveChanges();

                        MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        setting.DoDuLieuVaoType(context.tbTypes.ToList());

                        Clear();//to clear data field, after data inserted into the database                                           
                        this.Dispose();

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
