using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Customer
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        public string Username { get; set; }
        public int Balance { get; set; }
        public bool LoggedIn { get; set; }
        public int ID { get; set; }

        private void UpdateLabels()
        {
            usernameLbl.Text = Username;
            balanceAmountLbl.Text = "$" + Balance;
        }

        private void AddCustomerBtn_Click(object sender, EventArgs e)
        {
            AddCustomer addCust = new();
            addCust.Show();
        }

        private void MoneyOrderBtn_Click(object sender, EventArgs e)
        {
            if (LoggedIn)
            {
                MoneyOrder newOrder = new();
                newOrder.Show();
            }
            else
            {
                MessageBox.Show("Please log in.", "Error");
            }
        }

        private void CustomerLoginBtn_Click(object sender, EventArgs e)
        {
            Login custLogin = new();
            custLogin.ShowDialog(this);
            UpdateLabels();
        }

        private void EncryptBtn_Click(object sender, EventArgs e)
        {
            Encrypt encr = new();
            encr.Show();
        }

    }
}