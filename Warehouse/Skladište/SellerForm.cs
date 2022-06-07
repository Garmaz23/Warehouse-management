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
    public partial class SellerForm : Form
    {
        public SellerForm()
        {
            InitializeComponent();
        }


        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Projekt Informatika skladište\Skladište\Skladište\data.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
          
            try
            {
                Con.Open();

                string query = "select * from SellerTbl";

                SqlDataAdapter sda = new SqlDataAdapter(query, Con);

                SqlCommandBuilder builder = new SqlCommandBuilder(sda);

                var ds = new DataSet();

                sda.Fill(ds);

                SellerDGV.DataSource = ds.Tables[0];

                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            populate();
        }

        private void SellerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SellerId.Text = SellerDGV.SelectedRows[0].Cells[0].Value.ToString();
            SellerName.Text = SellerDGV.SelectedRows[0].Cells[1].Value.ToString();
            SellerAge.Text = SellerDGV.SelectedRows[0].Cells[2].Value.ToString();
            SellerPhone.Text = SellerDGV.SelectedRows[0].Cells[3].Value.ToString();
            SellerPassword.Text = SellerDGV.SelectedRows[0].Cells[4].Value.ToString();
            
        }
        //add seller
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }

                Con.Open();

                string query = "insert into SellerTbl values('" + SellerId.Text + "','" + SellerName.Text + "','" + SellerAge.Text + "," + SellerPhone.Text + "," + SellerPassword.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seller added successfully");

                Con.Close();
                populate();
                Con.Close();
                populate();
                SellerId.Text = "";
                SellerName.Text = "";
                SellerAge.Text = "";
                SellerPhone.Text = "";
                SellerPassword.Text = "";
                





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductForm cat = new ProductForm();
            cat.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm cat = new CategoryForm();
            cat.Show();
            this.Hide();
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            populate();
        }
        //delete seller
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                if (SellerId.Text == "")
                {
                    MessageBox.Show("Please type in id of the seller into textbox to delete it");
                }
                else
                {
                    Con.Open();
                    string query = "delete from SellerTbl where SellerId=" + SellerId.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller deleted successfully");
                    Con.Close();
                    populate();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                if (SellerId.Text == "" || SellerName.Text == "" || SellerAge.Text == "" || SellerPhone.Text == "" || SellerPassword.Text =="")
                {
                    MessageBox.Show("Please type info of the seller into textboxes to edit it");
                }
                else
                {
                    Con.Open();
                    string query = "update SellerTbl set SellerName='" + SellerName.Text + "',SellerAge='" + SellerPhone.Text + "',ProdPrice='" + SellerPassword.Text + "'where SellerId=" + SellerId.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product updated successfully");
                    Con.Close();
                    populate();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }
    }
}
