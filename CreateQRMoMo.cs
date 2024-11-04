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
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        // thay đổi ảnh, logo momo để chèn vào giữa mã qr
        public Image resizeImage(Image image, int new_height, int new_width) // Định nghĩa hàm resizeImage để thay đổi kích thước hình ảnh
        {
            Bitmap new_image = new Bitmap(new_width, new_height); // Tạo một Bitmap mới với kích thước mới (new_width x new_height)
            Graphics g = Graphics.FromImage((Image)new_image); // Tạo đối tượng Graphics từ Bitmap mới để thực hiện các thao tác vẽ
            g.InterpolationMode = InterpolationMode.High; // Đặt chế độ nội suy để đảm bảo chất lượng cao khi thay đổi kích thước
            g.DrawImage(image, 0, 0, new_width, new_height); // Vẽ hình ảnh gốc vào Bitmap mới với kích thước đã chỉnh
            return new_image; // Trả về hình ảnh đã được thay đổi kích thước
        }



        private void btnCreate_Click_1(object sender, EventArgs e) // Định nghĩa hàm xử lý sự kiện khi nhấn nút "Create"
        {
            // Tạo chuỗi mã QR theo định dạng yêu cầu của MoMo, chứa các thông tin từ ô nhập liệu (số điện thoại, tên, số tiền)
            var qrcode_text = $"2|99|{txtphone.Text.Trim()}|{txtname.Text.Trim()}|Null|0|0|{txtsotien.Text.Trim()}";

            BarcodeWriter barcodeWriter = new BarcodeWriter(); // Tạo một đối tượng BarcodeWriter để viết mã QR

            // Thiết lập các tùy chọn mã hóa cho mã QR
            EncodingOptions encodingOptions = new EncodingOptions()
            {
                Width = 250, // Đặt chiều rộng của mã QR
                Height = 250, // Đặt chiều cao của mã QR
                Margin = 0, // Đặt lề bằng 0 (không có khoảng trắng xung quanh mã QR)
                PureBarcode = false // Cho phép chèn logo vào giữa mã QR
            };

            // Thêm tùy chọn sửa lỗi (Error Correction Level) ở mức cao (H) để mã QR có thể chứa logo mà vẫn dễ quét
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

            barcodeWriter.Renderer = new BitmapRenderer(); // Thiết lập Renderer để BarcodeWriter vẽ mã QR dưới dạng Bitmap
            barcodeWriter.Options = encodingOptions; // Gán tùy chọn mã hóa đã thiết lập cho BarcodeWriter
            barcodeWriter.Format = BarcodeFormat.QR_CODE; // Đặt định dạng mã vạch là QR Code

            // Viết mã QR từ chuỗi qrcode_text và lưu thành đối tượng Bitmap
            Bitmap bitmap = barcodeWriter.Write(qrcode_text);

            // Sử dụng hàm resizeImage để thay đổi kích thước logo MoMo xuống 64x64 pixel
            Bitmap logo = resizeImage(Properties.Resources.logo_momo, 64, 64) as Bitmap;

            Graphics g = Graphics.FromImage(bitmap); // Tạo đối tượng Graphics từ mã QR Bitmap để thực hiện thao tác vẽ logo
                                                     // Vẽ logo vào trung tâm mã QR (tính vị trí giữa dựa trên kích thước mã QR và logo)
            g.DrawImage(logo, new Point((bitmap.Width - logo.Width) / 2, (bitmap.Height - logo.Height) / 2));

            pic_qrcode.Image = bitmap; // Hiển thị mã QR (có logo) trong PictureBox trên giao diện người dùng
        }



    }
}
