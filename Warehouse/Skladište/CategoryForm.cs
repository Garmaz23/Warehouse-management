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
    public partial class CategoryForm : Form
    {
      

        public CategoryForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductForm cat = new ProductForm();
            cat.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Projekt Informatika skladište\Skladište\Skladište\data.mdf;Integrated Security=True;Connect Timeout=30");
        //add button
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }

                Con.Open();

                string query = "insert into CategoryTbl values('" + CatIdTb.Text + "','" + CatNameTb.Text + "','" + CatDescTb.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Added Successfully");

                Con.Close();
                populate();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        
        private void populate()
        {
            
            try
            {
                Con.Open();

                string query = "select * from CategoryTbl";

                SqlDataAdapter sda = new SqlDataAdapter(query, Con);

                SqlCommandBuilder builder = new SqlCommandBuilder(sda);

                var ds = new DataSet();

                sda.Fill(ds);

                 CatDGV.DataSource = ds.Tables[0];
              
                Con.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       

        }

       
        private void CategoryForm_Load_1(object sender, EventArgs e)
        {
            populate();
        }

        private void CatDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatIdTb.Text = CatDGV.SelectedRows[0].Cells[0].Value.ToString();
            CatNameTb.Text = CatDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatDescTb.Text = CatDGV.SelectedRows[0].Cells[2].Value.ToString();
        }
        //delete button
        private void button7_Click(object sender, EventArgs e)
        {

            try
            {

                if (CatIdTb.Text == "")
                {
                    MessageBox.Show("Please type info of the category into textbox to delete it");
                }
                else
                {
                    Con.Open();
                    string query = "delete from CategoryTbl where CatId=" + CatIdTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category deleted successfully");
                    Con.Close();
                    populate();

                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //edit button
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                if (CatIdTb.Text == "" || CatNameTb.Text == "" || CatDescTb.Text == "")
                {
                    MessageBox.Show("Please type info of the category into textbox to edit it");
                }
                else
                {
                    Con.Open();
                    string query = "update CategoryTbl set CatName='"+CatNameTb.Text+"',CatDesc='"+CatDescTb.Text+"'where CatId="+CatIdTb.Text+"";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category updated successfully");
                    Con.Close();
                    populate();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SellerForm cat = new SellerForm();
            cat.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
            
        }
    }
}
