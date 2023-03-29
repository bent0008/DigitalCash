﻿using System;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        public string connectionString = "Data Source=BEN_T\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            bool codeExecuted = false;

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string username = usernameTxtbx.Text;
            string password = passwordTxtbx.Text;
            int dbBalance = 0;

            string query = "SELECT [balance] FROM LoginCredentials WHERE username = @username AND password = @password";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dbBalance = reader.GetInt32(0);
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