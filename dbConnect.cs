using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1_Laundry
{
    internal class dbConnect

    {

        SqlCommand cm = new SqlCommand();
        private SqlConnection cn = new SqlConnection("server = NguyenHoan\\SQLEXPRESS; DATABASE = QL_LAUNDRY; USER ID = sa; PWD = 25251325cz");
        
        public SqlConnection connect()
        {
            return cn;
        }
        public void open()
        {
            if (cn.State == System.Data.ConnectionState.Closed)
                cn.Open();
        }
        public void close()
        {
            if (cn.State == System.Data.ConnectionState.Open)
                cn.Close();
        }
        public void executeQuery(string sql)
        {
            try
            {
                open();
                cm = new SqlCommand(sql, connect());
                cm.ExecuteNonQuery();
                close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
