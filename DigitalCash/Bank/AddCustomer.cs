using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private void submitBtn_Click(object sender, EventArgs e)
        {
            string username = customerNameTxtbx.Text;
            string password = customerPwdTxtbx.Text;
            string balance = customerBalanceTxtbx.Text;
            
            if (string.IsNullOrEmpty(username))
            {
                UsernameWarningBox userWarn = new();
                userWarn.Show();

                customerNameTxtbx.Clear();
                customerPwdTxtbx.Clear();
                customerBalanceTxtbx.Clear();
            }
            else if (string.IsNullOrEmpty(password))
            {
                PasswordWarningBox passWarn = new();
                passWarn.Show();

                customerNameTxtbx.Clear();
                customerPwdTxtbx.Clear();
                customerBalanceTxtbx.Clear();
            }
            else if (string.IsNullOrEmpty(balance))
            {
                BalanceWarningBox balWarn = new();
                balWarn.Show();

                customerNameTxtbx.Clear();
                customerPwdTxtbx.Clear();
                customerBalanceTxtbx.Clear();
            }
            else 
            {
                // add these to db

                this.Close();
            }
        }
    }
}
