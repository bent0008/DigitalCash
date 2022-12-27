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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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


            // delete previous MoneyOrder table elements
            string deleteQuery = "DELETE FROM [dbo].[MoneyOrder]";
            SqlCommand comm = new SqlCommand(deleteQuery, con);
            comm.ExecuteNonQuery();

            for (int i = 1; i < (occurances + 1); i++)
            {
                Random rand = new();
                int serialNum = rand.Next(1000000, 9999999);

                // add these to MoneyOrder
                string query = "INSERT INTO [dbo].[MoneyOrder]([index],[moneyAmount],[serialNumber]) VALUES(@index,@moneyAmount,@serialNumber)";
                SqlCommand cmd = new SqlCommand(query, con);

                // Insert them to MoneyOrder
                cmd.Parameters.AddWithValue("@index", i);
                cmd.Parameters.AddWithValue("@moneyAmount", amount);
                cmd.Parameters.AddWithValue("@serialNumber", serialNum);

                cmd.ExecuteNonQuery();

                // add these to ArchivedOrders
                string queryTwo = "INSERT INTO [dbo].[ArchivedOrders]([index],[moneyAmount],[serialNumber]) VALUES(@index,@moneyAmount,@serialNumber)";
                SqlCommand cmd2 = new SqlCommand(queryTwo, con);

                // Insert them to ArchivedOrders
                cmd2.Parameters.AddWithValue("@index", i);
                cmd2.Parameters.AddWithValue("@moneyAmount", amount);
                cmd2.Parameters.AddWithValue("@serialNumber", serialNum);

                cmd2.ExecuteNonQuery();

                
            }
            this.Close();
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
