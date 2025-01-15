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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        public static string UserName = "";
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-570F7VS;Initial Catalog=BookShopDb;Integrated Security=True;Encrypt=False");
        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from UserTb1 where UName='"+ UNameTb1.Text +"'and UPassword ='"+ UPasswordTb1.Text +"'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                UserName = UNameTb1.Text;
                Billing obj = new Billing();
                obj.Show();
                this.Hide();
                Con.Close();

            }
            else
            {
                MessageBox.Show("用户名或密码错误");
            }
            Con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin obj = new AdminLogin();
            obj.Show();
            this.Hide();
        }
    }
}
