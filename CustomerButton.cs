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
    public partial class CustomerButton : PictureBox
    {
        public CustomerButton()
        {
            InitializeComponent();
        }
        private Image NomalImage {  get; set; } 
        private Image HoverImage { get; set; }

        

        private void CustomerButton_MouseHover(object sender, EventArgs e)
        {
            this.Image = HoverImage;
        }

        private void CustomerButton_MouseLeave(object sender, EventArgs e)
        {

        }
    }
}
