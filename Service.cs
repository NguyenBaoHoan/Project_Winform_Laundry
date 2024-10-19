using Project1_Laundry.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1_Laundry
{
    public partial class Service : Form
    {
        private readonly LaundryContextDB context;
        public Service()
        {
            InitializeComponent();
            context = new LaundryContextDB();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ServiceModule module = new ServiceModule(this);
            module.btnUpdate.Enabled = false;
            module.ShowDialog();
        }

        private void Service_Load(object sender, EventArgs e)
        {
            try
            {
                List<tbService> tbServices = context.tbServices.ToList();
                LoadServiceData(tbServices);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadServiceData(List<tbService> tbServices)
        {

            dgvService.Rows.Clear();
            int i = 0;

            foreach (var item in tbServices)
            {
                int index = dgvService.Rows.Add();
                dgvService.Rows[index].Cells[0].Value = ++i;
                dgvService.Rows[index].Cells[1].Value = item.id;
                dgvService.Rows[index].Cells[2].Value = item.name;
                dgvService.Rows[index].Cells[3].Value = item.price;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.ToLower();
            var filteredList = context.tbServices
                .Where(service => (service.id.ToString() + " " + service.name + " " + service.price)
                .ToLower().Contains(search))
                .ToList();

            LoadServiceData(filteredList);
        }

        private void dgvService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colName = dgvService.Columns[e.ColumnIndex].Name;

                if (colName == "Edit")
                {
                    ServiceModule module = new ServiceModule(this);
                    module.lblId.Text = dgvService.Rows[e.RowIndex].Cells[1].Value.ToString();
                    module.txtName.Text = dgvService.Rows[e.RowIndex].Cells[2].Value.ToString();
                    module.txtPrice.Text = dgvService.Rows[e.RowIndex].Cells[3].Value.ToString();

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
                            int id = int.Parse(dgvService.Rows[e.RowIndex].Cells[1].Value.ToString());

                            var service = context.tbServices.Find(id);
                            if (service != null)
                            {
                                context.tbServices.Remove(service);
                                context.SaveChanges();

                                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadServiceData(context.tbServices.ToList());
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
    }
}

