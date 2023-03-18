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
                string query = "INSERT INTO [dbo].[LoginCredentials]([username],[password],[balance]) VALUES(@username,@password,@balance)";
                SqlCommand cmd = new SqlCommand(query, con);

                // Insert them
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@balance", balance);

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