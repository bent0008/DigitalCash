using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank
{
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
        }


        public string connectionString = "Data Source=BEN_T\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";


        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string username = customerNameTxtbx.Text;
            string password = customerPwdTxtbx.Text;
            string balance = customerBalanceTxtbx.Text;
            string hashedPassword;

            // create a random ID for the customer
            Random rand = new();
            int idNumber = rand.Next(100000000, 999999999);

            // hash the password
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] computedPasswordHash = sha256.ComputeHash(passwordBytes);
                hashedPassword = Convert.ToBase64String(computedPasswordHash);
            }

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Error, the username can not be empty", "Error");

                customerNameTxtbx.Clear();
                customerPwdTxtbx.Clear();
                customerBalanceTxtbx.Clear();
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Error, the password can not be empty", "Error");

                customerNameTxtbx.Clear();
                customerPwdTxtbx.Clear();
                customerBalanceTxtbx.Clear();
            }
            else if (string.IsNullOrEmpty(balance))
            {
                MessageBox.Show("Error, the balance can not be empty", "Error");

                customerNameTxtbx.Clear();
                customerPwdTxtbx.Clear();
                customerBalanceTxtbx.Clear();
            }
            else
            {
                // add these to db
                string query = "INSERT INTO [dbo].[LoginCredentials]([username],[password],[balance],[ID]) VALUES(@username,@password,@balance,@ID)";
                SqlCommand cmd = new SqlCommand(query, con);

                // Insert them
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@balance", balance);
                cmd.Parameters.AddWithValue("@ID", idNumber);

                cmd.ExecuteNonQuery();

                Bank bankForm = this.Owner as Bank;

                bankForm.Balance = Convert.ToInt32(balance);
                bankForm.Username = username;
                bankForm.LoggedIn = true;
                this.DialogResult = DialogResult.OK;

                this.Close();
            }
        }
    }
}