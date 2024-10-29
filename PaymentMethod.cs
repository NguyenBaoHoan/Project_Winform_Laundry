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
    public partial class PaymentMethod : Form
    {
        private readonly Cash cashForm;

        public PaymentMethod(Cash cash)
        {
            InitializeComponent();
            cashForm = cash;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Hiển thị form thanh toán bằng tiền mặt
            SettlePayment settlePayment = new SettlePayment(cashForm);
            settlePayment.txtSale.Text = cashForm.lblTotal.Text;
            settlePayment.ShowDialog();
            this.Close(); // Đóng form PaymentMethod sau khi hiển thị SettlePayment
        }
        private void btnCard_Click(object sender, EventArgs e)
        {
            // Thêm logic thanh toán qua thẻ ở đây
            MessageBox.Show("Thanh toán bằng thẻ chưa được hỗ trợ.");
        }
    }
}
