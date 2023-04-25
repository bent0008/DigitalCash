using Microsoft.VisualBasic;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Bank
{
    public partial class Deposit : Form
    {
        public Deposit()
        {
            InitializeComponent();
        }

        public string connectionString = "Data Source=LAPTOP-UOPDFGH4\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";

        private void DepositBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            Bank bankForm = this.Owner as Bank;

            if (bankForm.LoggedIn)
            {
                string depositString = amountTxtbx.Text;
                double deposit = double.Parse(depositString);
                double currentBalance = Convert.ToInt32(bankForm.Balance);
                double newBalance = deposit + currentBalance;

                string query = "UPDATE [dbo].[LoginCredentials] SET balance = @balance WHERE username = @username";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@username", bankForm.Username);
                cmd.Parameters.AddWithValue("@balance", newBalance);

                cmd.ExecuteNonQuery();

                if (bankForm != null)
                {
                    bankForm.Balance = Convert.ToInt32(newBalance);
                    con.Close();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error: the bank form was not found.", "Error");
                }
            }
            else
            {
                MessageBox.Show("Please login.", "Error");
                this.Close();
            }
        }
    }
}
