using Project1_Laundry.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Project1_Laundry
{
    public partial class Setting : Form
    {
        bool hasdetail = false;
        public string title = "Laundry Shop";
        private readonly LaundryContextDB context;

        public Setting()
        {
            InitializeComponent();
            context = new LaundryContextDB();
        }

        #region Loại Chất Liệu

        private void btnAddVT_Click(object sender, EventArgs e)
        {
            ManageType module = new ManageType(this);
            module.btnUpdate.Enabled = false;
            module.ShowDialog();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            try
            {
                List<tbType> tbTypes = context.tbTypes.ToList();
                DoDuLieuVaoType(tbTypes);

                List<tbCostofGood> tbCostofGoods = context.tbCostofGoods.ToList();
                DoDuLieuVaoDgvCost(tbCostofGoods);

                LoadCompany();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DoDuLieuVaoType(List<tbType> tbTypes)
        {
            dgvType.Rows.Clear();
            int i = 0;

            foreach (var item in tbTypes)
            {
                int index = dgvType.Rows.Add();
                dgvType.Rows[index].Cells[0].Value = ++i;
                dgvType.Rows[index].Cells[1].Value = item.id;
                dgvType.Rows[index].Cells[2].Value = item.name;
                dgvType.Rows[index].Cells[3].Value = item._class;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.ToLower();
            var filteredList = context.tbTypes
                .Where(type => (type.id.ToString() + " " + type.name).ToLower().Contains(search))
                .ToList();

            DoDuLieuVaoType(filteredList);
        }

        private void dgvType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colName = dgvType.Columns[e.ColumnIndex].Name;

                if (colName == "Edit")
                {
                    ManageType module = new ManageType(this);
                    module.lblid.Text = dgvType.Rows[e.RowIndex].Cells[1].Value.ToString();
                    module.txtName.Text = dgvType.Rows[e.RowIndex].Cells[2].Value.ToString();
                    module.cbClass.Text = dgvType.Rows[e.RowIndex].Cells[3].Value.ToString();

                    module.btnSave.Enabled = false;
                    module.btnUpdate.Enabled = true;
                    module.ShowDialog();
                }
                else if (colName == "Delete")
                {
                    try
                    {
                        if (MessageBox.Show("Bạn có muốn xoá dòng này không?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int id = int.Parse(dgvType.Rows[e.RowIndex].Cells[1].Value.ToString());

                            var type = context.tbTypes.Find(id);
                            if (type != null)
                            {
                                context.tbTypes.Remove(type);
                                context.SaveChanges();

                                MessageBox.Show("Bạn đã xoá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DoDuLieuVaoType(context.tbTypes.ToList());
                            }
                            else
                            {
                                MessageBox.Show("Dòng không tồn tại", "Error");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Có lỗi trong quá trình xoá: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region Vốn

        public void LoadCompany()
        {
            try
            {
                var company = context.tbCompanies.FirstOrDefault();

                if (company != null)
                {
                    hasdetail = true;
                    txtComName.Text = company.name;
                    txtComAddress.Text = company.address;
                }
                else
                {
                    txtComName.Clear();
                    txtComAddress.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có muốn lưu thông tin?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    var company = context.tbCompanies.FirstOrDefault();
                    if (company != null)
                    {
                        company.name = txtComName.Text;
                        company.address = txtComAddress.Text;
                    }
                    else
                    {
                        company = new tbCompany
                        {
                            name = txtComName.Text,
                            address = txtComAddress.Text
                        };
                        context.tbCompanies.Add(company);
                    }

                    context.SaveChanges();
                    MessageBox.Show("Chi tiết công ty đã thay đổi thành công!", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtComName.Clear();
            txtComAddress.Clear();
        }

        #endregion

        #region Chi phí kinh doanh

        private void btnAddCoG_Click(object sender, EventArgs e)
        {
            CostManagement module = new CostManagement(this);
            module.btnUpdate.Enabled = false;
            module.ShowDialog();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearchChiPhi.Text.ToLower();
            var filteredList = context.tbCostofGoods
                .Where(cost => (cost.id.ToString() + " " + cost.costname).ToLower().Contains(search))
                .ToList();

            DoDuLieuVaoDgvCost(filteredList);
        }

        public void DoDuLieuVaoDgvCost(List<tbCostofGood> tbCostofGoods)
        {
            dgvCostofGoodSold.Rows.Clear();
            int i = 0;

            foreach (var item in tbCostofGoods)
            {
                int index = dgvCostofGoodSold.Rows.Add();

                dgvCostofGoodSold.Rows[index].Cells[0].Value = ++i;
                dgvCostofGoodSold.Rows[index].Cells[1].Value = item.id;
                dgvCostofGoodSold.Rows[index].Cells[2].Value = item.costname;
                dgvCostofGoodSold.Rows[index].Cells[3].Value = item.cost;
                dgvCostofGoodSold.Rows[index].Cells[4].Value = item.date;
            }
        }

        private void dgvCostofGoodSold_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colName = dgvCostofGoodSold.Columns[e.ColumnIndex].Name;

                if (colName == "EditCoG")
                {
                    // Khởi tạo form quản lý chi phí (CostManagement)
                    CostManagement module = new CostManagement(this);
                    module.lblid.Text = dgvCostofGoodSold.Rows[e.RowIndex].Cells[1].Value.ToString();
                    module.txtCostName.Text = dgvCostofGoodSold.Rows[e.RowIndex].Cells[2].Value.ToString();
                    module.txtCost.Text = dgvCostofGoodSold.Rows[e.RowIndex].Cells[3].Value.ToString();
                    module.dtpCoG.Value = Convert.ToDateTime(dgvCostofGoodSold.Rows[e.RowIndex].Cells[4].Value);

                    module.btnSave.Enabled = false;
                    module.btnUpdate.Enabled = true;
                    module.ShowDialog();
                }
                else if (colName == "DeleteCoG")
                {
                    try
                    {
                        if (MessageBox.Show("Bạn có muốn xoá dòng này không?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int id = int.Parse(dgvCostofGoodSold.Rows[e.RowIndex].Cells[1].Value.ToString());

                            var cost = context.tbCostofGoods.Find(id);
                            if (cost != null)
                            {
                                context.tbCostofGoods.Remove(cost);
                                context.SaveChanges();

                                MessageBox.Show("Bạn đã xoá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DoDuLieuVaoDgvCost(context.tbCostofGoods.ToList());
                            }
                            else
                            {
                                MessageBox.Show("Dòng không tồn tại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Có lỗi trong quá trình xoá: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        
    }
}
