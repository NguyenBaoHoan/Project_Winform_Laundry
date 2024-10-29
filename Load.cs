using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project1_Laundry
{
    public partial class Load : Form
    {

        public Load()
        {
            InitializeComponent();
        }
        
        int startpoint = 0;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            startpoint += 1;
            ProgressIndicator1.Start();
            if (startpoint > 40)
            {
                LoginForm login = new LoginForm();
                ProgressIndicator1.Stop();
                timer1.Stop();
                this.Hide();
                login.Show();
            }
        }

        private void Load_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        
    }
}
