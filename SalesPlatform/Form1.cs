using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SalesPlatform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            String time = DateTime.Now.ToString("yyy-MM-dd");
            String sFirst = txtSellerFirst.Text;
            String sLast = txtSellerLast.Text;
            String bFirst = txtBuyerFirst.Text;
            String bLast = txtBuyerLast.Text;
            String address = txtAddress.Text;
            String email = txtEmail.Text;
            String phone = txtPhone.Text;
            String comments = txtConcerns.Text;
            String hasPaid = "false";

            bool pellets = chckPellets.Checked;
            bool course = chckCourse.Checked;
            bool rust = chckRust.Checked;
            bool paid = chckPaid.Checked;

            double finalPrice = 0;
            int numPellets = 0;
            int numCourse = 0;
            int numRust = 0;

            if(paid)
            {
                hasPaid = "true";
            } else
            {
                hasPaid = "false";
            }

            if (cmbPellets.SelectedItem != null)
            {
                numPellets = int.Parse(cmbPellets.Text);
            }

            if (cmbCourse.SelectedItem != null)
            {
                numCourse = int.Parse(cmbCourse.Text);
            }

            if (cmbRust.SelectedItem != null)
            {
                numRust = int.Parse(cmbRust.Text);
            }

            if(pellets == true && numPellets >= 1)
            {
                finalPrice = finalPrice + (numPellets * 7.25);
            }

            if(course == true && numCourse >= 1)
            {
                finalPrice = finalPrice + (numCourse * 6.75);
            }

            if(rust == true && numRust >= 1)
            {
                finalPrice = finalPrice + (numRust * 7.75);
            }

            MySqlConnectionStringBuilder connString = new MySqlConnectionStringBuilder();
            connString.Server = "10.21.6.153";
            connString.UserID = "root";
            connString.Password = "26981";
            connString.Database = "test";

            MySqlConnection conn = new MySqlConnection(connString.ToString());
            MySqlCommand cmd = new MySqlCommand("INSERT INTO salt_sales (`date`, `seller`,`buyer`,`address`,`email`,`phone`,`pellets`,`course`,`rust`,`total_due`, `has_paid`,`concerns`) VALUES ('" + time + "','"
                + sFirst + " " + sLast + "','" + bFirst + " " + bLast
                + "','" + address + "','" + email + "','" + phone + "','"
                + numPellets + "','" + numCourse + "','" + numRust + "','" + finalPrice + "','" + paid + "','" + comments + "');", conn);
            conn.Open();

            cmd.ExecuteNonQuery();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lstSales.Items.Clear();

            MySqlConnectionStringBuilder connString = new MySqlConnectionStringBuilder();
            connString.Server = "10.21.6.153";
            connString.UserID = "root";
            connString.Password = "26981";
            connString.Database = "test";

            MySqlConnection conn = new MySqlConnection(connString.ToString());
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM salt_sales;", conn);
            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                ListViewItem lst = new ListViewItem(reader["ID"].ToString());
                lst.SubItems.Add(reader["seller"].ToString());
                lst.SubItems.Add(reader["seller"].ToString());
                lst.SubItems.Add(reader["pellets"].ToString());
                lst.SubItems.Add(reader["course"].ToString());
                lst.SubItems.Add(reader["rust"].ToString());
                lst.SubItems.Add(reader["address"].ToString());
                lst.SubItems.Add(reader["phone"].ToString());
                lst.SubItems.Add(reader["has_paid"].ToString());
                

                lstSales.Items.Add(lst);
            }
            conn.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ID = Int32.Parse(lstSales.SelectedItems[0].Text);

            string address = "";
            string email = "";
            string phone = "";
            string concerns = "";
            string pellets;
            string course;
            string rust;

            MySqlConnectionStringBuilder connString = new MySqlConnectionStringBuilder();
            connString.Server = "10.21.6.153";
            connString.UserID = "root";
            connString.Password = "26981";
            connString.Database = "test";

            MySqlConnection conn = new MySqlConnection(connString.ToString());
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM salt_sales WHERE `ID` = ('" + ID + "');", conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                address = reader["address"].ToString();
                email = reader["email"].ToString();
                phone = reader["phone"].ToString();
                concerns = reader["concerns"].ToString();
                
                
                if(reader["pellets"].ToString() != "0")
                {
                    pellets = reader["pellets"].ToString();
                    chckPellets1.Checked = true;
                    cmbPellets1.SelectedIndex = Int32.Parse(reader["pellets"].ToString()) - 1;
                }

                if (reader["course"].ToString() != "0")
                {
                    course = reader["course"].ToString();
                    chckCourse1.Checked = true;
                    cmbCourse1.Items.Add(course);
                    cmbCourse1.SelectedIndex = Int32.Parse(reader["course"].ToString()) - 1;
                }

                if (reader["rust"].ToString() != "0")
                {
                    rust = reader["rust"].ToString();
                    chckRust1.Checked = true;
                    cmbRust1.Items.Add(rust);
                    cmbRust1.SelectedIndex = Int32.Parse(reader["rust"].ToString()) - 1;
                }

                if(reader["has_paid"].ToString() == "True")
                {
                    chckPaid1.Checked = true;
                }

                txtAddress1.Text = address;
                txtEmail1.Text = email;
                txtPhn1.Text = phone;
                txtConcers1.Text = concerns;
            }
            conn.Close();
        }
    }
}
