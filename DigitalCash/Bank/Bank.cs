using Customer;
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


        private void UnblindBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            // join the two tables of MoneyOrder and ArchivedBlinds on the serialNumber
            string selectQuery = "SELECT * FROM [dbo].[MoneyOrder] JOIN [dbo].[ArchivedBlinds] ON [dbo].[MoneyOrder].[serialNumber] = [dbo].[ArchivedBlinds].[serialNumber]";

            using SqlCommand cmd = new SqlCommand(selectQuery, con);
            using SqlDataReader reader = cmd.ExecuteReader();

            // Call the RSAEncryption class and load the key
            RSAEncryption rsaEnc = new RSAEncryption();

            // Choose a random order to be the selection and we wont check that order
            Random rand = new();
            int selection = rand.Next(0, 100);
            MessageBox.Show(selection.ToString(), "Selection");

            int selectedIndex = 0;
            string selectedSerialNum = null;
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

                    MessageBox.Show(index.ToString());
                    MessageBox.Show(amountString.ToString(), "Amount");
                    MessageBox.Show(serialNumberString.ToString(), "Serial Number");
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
                    string blindString = reader.GetString(5);

                    // when the index is the selection
                    if (index == selection)
                    {
                        selectedIndex = index;
                        selectedSerialNum = serialNumberString;
                    }
                    else
                    {
                        // convert the blind to a bigint and set it as the blind factor in the rsaencryption class
                        BigInteger blindNum = BigInteger.Parse(blindString);
                        rsaEnc.setBlindFactor(blindNum);

                        // decrypt and unblind the data
                        string privateKey = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Bank\bin\Debug\net7.0-windows\privatekey.xml";
                        rsaEnc.LoadPrivateFromXml(privateKey);

                        // convert data to biginteger
                        BigInteger blindAmount = BigInteger.Parse(amountString);
                        BigInteger blindSerial = BigInteger.Parse(serialNumberString);

                        MessageBox.Show(blindString, "Blind String");
                        MessageBox.Show(rsaEnc.retrieveBlindFactor(), "Blind Function");
                        MessageBox.Show(blindAmount.ToString(), "BigInt Amount");
                        MessageBox.Show(blindSerial.ToString(), "BigInt Serial Number");

                        string unblindAmount = rsaEnc.Unblind(blindAmount);
                        string unblindSerial = rsaEnc.Unblind(blindSerial);

                        BigInteger amount = BigInteger.Parse(unblindAmount);
                        BigInteger serialNum = BigInteger.Parse(unblindSerial);

                        MessageBox.Show(amount.ToString(), "Unblind BigInt Amount");
                        MessageBox.Show(serialNum.ToString(), "Unblind BigInt Serial Number");

                        // The amount should be the same for each one in the lists. The serialNumber will be different for each, but if the amount changes, then the user cheated.
                        amountSet.Add(amount.ToString());
                        serialNumberSet.Add(serialNum.ToString());
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

                // Create command for adding to OrderSelection
                string insertQuery = "INSERT INTO [dbo].[OrderSelection]([index],[moneyAmount],[serialNumber]) VALUES(@selectedIndex,@selectedAmount,@selectedSerialNum)";
                SqlCommand cmdInsert = new SqlCommand(insertQuery, con);

                // Add data to OrderSelection
                cmdInsert.Parameters.AddWithValue("@index", selectedIndex);
                cmdInsert.Parameters.AddWithValue("@moneyAmount", selectedAmount);
                cmdInsert.Parameters.AddWithValue("@serialNumber", selectedSerialNum);
                cmdInsert.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("The amounts are not all the same", "Error");
            }

        }


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

        private void SignBtn_Click(object sender, EventArgs e)
        {
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

            // Call the RSAEncryption class
            RSAEncryption rsaEnc = new RSAEncryption();

            int index;
            string amountString, serialNumberString, moneyAmount;

            while (reader.Read())
            {
                index = reader.GetInt32(0);
                moneyAmount = reader.GetString(1);
                amountString = reader.GetString(2);
                serialNumberString = reader.GetString(3);

                // convert the data to bigints
                BigInteger amount = BigInteger.Parse(amountString);
                BigInteger serialNumber = BigInteger.Parse(serialNumberString);

                // sign the data
                string signedAmount = rsaEnc.Sign(amount);
                string signedSerial = rsaEnc.Sign(serialNumber);

                // put the data into the signed selection table
                string signedQuery = "INSERT INTO [dbo].[SignedSelection]([index],[moneyAmount],[encryptedAmount],[serialNumber]) VALUES(@index,@moneyAmount,@encryptedAmount,@serialNumber) ORDER BY [index]";

                SqlCommand cmdSigned = new SqlCommand(signedQuery, con);

                // Insert them to MoneyOrder
                cmdSigned.Parameters.AddWithValue("@index", index);
                cmdSigned.Parameters.AddWithValue("@moneyAmount", moneyAmount);
                cmdSigned.Parameters.AddWithValue("@encryptedAmount", signedAmount);
                cmdSigned.Parameters.AddWithValue("@serialNumber", signedSerial);

                cmdSigned.ExecuteNonQuery();

            }
            reader.Close();

            // delete previous OrderSelection table elements
            string deleteTwoQuery = "DELETE FROM [dbo].[OrderSelection]";
            SqlCommand commTwo = new SqlCommand(deleteTwoQuery, con);
            commTwo.ExecuteNonQuery();

            con.Close();
        }

        private void DecryptBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "SELECT [index],[moneyAmount],[serialNumber] FROM [dbo].[MoneyOrder]";

            using SqlCommand cmd = new SqlCommand(query, con);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int index = reader.GetInt32(0);
                string amountString = reader.GetString(1);
                string serialNumberString = reader.GetString(2);

                MessageBox.Show(amountString);
                // Read the XML string of the private and public key from a file
                string privateKeyXml = File.ReadAllText("C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Customer\\bin\\Debug\\net7.0-windows\\privatekey.xml", Encoding.UTF8);
                string publicKeyXml = File.ReadAllText("C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Customer\\bin\\Debug\\net7.0-windows\\publickey.xml", Encoding.UTF8);

                // Load the private key from the XML string
                RSACryptoServiceProvider rsaPrivate = new RSACryptoServiceProvider();
                rsaPrivate.FromXmlString(privateKeyXml);

                // Load the public key from the XML string
                RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider();
                rsaPublic.FromXmlString(publicKeyXml);

                // Convert to byte
                byte[] encryptedAmountBytes = Enumerable.Range(0, amountString.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(amountString.Substring(x, 2), 16)).ToArray();
                byte[] encryptedSerialBytes = Enumerable.Range(0, serialNumberString.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(serialNumberString.Substring(x, 2), 16)).ToArray();

                byte[] decryptedAmountBytes = rsaPublic.Decrypt(encryptedAmountBytes, false);
                byte[] decryptedSerialBytes = rsaPublic.Decrypt(encryptedSerialBytes, false);

                string plainAmount = Convert.ToHexString(decryptedAmountBytes);
                string plainSerial = Convert.ToHexString(decryptedSerialBytes);

                MessageBox.Show(plainAmount, "Amount");
                MessageBox.Show(plainSerial, "Serial");

            }
            reader.Close();
            con.Close();
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            RSACryptography rsa = new RSACryptography();

            string query = "SELECT [index],[moneyAmount],[serialNumber] FROM [dbo].[MoneyOrder]";

            using SqlCommand cmd = new SqlCommand(query, con);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int index = reader.GetInt32(0);
                string amountString = reader.GetString(1);
                string serialNumberString = reader.GetString(2);

                MessageBox.Show(amountString);

                string privateKey = rsa.GetPrivateKey();

                string decryptedAmount = rsa.Decrypt(amountString, privateKey);
                string decryptedSerial = rsa.Decrypt(serialNumberString, privateKey);

                MessageBox.Show(decryptedAmount, "Amount");
                MessageBox.Show(decryptedSerial, "Serial");
            }
            reader.Close();
            con.Close();
        }
        private void TestTwoBtn_Click(object sender, EventArgs e)
        {
            string encrypted = "BRaPvMhN7knabf+RrF296q2PW9aVYEM2hDDcahd1j0VHK2o+Wr8Mh/sq8esspYAJy/eagOtH9UFFqlarAs8C4BCGnCbqo9WFpoo58MNvwsUmOFvvvwqV+SW0A5VidObr6ow4xQaJf/3+q6XATvc9W/2FuZq9Ct12vJyuYC+ttDM=";
            RSACryptography rsa = new RSACryptography();

            string privateKey = rsa.GetPrivateKey();

            string decrypted = rsa.Decrypt(encrypted, privateKey);

            MessageBox.Show(decrypted, "decrypted");
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


    }
}