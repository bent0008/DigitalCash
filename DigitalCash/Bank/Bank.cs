using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Bank
{
    public partial class Bank : Form
    {
        public Bank()
        {
            InitializeComponent();
        }

        public string connectionString = "Data Source=BEN_T\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";
        public int orderAmount = 0;


        private void CheatedBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            // make a cheated bool to assign whether the customer cheated or not
            bool cheatedBool = false;

            string query = "SELECT [index],[moneyAmount],[serialNumber] FROM [dbo].[MoneyOrder]";

            using SqlCommand cmd = new SqlCommand(query, con);

            using SqlDataReader reader = cmd.ExecuteReader();

            // Make a list of amount and serial numbers to reference against other amounts and serial numbers
            HashSet<string> amountSet = new HashSet<string>();
            HashSet<string> serialNumberSet = new HashSet<string>();

            while (reader.Read())
            {
                int index = reader.GetInt32(0);
                string amountString = reader.GetString(1);
                string serialNumberString = reader.GetString(2);

                // If the serialNumber or amount is contained in one of the lists, show the user cheated. If not, add it to the list for the future
                if (serialNumberSet.Contains(serialNumberString) || amountSet.Contains(amountString))
                {
                    MessageBox.Show("You cheated!");

                    MessageBox.Show(index.ToString(), "Index");

                    cheatedBool= true;
                }
                else
                {
                    amountSet.Add(amountString);
                    serialNumberSet.Add(serialNumberString);
                }
            }
            reader.Close();

            // if no cheating occurred, the bool will be false and will show there was no cheating to give the user a response
            if (!cheatedBool)
            {
                MessageBox.Show("No cheating occurred!");
            }

            con.Close();
        }


        private void RevealBtn_Click(object sender, EventArgs e)
        {
            // Call the RSAEncryption class
            RSAEncryption rsa = new RSAEncryption();

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            // join the two tables of MoneyOrder and ArchivedBlinds on the serialNumber
            string selectQuery = "SELECT * FROM [dbo].[MoneyOrder] JOIN [dbo].[ArchivedBlinds] ON [dbo].[MoneyOrder].[serialNumber] = [dbo].[ArchivedBlinds].[serialNumber]";

            using SqlCommand cmd = new SqlCommand(selectQuery, con);
            using SqlDataReader reader = cmd.ExecuteReader();


            // Choose a random order to be the selection and we wont check that order
            Random rand = new();
            int selection = rand.Next(0, 100);

            int selectedIndex = 0;
            string selectedAmount = null;

            // Make a list of amount and serial numbers to reference against other amounts and serial numbers
            HashSet<string> amountSet = new HashSet<string>();
            HashSet<string> serialNumberSet = new HashSet<string>();

            // if there are no blinds, return a message that informs the user and then return the data from the table
            if (!reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("There are no blinds");
                string queryTwo = "SELECT * FROM [dbo].[MoneyOrder]";

                using SqlCommand cmdTwo = new SqlCommand(queryTwo, con);
                using SqlDataReader readerTwo = cmdTwo.ExecuteReader();

                while (readerTwo.Read())
                {
                    int index = readerTwo.GetInt32(0);
                    string amountString = readerTwo.GetString(1);
                    string serialNumberString = readerTwo.GetString(2);

                    MessageBox.Show(amountString, "amt");
                    MessageBox.Show(serialNumberString, "sn");
                }
                readerTwo.Close();
            }
            else
            {
                // read the data from money order to get money amount and serial
                while (reader.Read())
                {
                    int index = reader.GetInt32(0);
                    string amountString = reader.GetString(1);
                    string serialNumberString = reader.GetString(2);
                    int left = reader.GetInt32(3);
                    int right = reader.GetInt32(4);
                    string blindString = reader.GetString(6);

                    // when the index is the selection
                    if (index == selection)
                    {
                        selectedIndex = index;
                    }
                    else
                    {
                        // The amount should be the same for each one in the lists. The serialNumber will be different for each, but if the amount changes, then the user cheated.
                        amountSet.Add(amountString);
                        serialNumberSet.Add(serialNumberString);
                    }
                }
                reader.Close();
            }
            // The amount should be the same for each one in the lists. The serialNumber will be different for each, but if the amount changes, then the user cheated.
            // Check if all the elements in the set are the same
            if (amountSet.Distinct().Count() == 1)
            {
                // delete previous OrderSelection table elements
                string deleteQuery = "DELETE FROM [dbo].[OrderSelection]";
                SqlCommand comm = new SqlCommand(deleteQuery, con);
                comm.ExecuteNonQuery();

                selectedAmount = amountSet.First();
                orderAmount = Int32.Parse(selectedAmount);

                // read the encrypted money order to send the selection to a new database
                string queryThree = "SELECT * FROM [dbo].[CipherMoneyOrder]";

                using SqlCommand cmdThree = new SqlCommand(queryThree, con);
                using SqlDataReader readerThree = cmdThree.ExecuteReader();

                string amount = "";
                string serialNumber = "";
                string left = "";
                string right = "";

                while (readerThree.Read())
                {
                    int index = readerThree.GetInt32(0);

                    if (index == selectedIndex)
                    {
                        amount = readerThree.GetString(1);
                        serialNumber = readerThree.GetString(2);
                        left = readerThree.GetString(3);
                        right = readerThree.GetString(4);
                    }
                }
                readerThree.Close();

                // Create command for adding to OrderSelection
                string insertQuery = "INSERT INTO [dbo].[OrderSelection]([index],[moneyAmount],[serialNumber],[leftNumber],[rightNumber]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber,@rightNumber)";
                using SqlCommand cmdInsert = new SqlCommand(insertQuery, con);
                {
                    // Add data to OrderSelection
                    cmdInsert.Parameters.AddWithValue("@index", selectedIndex);
                    cmdInsert.Parameters.AddWithValue("@moneyAmount", amount);
                    cmdInsert.Parameters.AddWithValue("@serialNumber", serialNumber);
                    cmdInsert.Parameters.AddWithValue("@leftNumber", left);
                    cmdInsert.Parameters.AddWithValue("@rightNumber", right);
                    cmdInsert.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show("The amounts are not all the same.", "Error");
            }
            if (serialNumberSet.Distinct().Count() != 99)
            {
                MessageBox.Show("There are duplicate serial numbers.", "Error");
            }
            con.Close();
        }


        private void SignBtn_Click(object sender, EventArgs e)
        {
            // Call the RSAEncryption class
            RSAEncryption rsa = new RSAEncryption();

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            // delete previous SignedSelection table elements
            string deleteQuery = "DELETE FROM [dbo].[SignedSelection]";
            SqlCommand comm = new SqlCommand(deleteQuery, con);
            comm.ExecuteNonQuery();

            // get the selection from the table
            string query = "SELECT * FROM [dbo].[OrderSelection]";

            using SqlCommand cmd = new SqlCommand(query, con);
            using SqlDataReader reader = cmd.ExecuteReader();


            int index = 0;
            string amountString = "";
            string serialNumberString = "";
            string left = "";
            string right = "";

            while (reader.Read())
            {
                index = reader.GetInt32(0);
                amountString = reader.GetString(1);
                serialNumberString = reader.GetString(2);
                left = reader.GetString(3);
                right = reader.GetString(4);
            }
            reader.Close();

            // get the private key
            string privatePath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Bank\bin\Debug\net7.0-windows\privatekey.xml";
            rsa.LoadPrivateFromXml(privatePath);

            // convert the data to bigints
            BigInteger amount = rsa.ConvertToBigInt(amountString);
            BigInteger serialNumber = rsa.ConvertToBigInt(serialNumberString);

            // sign the data
            string signedAmount = rsa.Sign(amount);
            string signedSerial = rsa.Sign(serialNumber);

            // put the data into the signed selection table
            string signedQuery = "INSERT INTO [dbo].[SignedSelection]([index],[moneyAmount],[serialNumber],[leftNumber],[rightNumber]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber,@rightNumber)";

            using SqlCommand cmdSigned = new SqlCommand(signedQuery, con);
            {
                // Insert them to MoneyOrder
                cmdSigned.Parameters.AddWithValue("@index", index);
                cmdSigned.Parameters.AddWithValue("@moneyAmount", signedAmount);
                cmdSigned.Parameters.AddWithValue("@serialNumber", signedSerial);
                cmdSigned.Parameters.AddWithValue("@leftNumber", left);
                cmdSigned.Parameters.AddWithValue("@rightNumber", right);

                cmdSigned.ExecuteNonQuery();
            }

            // delete previous OrderSelection table elements
            string deleteTwoQuery = "DELETE FROM [dbo].[OrderSelection]";
            SqlCommand commTwo = new SqlCommand(deleteTwoQuery, con);
            commTwo.ExecuteNonQuery();

            con.Close();
        }


        private void CreateKey()
        {
            // Generate RSA key pair
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(512);

            // Export public key to file
            string publicKeyFile = "publickey.xml";
            File.WriteAllText(publicKeyFile, rsa.ToXmlString(false));

            // Export private key to file
            string privateKeyFile = "privatekey.xml";
            File.WriteAllText(privateKeyFile, rsa.ToXmlString(true));

            // Export public key to separate directory for the bank and merchant
            string bankDirectory = "C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Bank\\bin\\Debug\\net7.0-windows\\";
            string merchantDirectory = "C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Merchant\\bin\\Debug\\net7.0-windows\\";

            File.WriteAllText(Path.Combine(bankDirectory, publicKeyFile), rsa.ToXmlString(false));
            File.WriteAllText(Path.Combine(bankDirectory, privateKeyFile), rsa.ToXmlString(true));
            File.WriteAllText(Path.Combine(merchantDirectory, publicKeyFile), rsa.ToXmlString(false));
        }

        private void NewKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            string bankDirectory = "C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Bank\\bin\\Debug\\net7.0-windows\\";
            string customerDirectory = "C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Customer\\bin\\Debug\\net7.0-windows\\";
            string merchantDirectory = "C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Merchant\\bin\\Debug\\net7.0-windows\\";

            string publicKey = rsa.ToXmlString(false);
            File.WriteAllText(Path.Combine(bankDirectory, publicKey), rsa.ToXmlString(false));
            File.WriteAllText(Path.Combine(customerDirectory, publicKey), rsa.ToXmlString(false));
            File.WriteAllText(Path.Combine(merchantDirectory, publicKey), rsa.ToXmlString(false));

            string privateKey = rsa.ToXmlString(true);
            File.WriteAllText(Path.Combine(bankDirectory, privateKey), rsa.ToXmlString(false));

            //======new key=====
            rsa.PersistKeyInCsp = false;
            rsa.Clear();
            rsa = null;
        }


        public string Username { get; set; }
        public int Balance { get; set; }
        public bool LoggedIn { get; set; }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Login custLogin = new();
            custLogin.ShowDialog(this);
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            usernameLbl.Text = Username;
            balanceAmountLbl.Text = "$" + Balance;
        }

        private void DepositBtn_Click(object sender, EventArgs e)
        {
            Deposit depositBalance = new();
            depositBalance.ShowDialog(this);
            UpdateLabels();
        }

        private void NewCustBtn_Click(object sender, EventArgs e)
        {
            AddCustomer addCust = new();
            addCust.ShowDialog(this);
            UpdateLabels();
        }

        private void GenKeysBtn_Click(object sender, EventArgs e)
        {
            //CreateKey();
            NewKey();
            MessageBox.Show("New key generated.");
        }


    }
}