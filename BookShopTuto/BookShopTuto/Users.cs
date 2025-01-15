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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-570F7VS;Initial Catalog=BookShopDb;Integrated Security=True;Encrypt=False");

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || UPhoneTb.Text == "" || UAddTb.Text == "" || UPasswordTb.Text == "" )
            {
                MessageBox.Show("信息缺失!");
            }
            else
            {
                try
                {
                    Con.Open();
                    string quert = "insert into UserTb1 values('" + UNameTb.Text + "','" + UPhoneTb.Text + "','" + UAddTb.Text + "','" +UPasswordTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(quert, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("用户信息保存成功");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void populate()
        {
            Con.Open();
            string query = "select * from UserTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Reset()
        {
            UNameTb.Text = string.Empty;
            UAddTb.Text = string.Empty; 
            UPhoneTb.Text = string.Empty;
            UPasswordTb.Text = string.Empty;
            Key = 0;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Reset();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("信息缺失!");
            }
            else
            {
                try
                {
                    Con.Open();
                    string quert = "delete from UserTb1 where UId = " + Key + "";
                    SqlCommand cmd = new SqlCommand(quert, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("用户信息已删除");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        int Key =0;
        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UNameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            UPhoneTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            UAddTb.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();
            UPasswordTb.Text = UserDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (UNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(UserDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || UPhoneTb.Text == "" || UAddTb.Text == "" || UPasswordTb.Text == "" )
            {
                MessageBox.Show("信息缺失!");
            }
            else
            {
                try
                {
                    Con.Open();
                    string quert = "update UserTb1 set UName ='" + UNameTb.Text + "',UPhone = '" + UPhoneTb.Text + "',UAdd = '" + UAddTb.Text+ "',UPassword='" + UPasswordTb.Text + "' where UId = " + Key + "         ";
                    SqlCommand cmd = new SqlCommand(quert, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("用户信息已更新");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Books obj = new Books();
            obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
