using Project1_Laundry.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1_Laundry
{
    public partial class CashCutomer : Form
    {
        private readonly LaundryContextDB context;
        private readonly Cash cash;

        public CashCutomer(Cash cashForm)
        {
            InitializeComponent();
            context = new LaundryContextDB();
            cash = cashForm;
            LoadCustomerData();
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            
        }

        private async void LoadCustomerData()
        {
            try
            {
                // Sử dụng AsNoTracking() để không theo dõi các thay đổi - cải thiện hiệu năng
                var query = context.tbCustomers.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    var searchText = txtSearch.Text.ToLower();
                    query = query.Where(c =>
                        (c.name + " " + c.phone + " " + c.address).ToLower().Contains(searchText));
                }

                // Sử dụng async/await để truy vấn dữ liệu không đồng bộ
                var customerList = await query.ToListAsync();

                dgvCustomer.Rows.Clear();

                int i = 0;
                foreach (var customer in customerList)
                {
                    dgvCustomer.Rows.Add(
                        ++i,
                        customer.id,
                        customer.vid ?? 0,  // Xử lý giá trị null cho vid
                        customer.name,
                        customer.phone,
                        customer.no,
                        customer.model,
                        customer.address,
                        customer.points
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }


        private void dgvCustomer_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
                if (colName == "Select")
                {
                    var customerId = dgvCustomer.Rows[e.RowIndex].Cells[1].Value;
                    var typeId = dgvCustomer.Rows[e.RowIndex].Cells[2].Value;

                    if (customerId != null && typeId != null)
                    {
                        cash.customerId = (int)customerId;
                        cash.TypeId = (int)typeId;
                        this.Dispose();
                        cash.panelCash.Height = 1;
                    }
                    else
                    {
                        MessageBox.Show("Customer ID or Type ID is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadCustomerData();
        }
    }
}