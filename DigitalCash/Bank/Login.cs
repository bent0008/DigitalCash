﻿using System;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        public string connectionString = "Data Source=LAPTOP-UOPDFGH4\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            bool codeExecuted = false;

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string username = usernameTxtbx.Text;
            string password = passwordTxtbx.Text;
            int dbBalance = 0;
            int dbID = 0;
            string hashedPassword;

            // hash the password to check with database
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] computedPasswordHash = sha256.ComputeHash(passwordBytes);
                hashedPassword = Convert.ToBase64String(computedPasswordHash);
            }

            string query = "SELECT [balance], [ID] FROM LoginCredentials WHERE username = @username AND password = @password";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", hashedPassword);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dbBalance = reader.GetInt32(0);
                    dbID = reader.GetInt32(1);
                    codeExecuted = true;
                }
                if (codeExecuted == false)
                {
                    MessageBox.Show("Error, the username or password is invalid.", "Error");
                    usernameTxtbx.Clear();
                    passwordTxtbx.Clear();
                }
                else
                {
                    Bank bankForm = this.Owner as Bank;

                    if (bankForm != null)
                    {
                        bankForm.Balance = dbBalance;
                        bankForm.Username = username;
                        bankForm.LoggedIn = true;
                        bankForm.ID = dbID;
                        con.Close();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error: the bank form was not found.", "Error");
                    }
                }
                reader.Close();
                con.Close();
            }
            
        }
    }
}
