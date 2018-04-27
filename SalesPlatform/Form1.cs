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

            bool pellets = chckPellets.Checked;
            bool course = chckCourse.Checked;
            bool rust = chckRust.Checked;
            bool paid = chckPaid.Checked;

            double finalPrice = 0;
            int numPellets = 0;
            int numCourse = 0;
            int numRust = 0;


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
            MySqlCommand cmd = new MySqlCommand("INSERT INTO salt_sales (`date`, `seller`,`buyer`,`address`,`email`,`phone`,`pellets`,`course`,`rust`,`total_due`) VALUES ('" + time + "','"
                + sFirst + " " + sLast + "','" + bFirst + " " + bLast
                + "','" + address + "','" + email + "','" + phone + "','"
                + numPellets + "','" + numCourse + "','" + numRust + "','" + finalPrice + "');", conn);
            conn.Open();

            cmd.ExecuteNonQuery();
            
        }
    }
}
