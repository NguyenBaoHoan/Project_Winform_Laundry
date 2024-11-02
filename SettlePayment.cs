using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project1_Laundry.Models;
namespace Project1_Laundry
{
    public partial class SettlePayment : Form
    {
        private readonly LaundryContextDB context;
        Cash cash;
        string title = "Laundry Shop";
        public SettlePayment(Cash cashForm)
        {
            InitializeComponent();  
            context = new LaundryContextDB();
            cash = cashForm;    
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void btnPoint_Click_1(object sender, EventArgs e)
        {
            txtCash.Text += btnPoint.Text;

        }


        private void btn2_Click_1(object sender, EventArgs e)
        {
            txtCash.Text += btn2.Text;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(txtChange.Text) < 0 || txtCash.Text.Equals(""))
                {
                    MessageBox.Show("Số tiền không đủ để thanh toán, Bạn vui lòng nhập chính xác!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    foreach (DataGridViewRow row in cash.dgvCash.Rows)
                    {
                        int cashId = int.Parse(row.Cells[1].Value.ToString());
                        var cashRecord = context.tbCashes.FirstOrDefault(c => c.id == cashId);
                        if (cashRecord != null)
                        {
                            cashRecord.status = "Sold";
                            cashRecord.price = row.Cells[9].Value.ToString();
                        }
                    }

                    // Cộng điểm cho khách hàng
                    var customer = context.tbCustomers.FirstOrDefault(c => c.id == cash.customerId);
                    if (customer != null)
                    {
                        customer.points += 1;
                    }

                    context.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu

                    //Hiển thị biên lai
                    Receipt module = new Receipt(cash);
                    module.LoadReceipt(txtCash.Text, txtChange.Text);
                    module.ShowDialog();


                    MessageBox.Show("Thanh toán đã lưu thành công!", "Thành Toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cash.loadCash();

                    this.Dispose();
                    cash.btnAddCustomer.Enabled = true;
                    cash.btnAddService.Enabled = false;
                    cash.getTransno();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }

        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double charge = double.Parse(txtCash.Text) - double.Parse(txtSale.Text);
                txtChange.Text = charge.ToString("#,##0.000");
            }
            catch (Exception)
            {
                txtChange.Text = "0.000";
            }
        }

        private void SettlePayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnter.PerformClick();// action click enter button
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Dispose(); // Đóng form hiện tại
            }
        }

        private void btnClean_Click_1(object sender, EventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus(); // dấu nhau quay lại cash
        }

        

        

        private void btn3_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn3.Text;
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn00.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn6.Text;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn5.Text;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn9.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn0.Text;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn1.Text;
        }

        private void txtSale_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
