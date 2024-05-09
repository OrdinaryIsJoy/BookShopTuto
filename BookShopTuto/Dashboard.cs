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

namespace BookShopTuto
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-570F7VS;Initial Catalog=BookShopDb;Integrated Security=True;Encrypt=False");
       
        
        
        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Books obj = new Books();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Users obj = new Users();
            obj.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Con.Open();
            SqlDataAdapter sdaBooks = new SqlDataAdapter("select sum(BQty) from BookTb1",Con);
            DataTable dtBooks = new DataTable();
            sdaBooks.Fill(dtBooks);
            BookStockLb1.Text = dtBooks.Rows[0][0].ToString();

            SqlDataAdapter sdaAmount = new SqlDataAdapter("select sum(Amount) from BillTb1",Con);
            DataTable dtAmount = new DataTable();
            sdaAmount.Fill(dtAmount);
            AmountStockLb1.Text = dtAmount.Rows[0][0].ToString();
            
            SqlDataAdapter sdaUsers = new SqlDataAdapter("select count(Uid) from UserTb1",Con);
            DataTable dtUsers = new DataTable();
            sdaUsers.Fill(dtUsers);
            UserStockLb1.Text = dtUsers.Rows[0][0].ToString();



            Con.Close();


        }



        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
