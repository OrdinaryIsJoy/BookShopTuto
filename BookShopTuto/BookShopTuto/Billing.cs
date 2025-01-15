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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }

        private void Reset()
        {
            BTitleTb.Text = string.Empty;
            BPriceTb.Text = string.Empty;
            QtyTb.Text = string.Empty;
            //Key = 0;
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
        int n = 0;
        int GrdTotal = 0;
        private void AddToBillBtn_Click(object sender, EventArgs e)
        {

            int num = 0;
            if (BTitleTb.Text == "")
            {
                MessageBox.Show("信息缺失!");
            }
            else if (!int.TryParse(QtyTb.Text, out num))
            {
                MessageBox.Show("数字!");
            }
            else if (Convert.ToInt32(QtyTb.Text) > stock)
            {
                MessageBox.Show("库存不够!");
            }
            else if (Convert.ToInt32(QtyTb.Text) <= 0)
            {
                MessageBox.Show("？");
            }
            else
            {
                n++;
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(BPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n;
                newRow.Cells[1].Value = BTitleTb.Text;
                newRow.Cells[2].Value = BPriceTb.Text;
                newRow.Cells[3].Value = QtyTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                UpdateDGV();
                Reset();
                GrdTotal = GrdTotal + total;
                GrdTotelLb1.Text = GrdTotal.ToString();
            }
        }

        private void UpdateDGV()
        {
            int Qty1 = 0;
            int Qty2 = 0;
            int.TryParse(QtyTb.Text, out Qty1);
            int.TryParse(BookDGV.SelectedRows[0].Cells[4].Value.ToString(), out Qty2);
            BookDGV.SelectedRows[0].Cells[4].Value = Qty2 - Qty1;
            stock = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[4].Value);
            

            //MessageBox.Show(stock.ToString());

        }


        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-570F7VS;Initial Catalog=BookShopDb;Integrated Security=True;Encrypt=False");

        private void populate()
        {
            Con.Open();
            string query = "select * from BookTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int Key = 0;
        int stock = 0;
        List<int> Keylist = new List<int>();
        List<int> stocklist = new List<int>();
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BTitleTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            BPriceTb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            QtyTb.Text = "1";
            //BookDGV.SelectedRows[0].Cells[5].Value.ToString();

            if (BTitleTb.Text == "")
            {
                Key = 0;
                stock = 0;
            }
            else
            {
                Key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString()); 
                Keylist.Add(Key);
                stock = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[4].Value);
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {   
            
           
            if (BillDGV.Rows[0].Cells[0].Value ==null)
            {
                MessageBox.Show("您还没有挑选商品");
            }
            else
            {
                //打印小票
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                } 
                //更新数据库
                try
                {
                    Con.Open();
                    string quert = "insert into BillTb1 values('" + Login.UserName + "'," + GrdTotelLb1.Text + ")";
                    SqlCommand cmd = new SqlCommand(quert, Con);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        int prodid, prodqty, prodprice, tottal, pos = 60;

        private void UserNameLb1_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserNameLb1_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();

        }

        private void UserNameLb1_Paint(object sender, PaintEventArgs e)
        {
            UserNameLb1.Text = Login.UserName;
        }

        
        string prodname;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("鱼鱼书店", new Font("宋体", 8, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("编号 产品 价格 数量 总计", new Font("宋体", 8, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach (DataGridViewRow row in BillDGV.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column7"].Value);
                prodname = "" + row.Cells["Column8"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column9"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column10"].Value);
                tottal = Convert.ToInt32(row.Cells["Column11"].Value);
                e.Graphics.DrawString("" + prodid, new Font("宋体", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("宋体", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("宋体", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("宋体", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + tottal, new Font("宋体", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("订单总额: ￥" + GrdTotal, new Font("宋体", 12, FontStyle.Bold), Brushes.Crimson, new Point(60, pos + 50));
            e.Graphics.DrawString("*****************鱼鱼书店*****************", new Font("宋体", 10, FontStyle.Bold), Brushes.Crimson, new Point(60, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            /*
             foreach (var item in Keylist)
            {
                UpdateBook(item);
            }
            */

            GrdTotal = 0;

        }
        private void UpdateBook(int Index)
        {
            try
            {
                Con.Open();
                string quert = "update BookTb1 set BQty = " + stock + " where BId = " + Index + "";
                SqlCommand cmd = new SqlCommand(quert, Con);
                cmd.ExecuteNonQuery();
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
}
