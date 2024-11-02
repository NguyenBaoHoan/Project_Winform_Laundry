using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;
using ZXing;// mã nguồn mở dùng để tạo mã 
using System.Drawing.Drawing2D;
using Project1_Laundry.Models;
using System.Runtime.Remoting.Contexts;


namespace Project1_Laundry
{
    public partial class CreateQRMoMo : Form
    {
        private readonly LaundryContextDB context;
        private readonly Cash cash;
        public CreateQRMoMo(Cash cashForm)
        {
            InitializeComponent();
            cash = cashForm;
            context = new LaundryContextDB();
        }
        private void btnSuccess_Click(object sender, EventArgs e)
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
            module.LoadReceipt(txtsotien.Text, "0");
            module.ShowDialog();


            MessageBox.Show("Thanh toán đã lưu thành công!", "Thành Toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cash.loadCash();

            this.Dispose();
            cash.btnAddCustomer.Enabled = true;
            cash.btnAddService.Enabled = false;
            cash.getTransno();
        }



        public Image resizeImage(Image image, int new_height, int new_width)
        {
            Bitmap new_image = new Bitmap(new_width, new_height);
            Graphics g = Graphics.FromImage((Image)new_image);
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, new_width, new_height);
            return new_image;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        

        private void btnCreate_Click_1(object sender, EventArgs e)
        {
            var qrcode_text = $"2|99|{txtphone.Text.Trim()}|{txtname.Text.Trim()}|Null|0|0|{txtsotien.Text.Trim()}";
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            EncodingOptions encodingOptions = new EncodingOptions() { Width = 250, Height = 250, Margin = 0, PureBarcode = false };
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            barcodeWriter.Renderer = new BitmapRenderer();
            barcodeWriter.Options = encodingOptions;
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            Bitmap bitmap = barcodeWriter.Write(qrcode_text);
            Bitmap logo = resizeImage(Properties.Resources.logo_momo, 64, 64) as Bitmap;
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(logo, new Point((bitmap.Width - logo.Width) / 2, (bitmap.Height - logo.Height) / 2));
            pic_qrcode.Image = bitmap;
        }

        
    }
}
