using Project1_Laundry.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Project1_Laundry
{
    public partial class ServiceModule : Form
    {
        private readonly LaundryContextDB context;
        private readonly Service serviceForm;

        public ServiceModule(Service ser)
        {
            InitializeComponent();
            context = new LaundryContextDB();
            serviceForm = ser;  // Lưu tham chiếu tới form Service
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPrice.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ tên và giá dịch vụ!", "Warning");
                    return;
                }

                if (MessageBox.Show("Bạn có muốn thêm dịch vụ này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var service = new tbService()
                    {
                        name = txtName.Text,
                        price = txtPrice.Text
                    };

                    context.tbServices.Add(service);
                    context.SaveChanges();

                    MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    serviceForm.LoadServiceData(context.tbServices.ToList());  // Gọi phương thức từ form Service
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        public void Clear()
        {
            txtName.Clear();
            txtPrice.Clear();

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        
        // Không cho nhập số ở text price
       
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only allow digit number
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPrice.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ tên và giá dịch vụ!", "Warning");
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn cập nhật dịch vụ này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (int.TryParse(lblId.Text, out int serviceId))
                    {
                        var service = context.tbServices.Find(serviceId);
                        if (service != null)
                        {
                            service.name = txtName.Text;
                            service.price = txtPrice.Text;

                            context.SaveChanges();

                            MessageBox.Show("Cập nhật dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            serviceForm.LoadServiceData(context.tbServices.ToList());  // Gọi phương thức từ form Service
                            Clear();
                            this.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dịch vụ cần chỉnh sửa!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("ID dịch vụ không hợp lệ!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
