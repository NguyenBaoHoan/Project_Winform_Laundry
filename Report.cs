using System.Globalization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project1_Laundry.Models;
using System.Data.SqlClient;
namespace Project1_Laundry
{
    public partial class Report : Form
    {


        private readonly LaundryContextDB context;

        public Report()
        {
            InitializeComponent();
            context = new LaundryContextDB();
        }



        #region topService
        private void dtFromTopSelling_ValueChanged(object sender, EventArgs e)
        {
            loadTopService();
        }

        private void dtToTopSelling_ValueChanged(object sender, EventArgs e)
        {
            loadTopService();
        }

        private void loadTopService()
        {
            try
            {
                var fromDate = dtFromTopSelling.Value.Date;
                var toDate = dtToTopSelling.Value.Date;

                CultureInfo vietnamCulture = new CultureInfo("vi-VN");

                var topSelling = context.tbCashes
                    .Where(c => c.date >= fromDate && c.date <= toDate && c.status == "Sold")
                    .GroupBy(c => new { c.tbService.name }) // Tạo nhóm theo dịch vụ
                    .ToList()
                    .Select(g => new
                    {
                        ServiceName = g.Key.name, // Key là thuộc tính đã gom nhóm
                        Quantity = g.Count(),
                        Total = g.Sum(c => decimal.Parse(c.price) * 1000) // Nhân với 1000 cần thiết để xử lý số tiền VND
                    })
                    .OrderByDescending(g => g.Quantity)
                    .Take(10)
                    .ToList();

                dgvTopSelling.Rows.Clear();
                int i = 0;

                foreach (var item in topSelling)
                {
                    i++;
                    dgvTopSelling.Rows.Add(i, item.ServiceName, item.Quantity, item.Total.ToString("N0", vietnamCulture));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        #endregion topService
        #region Revenus
        private void dtFromRevenud_ValueChanged(object sender, EventArgs e)
        {
            loadRevenus();
        }

        private void dtToRevenus_ValueChanged(object sender, EventArgs e)
        {
            loadRevenus();
        }

        public void loadRevenus()
        {
            try
            {
                var fromDate = dtFromRevenud.Value.Date;
                var toDate = dtToRevenus.Value.Date;

                var revenus = context.tbCashes
                    .Where(c => c.date >= fromDate && c.date <= toDate && c.status == "Sold")
                    .GroupBy(c => c.date.Value)
                    .ToList()
                    .Select(g => new
                    {
                        Date = g.Key,
                        Total = g.Sum(c => Convert.ToDouble(c.price) * 1000) // * 1000 thành việt VND
                    })
                    .ToList();

                dgvRevenus.Rows.Clear();
                double total = 0;
                int i = 0;

                CultureInfo vietnamCulture = new CultureInfo("vi-VN");

                foreach (var item in revenus)
                {
                    i++;
                    dgvRevenus.Rows.Add(i, item.Date.ToString("dd/MM/yyyy"), item.Total.ToString("N0", vietnamCulture));
                    total += item.Total;
                }

                lblRevenus.Text = total.ToString("N0", vietnamCulture);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        #endregion Revenus
        #region LoadChiPhi
        private void dtFromChiPhi_ValueChanged(object sender, EventArgs e)
        {
            loadCostofGood();
        }

        private void dtToChiPhi_ValueChanged(object sender, EventArgs e)
        {
            loadCostofGood();
        }


        public void loadCostofGood()
        {
            try
            {
                var fromDate = dtFromChiPhi.Value.Date;
                var toDate = dtToChiPhi.Value.Date;

                var costOfGoods = context.tbCostofGoods
                    .Where(c => c.date >= fromDate && c.date <= toDate)
                    .ToList()
                    .Select(c => new
                    {
                        c.costname,
                        Cost = c.cost,
                        c.date
                    })
                    .ToList();

                dgvCostofGood.Rows.Clear();
                double total = 0;
                int i = 0;

                foreach (var item in costOfGoods)
                {
                    i++;
                    dgvCostofGood.Rows.Add(i, item.costname, item.Cost, item.date.Value.ToString("dd/MM/yyyy"));

                    // Loại bỏ dấu chấm hoặc dấu phẩy và chuyển đổi thành double
                    string cleanedCost = item.Cost.Replace(".", "").Replace(",", "");
                    if (double.TryParse(cleanedCost, out double costValue))
                    {
                        total += costValue;
                    }
                    else
                    {
                        MessageBox.Show($"Không thể chuyển đổi giá trị: {item.Cost}", "Lỗi định dạng");
                    }
                }

                lblCoG.Text = total.ToString("#,##0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        #endregion LoadChiPhi



        #region GrossProfit

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            LoadGrossProfit();
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            LoadGrossProfit();
        }



        public void LoadGrossProfit()
        {

            // Retrieve and parse Revenues data
            var revenues = context.tbCashes
                .Where(cash => cash.date >= dtFrom.Value && cash.date <= dtTo.Value)
                .ToList()
                .Select(cash =>
                {
                    double value;
                    return double.TryParse(cash.price, out value) ? value : 0;
                })
                .Sum();

            // Retrieve and parse Cost of Goods data
            var costs = context.tbCostofGoods
                .Where(cost => cost.date >= dtFrom.Value && cost.date <= dtTo.Value)
                .ToList()
                .Select(cost =>
                {
                    double value;
                    return double.TryParse(cost.cost, out value) ? value : 0;
                })
                .Sum();

            // Update Text Boxes
            txtRevenus.Text = revenues.ToString("#,##0.00");
            txtCoG.Text = costs.ToString("#,##0.00");

            // Calculate and display Gross Profit
            double grossProfit = revenues - costs;
            txtProfit.Text = grossProfit.ToString("#,##0.00");

            // Change color based on Gross Profit value
            txtProfit.ForeColor = grossProfit < 0 ? Color.Red : Color.Green;

        }

        #endregion GrossProfit
    }
}
