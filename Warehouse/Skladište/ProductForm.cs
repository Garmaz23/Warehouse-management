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
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Projekt Informatika skladište\Skladište\Skladište\data.mdf;Integrated Security=True;Connect Timeout=30");
        private void fillcombo()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CatName from CategoryTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            CatCb.ValueMember = "CatName";
            CatCb.DataSource = dt;
            Con.Close();
        }

        private void populate()
        {

            try
            {
                Con.Open();

                string query = "select * from ProductTbl";

                SqlDataAdapter sda = new SqlDataAdapter(query, Con);

                SqlCommandBuilder builder = new SqlCommandBuilder(sda);

                var ds = new DataSet();

                sda.Fill(ds);

                ProdDGV.DataSource = ds.Tables[0];

                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void ProductForm_Load(object sender, EventArgs e)
        {
            fillcombo();
            fillcombo1();
            populate();

        }
      
        //categories button
        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm cat = new CategoryForm();
            cat.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      
        //add product
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }

                Con.Open();

                string query = "insert into ProductTbl values('" + ProdId.Text + "','" + ProdName.Text + "','" + ProdQty.Text + "','"+ ProdPrice.Text+"','"+CatCb.SelectedValue.ToString()+"')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully");

                Con.Close();
                populate();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        //seller button
        private void button1_Click(object sender, EventArgs e)
        {
            SellerForm sell = new SellerForm();
            sell.Show();
            this.Hide();
        }
        //delete product
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                if (ProdId.Text == "")
                {
                    MessageBox.Show("Please type in id of the product into textbox to delete it");
                }
                else
                {
                    Con.Open();
                    string query = "delete from ProductTbl where ProdId=" + ProdId.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product deleted successfully");
                    Con.Close();
                    populate();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //edit product
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                if (ProdId.Text == "" || ProdName.Text == "" || ProdPrice.Text == ""|| ProdQty.Text == "")
                {
                    MessageBox.Show("Please type info of the product into textboxes to edit it");
                }
                else
                {
                    Con.Open();
                    string query = "update ProductTbl set ProdName='" + ProdName.Text + "',ProdQty='" + ProdQty.Text +"',ProdPrice='"+ProdPrice.Text+"',ProdCat='"+CatCb.SelectedValue.ToString()+ "'where ProdId=" + ProdId.Text + ";";
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

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Con.Open();
            string query = "select * from ProductTbl where ProdCat='" + Cb.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void fillcombo1()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CatName from CategoryTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            Cb.ValueMember = "CatName";
            Cb.DataSource = dt;
            Con.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            populate();
        }

        

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }
    }
}
