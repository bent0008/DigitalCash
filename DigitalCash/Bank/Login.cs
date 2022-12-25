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

namespace Bank
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=SQLEXPRESS01;Initial Catalog=Demodb;User ID=Ben_T;Password=xfsdf";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            MessageBox.Show("Connection Open  !");
            cnn.Close();


            string username = usernameTxtbx.Text;
            string password = passwordTxtbx.Text;

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

            for (int i = 0; i < lengthOfDatabase; i++)
            {
                if (usernameDatabase[i] == username)
                {
                    if (passwordDatabase[i] == password)
                    {
                        List<string> customer = new();
                        // username > balance
                        customer.Append(username);
                        customer.Append(balanceDatabase[i]);
                        MessageBox.Show("Welcome " + username + "\nYou have " + balanceDatabase[i]);
                    }
                    else
                    {
                        PasswordWarningBox passWarn = new();
                        passWarn.Show();
                    }
                }
                else
                {
                    UsernameWarningBox userWarn = new();
                    userWarn.Show();
                }

            }
        }
    }
}
