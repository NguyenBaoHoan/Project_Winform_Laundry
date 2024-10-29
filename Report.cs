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

                var topSelling = context.tbCashes
                    .Where(c => c.date >= fromDate && c.date <= toDate && c.status == "Sold")
                    .GroupBy(c => new { c.tbService.name })
                    .ToList()
                    .Select(g => new
                    {
                        ServiceName = g.Key.name,
                        Quantity = g.Count(),
                        Total = g.Sum(c => decimal.Parse(c.price))
                    })
                    .OrderByDescending(g => g.Quantity)
                    .Take(10)
                    .ToList();

                dgvTopSelling.Rows.Clear();
                int i = 0;

                foreach (var item in topSelling)
                {
                    i++;
                    dgvTopSelling.Rows.Add(i, item.ServiceName, item.Quantity, item.Total);
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
                using (var db = new LaundryContextDB())
                {
                    var fromDate = dtFromRevenud.Value.Date;
                    var toDate = dtToRevenus.Value.Date;

                    var revenus = db.tbCashes
                        .Where(c => c.date >= fromDate && c.date <= toDate && c.status == "Sold")
                        .GroupBy(c => c.date.Value)
                        .ToList()
                        .Select(g => new
                        {
                            Date = g.Key,
                            Total = g.Sum(c => Convert.ToDouble(c.price))
                        })
                        .ToList();

                    dgvRevenus.Rows.Clear();
                    double total = 0;
                    int i = 0;

                    foreach (var item in revenus)
                    {
                        i++;
                        dgvRevenus.Rows.Add(i, item.Date.ToShortDateString(), item.Total.ToString("#,##0.000"));
                        total += item.Total;
                    }

                    lblRevenus.Text = total.ToString("#,##0.000");
                }
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
                    dgvCostofGood.Rows.Add(i, item.costname, item.Cost, item.date.Value.ToShortDateString());

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


    }
}
