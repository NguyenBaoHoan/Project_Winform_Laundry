using Project1_Laundry.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1_Laundry
{
    public partial class CashService : Form
    {
        private readonly LaundryContextDB context;
        private readonly Cash cash;
        
        public CashService(Cash cashForm)
        {
            InitializeComponent();
            context = new LaundryContextDB();
            cash = cashForm;
            LoadServiceData();
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            LoadServiceData();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow dr in dgvService.Rows)
                {
                    bool chkbox = dr.Cells["Select"].Value != null && Convert.ToBoolean(dr.Cells["Select"].Value);
                    if (chkbox)
                    {
                        var sid = Convert.ToInt32(dr.Cells[1].Value);

                        var price = dr.Cells[3].Value?.ToString();// kiểm tra null nếu null thì trả về null không gọi toString()

                        // Kiểm tra xem bản ghi đã tồn tại chưa
                        var existingCash = context.tbCashes
                            .FirstOrDefault(c => c.sid == sid && c.transno == cash.lblTransno.Text);

                        if (existingCash == null)
                        {
                            // Tạo đối tượng tbCash mới
                            var newCash = new tbCash 
                            {
                                transno = cash.lblTransno.Text,
                                cid = cash.customerId,
                                sid = sid,
                                idType = cash.TypeId,
                                price = price,
                                date = DateTime.Now,
                                status = "Pending"  // Thêm status mặc định
                            };

                            // Thêm đối tượng mới vào DbSet
                            context.tbCashes.Add(newCash);
                        }

                    }

                    // Lưu các thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();
                }

                // Kích hoạt nút Cash
                cash.btnCash.Enabled = true;

                // Đóng form và làm mới giao diện
                this.Dispose();
                cash.panelCash.Height = 1;
                cash.loadCash();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error CashService: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadServiceData()
        {
            try
            {
                var query = context.tbServices.AsQueryable();

                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    var searchText = txtSearch.Text.ToLower();
                    query = query.Where(s => s.name.ToLower().Contains(searchText));
                }

                var serviceList = query.ToList();
                dgvService.Rows.Clear();

                int i = 0;
                foreach (var service in serviceList)
                {
                    dgvService.Rows.Add(++i, service.id, service.name, service.price);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading services: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
