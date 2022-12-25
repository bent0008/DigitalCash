using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Customer
{
    public partial class MoneyOrder : Form
    {
        public MoneyOrder()
        {
            InitializeComponent();
        }


        public string connectionString = "Data Source=BEN_T\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";


        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (cheatChkbx.Checked == true)
            {
                MessageBox.Show("You Cheated");
            }
            else
            {
                CreateMoneyOrder();
            }

        }

        private void CreateMoneyOrder()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string amount = moneyAmountTxtbx.Text;
            int occurances = 100;
            Random rand = new();
            int serialNum = rand.Next(1000000, 9999999);

            if (con.State == System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Connection successful");
            }

            MessageBox.Show(serialNum.ToString());
            //MessageBox.Show();

            for (int i = 0; i < occurances; i++)
            {
                // index, amount, serialNum
            }
        }

        private void GenerateIdentifier()
        {
            //// Generate RSA key pair
            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            //// Export public key to file
            //string publicKeyFile = "publickey";
            //File.WriteAllText(publicKeyFile, rsa.ToXmlString(false));

            //// Export private key to file
            //string privateKeyFile = "privatekey";
            //File.WriteAllText(privateKeyFile, rsa.ToXmlString(true));


            //Random rand = new();
            //int identifier = rand.Next(1000000, 9999999);

        }

    }
}
