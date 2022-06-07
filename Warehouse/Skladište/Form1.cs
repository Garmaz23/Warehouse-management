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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string Sellername = "";
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Projekt Informatika skladište\Skladište\Skladište\data.mdf;Integrated Security=True;Connect Timeout=30");

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Clearlabel_Click(object sender, EventArgs e)
        {
            UnameTb.Text = "";
            PassTb.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(UnameTb.Text ==""|| PassTb.Text == "")
            {
                MessageBox.Show("Enter username and password");
            }
            else
            {
                if (RoleCb.SelectedIndex > -1)
                {

                    if (RoleCb.SelectedItem.ToString() == "Admin")
                    {
                        if (UnameTb.Text == "Admin" && PassTb.Text == "split021")
                        {
                            ProductForm prod = new ProductForm();
                            prod.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If you are the Admin, enter the correct username and password");
                        }
                    }

                    else
                    {
                        //MessageBox.Show("You are in the operator section");
                        Con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("Select count(8) from SellerTbl where SellerName='" + UnameTb.Text + "'and SellerPassword='" + PassTb.Text + "'", Con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            Sellername = UnameTb.Text;
                            SellingForm sell = new SellingForm();
                            sell.Show();
                            this.Hide();
                            Con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong username or password");
                        }
                        Con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Select a role");
                }
            }
        }

       
    }
}
