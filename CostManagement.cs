using Project1_Laundry.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Project1_Laundry
{

    public partial class CostManagement : Form
    {
        private readonly LaundryContextDB context;
        Setting setting;
        bool check = false;
        string title = "Laundry Shop";
        public CostManagement(Setting sett)
        {
            InitializeComponent();
            setting = sett;
            context = new LaundryContextDB();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtCost.Clear();
            txtCostName.Clear();
            dtCoG.Value = DateTime.Now;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        public void checkField()
        {
            if (txtCostName.Text == "" || txtCost.Text == "")
            {
                MessageBox.Show("Bạn phải nhập dữ liệu!", "Warning");
                return; // return to the data field and form
            }
            check = true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Bạn chắc rằng muốn thêm dữ liệu?",
                        "Chi Phí", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        var newCost = new tbCostofGood
                        {
                            costname = txtCostName.Text,
                            cost = txtCost.Text,
                            date = dtCoG.Value
                        };

                        context.tbCostofGoods.Add(newCost);
                        context.SaveChanges(); // Lưu thay đổi vào CSDL

                        MessageBox.Show("Bạn đã thêm chi phí hoạt động thành công!", title);
                        Clear(); // Xóa dữ liệu nhập sau khi lưu
                        setting.DoDuLieuVaoDgvCost(context.tbCostofGoods.ToList()); // Tải lại danh sách
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.InnerException?.Message ?? ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                checkField();
                if (check)
                {
                    if(MessageBox.Show("Bạn có muốn chỉnh sửa loại chi phí này không", "Chỉnh sửa chi phí", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int id = int.Parse(lblid.Text);
                        var exist = context.tbCostofGoods.Find(id);
                        if(exist != null)
                        {
                            exist.costname = txtCostName.Text;
                            exist.cost = txtCost.Text;
                            exist.date = dtCoG.Value;

                            context.SaveChanges(); // save to database

                            MessageBox.Show("Bạn đã chỉnh sửa thành công!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();
                            this.Dispose();
                            setting.DoDuLieuVaoDgvCost(context.tbCostofGoods.ToList());
                        }
                        else
                        {
                            MessageBox.Show("Dòng không tồn tại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", title);
            }

        }
        // không có phép nhập ký tự chữ cái
        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only allow digit number
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
