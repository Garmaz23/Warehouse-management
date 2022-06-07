using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Skladište
{
    public partial class SellingForm : Form
    {
        public SellingForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Projekt Informatika skladište\Skladište\Skladište\data.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {

            try
            {
                Con.Open();

                string query = "select ProdName, ProdQty, ProdPrice from ProductTbl";

                SqlDataAdapter sda = new SqlDataAdapter(query, Con);

                SqlCommandBuilder builder = new SqlCommandBuilder(sda);

                var ds = new DataSet();

                sda.Fill(ds);

                ProdDGV1.DataSource = ds.Tables[0];

                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void populatebills()
        {

            try
            {
                Con.Open();

                string query = "select * from BillTbl";

                SqlDataAdapter sda = new SqlDataAdapter(query, Con);

                SqlCommandBuilder builder = new SqlCommandBuilder(sda);

                var ds = new DataSet();

                sda.Fill(ds);

                BillsDGV.DataSource = ds.Tables[0];

                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        //add button
        private void button4_Click(object sender, EventArgs e)
        {
            if(BillID.Text == "")
            {
                MessageBox.Show("Missing Bill Id");
            }
            else
            {
                try
                {
                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }

                    Con.Open();

                    string query = "insert into  BillTbl values('" + BillID.Text + "','" + SellerNamelb.Text + "','" + Datelb.Text + "','" + Amtlb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order Added Successfully");

                    Con.Close();
                    populatebills();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            
        }

     

        private void ProdDGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdName.Text = ProdDGV1.SelectedRows[0].Cells[0].Value.ToString();
            ProdPrice.Text = ProdDGV1.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void SellingForm_Load(object sender, EventArgs e)
        {
            populate();
            populatebills();
            fillcombo();
            SellerNamelb.Text = Form1.Sellername;

        }
        int flag = 0;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Datelb.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

      

      

        private void button2_Click(object sender, EventArgs e)
        {
            int m = 0,  Grdtotal = 0;


            if (ProdName.Text == ""|| ProdQty.Text == "")
            {
                MessageBox.Show("Missing data");
            }
            else
            {
                 int total = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(ProdQty.Text);
                
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(OrderDGV);
                //newRow.Cells[0].Value = m + 1;
                newRow.Cells[0].Value = ProdName.Text;
                newRow.Cells[1].Value = ProdPrice.Text;
                newRow.Cells[2].Value = ProdQty.Text;
                
                newRow.Cells[3].Value = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(ProdQty.Text);
                OrderDGV.Rows.Add(newRow);

               // m = m + 1;
               
                Grdtotal += total;
                Amtlb.Text =  Grdtotal +"";
            }
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(printPreviewDialog1.ShowDialog()== DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void BillsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            flag = 1;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(" Your order", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Blue, new Point(230));
            e.Graphics.DrawString("Bill ID:"+BillsDGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Red, new Point(100,70));
            e.Graphics.DrawString("Seller name:" + BillsDGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Red, new Point(100, 100));
            e.Graphics.DrawString("Date:" + BillsDGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Red, new Point(100, 130));
            e.Graphics.DrawString("Total amount:" + BillsDGV.SelectedRows[0].Cells[3].Value.ToString()+"$", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Red, new Point(100, 160));
            e.Graphics.DrawString("Garmaz & Co.", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(270,230));






        }

        private void button1_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void SearchCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Con.Open();
            string query = "select ProdName,ProdQty, ProdPrice from ProductTbl where ProdCat='" + SearchCb.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV1.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void fillcombo()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CatName from CategoryTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            SearchCb.ValueMember = "CatName";
            SearchCb.DataSource = dt;
            Con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }
    }

}
