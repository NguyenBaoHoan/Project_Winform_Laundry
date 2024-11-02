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

        
        

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Hiển thị form thanh toán bằng tiền mặt
            SettlePayment settlePayment = new SettlePayment(cashForm);
            settlePayment.txtSale.Text = cashForm.lblTotal.Text;
            settlePayment.ShowDialog();
            this.Dispose(); // Đóng form PaymentMethod sau khi hiển thị SettlePayment
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            CreateQRMoMo createQRMoMo = new CreateQRMoMo(cashForm);
            createQRMoMo.txtsotien.Text = RemoveDots(cashForm.lblTotal.Text);
            createQRMoMo.ShowDialog();
            this.Dispose();
        }
        private string RemoveDots(string input)
        {
            return input.Replace(".", "");
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
