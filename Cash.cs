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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Project1_Laundry
{
    public partial class Cash : Form
    {
        private readonly LaundryContextDB context;
        public int customerId = 0, TypeId = 0;
        public string no, model;
        MainForm main;
        public Cash()
        {
            InitializeComponent();
            context = new LaundryContextDB();
            getTransno();
            loadCash();
            //main = MainForm;
        }

        private void Cash_Load(object sender, EventArgs e)
        {
            loadCash();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new CashCutomer(this));
            btnAddService.Enabled = true;
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            openChildForm(new CashService(this));
            btnAddCustomer.Enabled = false;
        }

        //private void btnCash_Click(object sender, EventArgs e)
        //{
        //    SettlePayment module = new SettlePayment(this);
        //    module.txtSale.Text = lblTotal.Text;
        //    module.ShowDialog();
        //    main.loadGrossProfit();
        //}

        private Form activeForm = null;

        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelCash.Height = 200;
            panelCash.Controls.Add(childForm);
            panelCash.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void dgvCash_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            SettlePayment module = new SettlePayment(this);
            module.txtSale.Text = lblTotal.Text;
            module.ShowDialog();
            
        }

        

        // create 1 hàm lấy số thứ tự dựa vào ngày 
        public void getTransno()
        {
            try
            {
                string sdate = DateTime.Now.ToString("ddMMyyyy");

                // Lấy giao dịch mới nhất trong ngày
                var latestTransaction = context.tbCashes
                    .Where(c => c.transno.ToString().StartsWith(sdate))
                    .OrderByDescending(c => c.id)
                    .FirstOrDefault();

                if (latestTransaction != null)
                {
                    int count = int.Parse(latestTransaction.transno.ToString().Substring(8, 4));
                    lblTransno.Text = sdate + (count + 1).ToString("D4");
                }
                else
                {
                    lblTransno.Text = sdate + "1001";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating transaction number: {ex.Message}", "Error");
            }
        }

        public void loadCash()
        {
            try
            {
                int i = 0;
                double total = 0;
                double price = 0;
                dgvCash.Rows.Clear();

                // Lấy danh sách các giao dịch có status 'Pending' và transno khớp
                var cashList = context.tbCashes
                    .Where(c => c.status == "Pending" && c.transno.ToString() == lblTransno.Text)
                    .Select(c => new
                    {
                        c.id,
                        c.transno,
                        CustomerName = c.tbCustomer.name,
                        No = c.tbCustomer.no,
                        Model = c.tbCustomer.model,
                        TypeName = c.tbType.name,
                        Class = c.tbType._class,
                        ServiceName = c.tbService.name,
                        Price = c.price,
                        Date = c.date
                    })
                    .ToList();

                // Thêm dữ liệu vào DataGridView
                foreach (var cash in cashList)
                {
                    i++;
                    price = double.Parse(cash.Class) * double.Parse(cash.Price);

                    dgvCash.Rows.Add(
                        i,
                        cash.id,
                        cash.transno,
                        cash.CustomerName,
                        cash.No,
                        cash.Model,
                        cash.TypeName,
                        cash.Class,
                        cash.ServiceName,
                        price,
                        cash.Date?.ToString("dd/MM/yyyy")
                    );

                    total += price;
                    no = cash.No;
                    model = cash.Model;
                }

                lblTotal.Text = total.ToString("#,##0.000");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cash data: {ex.Message}", "Error");
            }
        }
    }
}
