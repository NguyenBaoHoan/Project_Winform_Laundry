using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.Reporting.WinForms;
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
    public partial class Receipt : Form
    {
        private readonly LaundryContextDB context;
        Cash cash;
        string title = "Laundry Shop";
        public Receipt(Cash cashForm)
        {
            InitializeComponent();
            context = new LaundryContextDB();
            cash = cashForm;
        }

        private void Receipt_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
        public void LoadReceipt(string pcash, string pchange)
        {
            try
            {
                
                // Thiết lập đường dẫn báo cáo
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report1.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                // Truy vấn dữ liệu từ bảng tbCash và tbService
                var receiptData = context.tbCashes
                    .Where(c => c.transno == cash.lblTransno.Text)
                    .Select(c => new
                    {
                        ServiceName = c.tbService.name,
                        Price = c.price
                    })
                    .ToList();

                // Chuyển dữ liệu truy vấn thành DataTable cho ReportViewer
                DataTable dtReceipt = new DataTable();
                dtReceipt.Columns.Add("name", typeof(string));
                dtReceipt.Columns.Add("price", typeof(string));

                foreach (var item in receiptData)
                {
                    dtReceipt.Rows.Add(item.ServiceName, item.Price);
                }

                // Đặt các tham số cho báo cáo
                ReportParameter tienHoanTra = new ReportParameter("tienHoanTra", pchange);
                ReportParameter tienMat = new ReportParameter("tienMat", pcash);
                ReportParameter phaiThanhToan = new ReportParameter("phaiThanhToan", cash.lblTotal.Text);
                ReportParameter giaoDich = new ReportParameter("giaoDich", cash.lblTransno.Text);

                // Thêm tham số vào ReportViewer
                reportViewer1.LocalReport.SetParameters(new[] { tienHoanTra, tienMat, phaiThanhToan, giaoDich });

                // Thêm nguồn dữ liệu vào ReportViewer
                ReportDataSource rptDataSource = new ReportDataSource("DataSet1", dtReceipt);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);

                // Cài đặt chế độ hiển thị của ReportViewer
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.FullPage;
                reportViewer1.ZoomPercent = 30;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
