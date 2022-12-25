using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Customer
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        public string connectionString = "Data Source=BEN_T\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";


        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string username = usernameTxtbx.Text;
            string password = passwordTxtbx.Text;

            if (con.State == System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Connection successful");
            }


            if (string.IsNullOrEmpty(username))
            {
                UsernameWarningBox userWarn = new();
                userWarn.Show();

                usernameTxtbx.Clear();
                passwordTxtbx.Clear();
            }
            else if (string.IsNullOrEmpty(password))
            {
                PasswordWarningBox passWarn = new();
                passWarn.Show();

                usernameTxtbx.Clear();
                passwordTxtbx.Clear();
            }

            string query = "INSERT INTO [dbo].[LoginCredentials]([username],[password],[balance]) VALUES(@username,@password,@balance)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            //for (int i = 0; i < lengthOfDatabase; i++)
            //{
            //    if (usernameDatabase[i] == username)
            //    {
            //        if (passwordDatabase[i] == password)
            //        {
            //            List<string> customer = new();
            //            // username > balance
            //            customer.Append(username);
            //            customer.Append(balanceDatabase[i]);
            //            MessageBox.Show("Welcome " + username + "\nYou have " + balanceDatabase[i]);
            //        }
            //        else
            //        {
            //            PasswordWarningBox passWarn = new();
            //            passWarn.Show();
            //        }
            //    }
            //    else
            //    {
            //        UsernameWarningBox userWarn = new();
            //        userWarn.Show();
            //    }

            //}

            con.Close();
        }
    }
}
