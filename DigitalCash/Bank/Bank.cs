using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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

            // create a list for serial numbers
            List<string> serialNumbers = new List<string>();
            List<Payment> duplicatePayments = new List<Payment>();

            // get the selection from the table
            string query = "SELECT * FROM [dbo].[ArchivedPayments]";

            using SqlCommand cmd = new SqlCommand(query, con);
            using SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                string serialNumberString = reader.GetString(1);

                serialNumbers.Add(serialNumberString);
            }
            reader.Close();

            var duplicateSerialNumbers = serialNumbers.GroupBy(s => s).Where(g => g.Count() > 1).Select(g => g.Key);

            if (duplicateSerialNumbers == null || !duplicateSerialNumbers.Any())
            {
                MessageBox.Show("No cheating has occurred!", "Alert");
            }
            else
            {
                foreach (var serialNumber in duplicateSerialNumbers)
                {
                    string cheatedQuery = "SELECT * FROM [dbo].[ArchivedPayments] WHERE [serialNumber] = @serialNumber";

                    using SqlCommand cmdCheated = new SqlCommand(cheatedQuery, con);
                    cmdCheated.Parameters.AddWithValue("@serialNumber", serialNumber);
                    using SqlDataReader cheatedReader = cmdCheated.ExecuteReader();

                    while (cheatedReader.Read())
                    {
                        Payment payment = new Payment();

                        payment.Amount = cheatedReader.GetString(0);
                        payment.SerialNumber = cheatedReader.GetString(1);
                        payment.Left1 = cheatedReader.GetString(2);
                        payment.Right1 = cheatedReader.GetString(3);
                        payment.Left2 = cheatedReader.GetString(4);
                        payment.Right2 = cheatedReader.GetString(5);
                        payment.Left3 = cheatedReader.GetString(6);
                        payment.Right3 = cheatedReader.GetString(7);
                        payment.Left4 = cheatedReader.GetString(8);
                        payment.Right4 = cheatedReader.GetString(9);
                        payment.Left5 = cheatedReader.GetString(10);
                        payment.Right5 = cheatedReader.GetString(11);
                        payment.Left6 = cheatedReader.GetString(12);
                        payment.Right6 = cheatedReader.GetString(13);
                        payment.Left7 = cheatedReader.GetString(14);
                        payment.Right7 = cheatedReader.GetString(15);
                        payment.Left8 = cheatedReader.GetString(16);
                        payment.Right8 = cheatedReader.GetString(17);
                        payment.Left9 = cheatedReader.GetString(18);
                        payment.Right9 = cheatedReader.GetString(19);
                        payment.Left10 = cheatedReader.GetString(20);
                        payment.Right10 = cheatedReader.GetString(21);
                        payment.Left11 = cheatedReader.GetString(22);
                        payment.Right11 = cheatedReader.GetString(23);
                        payment.Left12 = cheatedReader.GetString(24);
                        payment.Right12 = cheatedReader.GetString(25);
                        payment.Left13 = cheatedReader.GetString(26);
                        payment.Right13 = cheatedReader.GetString(27);
                        payment.Left14 = cheatedReader.GetString(28);
                        payment.Right14 = cheatedReader.GetString(29);
                        payment.Left15 = cheatedReader.GetString(30);
                        payment.Right15 = cheatedReader.GetString(31);

                        duplicatePayments.Add(payment);
                    }
                    cheatedReader.Close();
                }

                // create a list for the xorResults to cross reference each other
                var xorResults = new List<int>();

                // create bool to check if merchant has cheated
                bool merchantCheated = false;

                for (int i = 1; i <= 15; i++)
                {
                    var leftPropName = "Left" + i;
                    var rightPropName = "Right" + i;

                    var leftValue1 = duplicatePayments[0].GetType().GetProperty(leftPropName).GetValue(duplicatePayments[0]);
                    var leftValue2 = duplicatePayments[1].GetType().GetProperty(leftPropName).GetValue(duplicatePayments[1]);
                    var rightValue1 = duplicatePayments[0].GetType().GetProperty(rightPropName).GetValue(duplicatePayments[0]);
                    var rightValue2 = duplicatePayments[1].GetType().GetProperty(rightPropName).GetValue(duplicatePayments[1]);


                    if (leftValue1.Equals(leftValue2) && rightValue1.Equals(rightValue2) && !merchantCheated)
                    {
                        MessageBox.Show("The merchant cheated!", "Alert!");
                        merchantCheated = true;
                    }


                    if (!leftValue1.Equals(leftValue2) && !rightValue1.Equals(rightValue2))
                    {
                        try
                        {
                            leftValue1 = Convert.ToInt32(leftValue1);
                        }
                        catch (FormatException ex)
                        {
                            // just means its a hash value
                        }
                        try
                        {
                            leftValue2 = Convert.ToInt32(leftValue2);
                        }
                        catch (FormatException ex)
                        {
                            // just means its a hash value
                        }
                        try
                        {
                            rightValue1 = Convert.ToInt32(rightValue1);
                        }
                        catch (FormatException ex)
                        {
                            // just means its a hash value
                        }
                        try
                        {
                            rightValue2 = Convert.ToInt32(rightValue2);
                        }
                        catch (FormatException ex)
                        {
                            // just means its a hash value
                        }


                        try
                        {
                            int xorResult1 = (int)leftValue1 ^ (int)rightValue2;
                            xorResults.Add(xorResult1);
                        }
                        catch (FormatException ex)
                        {
                            // one or more of them is a hash
                        }
                        catch (InvalidCastException exc)
                        {
                            // one or more of them is a hash
                        }

                        try
                        {
                            int xorResult2 = (int)leftValue2 ^ (int)rightValue1;
                            xorResults.Add(xorResult2);
                        }
                        catch (FormatException ex)
                        {
                            // one or more of them is a hash
                        }
                        catch (InvalidCastException exc)
                        {
                            // one or more of them is a hash
                        }
                    }
                }

                // check for duplicates
                var duplicates = xorResults.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key);

                if (duplicates.Any())
                {
                    var maxDuplicate = duplicates.OrderByDescending(x => xorResults.Count(y => y == x)).First();
                    string username = "";

                    string loginQuery = "SELECT [username] FROM LoginCredentials WHERE ID = @ID";

                    SqlCommand loginCmd = new SqlCommand(loginQuery, con);

                    loginCmd.Parameters.AddWithValue("@ID", maxDuplicate);

                    using (SqlDataReader loginReader = loginCmd.ExecuteReader())
                    {
                        while (loginReader.Read())
                        {
                            username = loginReader.GetString(0);
                        }
                    }

                    MessageBox.Show($"The customer who cheated is {username}", "Alert!");
                }
                else
                {
                    MessageBox.Show("No duplicates found", "Alert!");
                }

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
            List<string> amountSet = new List<string>();
            List<string> serialNumberSet = new List<string>();

            // if there are no blinds, return a message that informs the user and then return the data from the table
            if (!reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("There are no blinds");
            }
            else
            {
                // read the data from money order to get money amount and serial
                while (reader.Read())
                {
                    int index = reader.GetInt32(0);
                    string amountString = reader.GetString(1);
                    string serialNumberString = reader.GetString(2);
                    int left1 = reader.GetInt32(3);
                    int right1 = reader.GetInt32(4);
                    int left2 = reader.GetInt32(5);
                    int right2 = reader.GetInt32(6);
                    int left3 = reader.GetInt32(7);
                    int right3 = reader.GetInt32(8);
                    int left4 = reader.GetInt32(9);
                    int right4 = reader.GetInt32(10);
                    int left5 = reader.GetInt32(11);
                    int right5 = reader.GetInt32(12);
                    int left6 = reader.GetInt32(13);
                    int right6 = reader.GetInt32(14);
                    int left7 = reader.GetInt32(15);
                    int right7 = reader.GetInt32(16);
                    int left8 = reader.GetInt32(17);
                    int right8 = reader.GetInt32(18);
                    int left9 = reader.GetInt32(19);
                    int right9 = reader.GetInt32(20);
                    int left10 = reader.GetInt32(21);
                    int right10 = reader.GetInt32(22);
                    int left11 = reader.GetInt32(23);
                    int right11 = reader.GetInt32(24);
                    int left12 = reader.GetInt32(25);
                    int right12 = reader.GetInt32(26);
                    int left13 = reader.GetInt32(27);
                    int right13 = reader.GetInt32(28);
                    int left14 = reader.GetInt32(29);
                    int right14 = reader.GetInt32(30);
                    int left15 = reader.GetInt32(31);
                    int right15 = reader.GetInt32(32);
                    string blindString = reader.GetString(35);

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
                string left1 = "";
                string right1 = "";
                string left2 = "";
                string right2 = "";
                string left3 = "";
                string right3 = "";
                string left4 = "";
                string right4 = "";
                string left5 = "";
                string right5 = "";
                string left6 = "";
                string right6 = "";
                string left7 = "";
                string right7 = "";
                string left8 = "";
                string right8 = "";
                string left9 = "";
                string right9 = "";
                string left10 = "";
                string right10 = "";
                string left11 = "";
                string right11 = "";
                string left12 = "";
                string right12 = "";
                string left13 = "";
                string right13 = "";
                string left14 = "";
                string right14 = "";
                string left15 = "";
                string right15 = "";

                while (readerThree.Read())
                {
                    int index = readerThree.GetInt32(0);

                    if (index == selectedIndex)
                    {
                        amount = readerThree.GetString(1);
                        serialNumber = readerThree.GetString(2);
                        left1 = readerThree.GetString(3);
                        right1 = readerThree.GetString(4);
                        left2 = readerThree.GetString(5);
                        right2 = readerThree.GetString(6);
                        left3 = readerThree.GetString(7);
                        right3 = readerThree.GetString(8);
                        left4 = readerThree.GetString(9);
                        right4 = readerThree.GetString(10);
                        left5 = readerThree.GetString(11);
                        right5 = readerThree.GetString(12);
                        left6 = readerThree.GetString(13);
                        right6 = readerThree.GetString(14);
                        left7 = readerThree.GetString(15);
                        right7 = readerThree.GetString(16);
                        left8 = readerThree.GetString(17);
                        right8 = readerThree.GetString(18);
                        left9 = readerThree.GetString(19);
                        right9 = readerThree.GetString(20);
                        left10 = readerThree.GetString(21);
                        right10 = readerThree.GetString(22);
                        left11 = readerThree.GetString(23);
                        right11 = readerThree.GetString(24);
                        left12 = readerThree.GetString(25);
                        right12 = readerThree.GetString(26);
                        left13 = readerThree.GetString(27);
                        right13 = readerThree.GetString(28);
                        left14 = readerThree.GetString(29);
                        right14 = readerThree.GetString(30);
                        left15 = readerThree.GetString(31);
                        right15 = readerThree.GetString(32);
                    }
                }
                readerThree.Close();

                // Create command for adding to OrderSelection
                string insertQuery = "INSERT INTO [dbo].[OrderSelection]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
                using SqlCommand cmdInsert = new SqlCommand(insertQuery, con);
                {
                    // Add data to OrderSelection
                    cmdInsert.Parameters.AddWithValue("@index", selectedIndex);
                    cmdInsert.Parameters.AddWithValue("@moneyAmount", amount);
                    cmdInsert.Parameters.AddWithValue("@serialNumber", serialNumber);
                    cmdInsert.Parameters.AddWithValue("@leftNumber1", left1);
                    cmdInsert.Parameters.AddWithValue("@rightNumber1", right1);
                    cmdInsert.Parameters.AddWithValue("@leftNumber2", left2);
                    cmdInsert.Parameters.AddWithValue("@rightNumber2", right2);
                    cmdInsert.Parameters.AddWithValue("@leftNumber3", left3);
                    cmdInsert.Parameters.AddWithValue("@rightNumber3", right3);
                    cmdInsert.Parameters.AddWithValue("@leftNumber4", left4);
                    cmdInsert.Parameters.AddWithValue("@rightNumber4", right4);
                    cmdInsert.Parameters.AddWithValue("@leftNumber5", left5);
                    cmdInsert.Parameters.AddWithValue("@rightNumber5", right5);
                    cmdInsert.Parameters.AddWithValue("@leftNumber6", left6);
                    cmdInsert.Parameters.AddWithValue("@rightNumber6", right6);
                    cmdInsert.Parameters.AddWithValue("@leftNumber7", left7);
                    cmdInsert.Parameters.AddWithValue("@rightNumber7", right7);
                    cmdInsert.Parameters.AddWithValue("@leftNumber8", left8);
                    cmdInsert.Parameters.AddWithValue("@rightNumber8", right8);
                    cmdInsert.Parameters.AddWithValue("@leftNumber9", left9);
                    cmdInsert.Parameters.AddWithValue("@rightNumber9", right9);
                    cmdInsert.Parameters.AddWithValue("@leftNumber10", left10);
                    cmdInsert.Parameters.AddWithValue("@rightNumber10", right10);
                    cmdInsert.Parameters.AddWithValue("@leftNumber11", left11);
                    cmdInsert.Parameters.AddWithValue("@rightNumber11", right11);
                    cmdInsert.Parameters.AddWithValue("@leftNumber12", left12);
                    cmdInsert.Parameters.AddWithValue("@rightNumber12", right12);
                    cmdInsert.Parameters.AddWithValue("@leftNumber13", left13);
                    cmdInsert.Parameters.AddWithValue("@rightNumber13", right13);
                    cmdInsert.Parameters.AddWithValue("@leftNumber14", left14);
                    cmdInsert.Parameters.AddWithValue("@rightNumber14", right14);
                    cmdInsert.Parameters.AddWithValue("@leftNumber15", left15);
                    cmdInsert.Parameters.AddWithValue("@rightNumber15", right15);
                    cmdInsert.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show("The amounts are not all the same. You cheated.", "Error");
            }
            if (serialNumberSet.Distinct().Count() != 99)
            {
                MessageBox.Show("There are duplicate serial numbers. You cheated.", "Error");
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
            string left1 = "";
            string right1 = "";
            string left2 = "";
            string right2 = "";
            string left3 = "";
            string right3 = "";
            string left4 = "";
            string right4 = "";
            string left5 = "";
            string right5 = "";
            string left6 = "";
            string right6 = "";
            string left7 = "";
            string right7 = "";
            string left8 = "";
            string right8 = "";
            string left9 = "";
            string right9 = "";
            string left10 = "";
            string right10 = "";
            string left11 = "";
            string right11 = "";
            string left12 = "";
            string right12 = "";
            string left13 = "";
            string right13 = "";
            string left14 = "";
            string right14 = "";
            string left15 = "";
            string right15 = "";

            while (reader.Read())
            {
                index = reader.GetInt32(0);
                amountString = reader.GetString(1);
                serialNumberString = reader.GetString(2);
                left1 = reader.GetString(3);
                right1 = reader.GetString(4);
                left2 = reader.GetString(5);
                right2 = reader.GetString(6);
                left3 = reader.GetString(7);
                right3 = reader.GetString(8);
                left4 = reader.GetString(9);
                right4 = reader.GetString(10);
                left5 = reader.GetString(11);
                right5 = reader.GetString(12);
                left6 = reader.GetString(13);
                right6 = reader.GetString(14);
                left7 = reader.GetString(15);
                right7 = reader.GetString(16);
                left8 = reader.GetString(17);
                right8 = reader.GetString(18);
                left9 = reader.GetString(19);
                right9 = reader.GetString(20);
                left10 = reader.GetString(21);
                right10 = reader.GetString(22);
                left11 = reader.GetString(23);
                right11 = reader.GetString(24);
                left12 = reader.GetString(25);
                right12 = reader.GetString(26);
                left13 = reader.GetString(27);
                right13 = reader.GetString(28);
                left14 = reader.GetString(29);
                right14 = reader.GetString(30);
                left15 = reader.GetString(31);
                right15 = reader.GetString(32);
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
            string signedQuery = "INSERT INTO [dbo].[SignedSelection]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";

            using SqlCommand cmdSigned = new SqlCommand(signedQuery, con);
            {
                // Insert them to MoneyOrder
                cmdSigned.Parameters.AddWithValue("@index", index);
                cmdSigned.Parameters.AddWithValue("@moneyAmount", signedAmount);
                cmdSigned.Parameters.AddWithValue("@serialNumber", signedSerial);
                cmdSigned.Parameters.AddWithValue("@leftNumber1", left1);
                cmdSigned.Parameters.AddWithValue("@rightNumber1", right1);
                cmdSigned.Parameters.AddWithValue("@leftNumber2", left2);
                cmdSigned.Parameters.AddWithValue("@rightNumber2", right2);
                cmdSigned.Parameters.AddWithValue("@leftNumber3", left3);
                cmdSigned.Parameters.AddWithValue("@rightNumber3", right3);
                cmdSigned.Parameters.AddWithValue("@leftNumber4", left4);
                cmdSigned.Parameters.AddWithValue("@rightNumber4", right4);
                cmdSigned.Parameters.AddWithValue("@leftNumber5", left5);
                cmdSigned.Parameters.AddWithValue("@rightNumber5", right5);
                cmdSigned.Parameters.AddWithValue("@leftNumber6", left6);
                cmdSigned.Parameters.AddWithValue("@rightNumber6", right6);
                cmdSigned.Parameters.AddWithValue("@leftNumber7", left7);
                cmdSigned.Parameters.AddWithValue("@rightNumber7", right7);
                cmdSigned.Parameters.AddWithValue("@leftNumber8", left8);
                cmdSigned.Parameters.AddWithValue("@rightNumber8", right8);
                cmdSigned.Parameters.AddWithValue("@leftNumber9", left9);
                cmdSigned.Parameters.AddWithValue("@rightNumber9", right9);
                cmdSigned.Parameters.AddWithValue("@leftNumber10", left10);
                cmdSigned.Parameters.AddWithValue("@rightNumber10", right10);
                cmdSigned.Parameters.AddWithValue("@leftNumber11", left11);
                cmdSigned.Parameters.AddWithValue("@rightNumber11", right11);
                cmdSigned.Parameters.AddWithValue("@leftNumber12", left12);
                cmdSigned.Parameters.AddWithValue("@rightNumber12", right12);
                cmdSigned.Parameters.AddWithValue("@leftNumber13", left13);
                cmdSigned.Parameters.AddWithValue("@rightNumber13", right13);
                cmdSigned.Parameters.AddWithValue("@leftNumber14", left14);
                cmdSigned.Parameters.AddWithValue("@rightNumber14", right14);
                cmdSigned.Parameters.AddWithValue("@leftNumber15", left15);
                cmdSigned.Parameters.AddWithValue("@rightNumber15", right15);

                cmdSigned.ExecuteNonQuery();
            }

            // delete previous OrderSelection table elements
            string deleteTwoQuery = "DELETE FROM [dbo].[OrderSelection]";
            SqlCommand commTwo = new SqlCommand(deleteTwoQuery, con);
            commTwo.ExecuteNonQuery();

            con.Close();
        }

        private void NewKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            string bankDirectory = "C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Bank\\bin\\Debug\\net7.0-windows\\";
            string customerDirectory = "C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Customer\\bin\\Debug\\net7.0-windows\\";
            string merchantDirectory = "C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Merchant\\bin\\Debug\\net7.0-windows\\";

            string publicKeyFile = "publickey.xml";
            string publicKey = rsa.ToXmlString(false);
            File.WriteAllText(Path.Combine(bankDirectory, publicKeyFile), publicKey);
            File.WriteAllText(Path.Combine(customerDirectory, publicKeyFile), publicKey);
            File.WriteAllText(Path.Combine(merchantDirectory, publicKeyFile), publicKey);

            string privateKeyFile = "privatekey.xml";
            string privateKey = rsa.ToXmlString(true);
            File.WriteAllText(Path.Combine(bankDirectory, privateKeyFile), privateKey);

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
            if (LoggedIn)
            {
                if (Username == "Admin")
                {
                    NewKey();
                    MessageBox.Show("New key pair generated.");
                }
                else
                {
                    MessageBox.Show("Unauthorized access.", "Error");
                }
            }
            else
            {
                MessageBox.Show("Please log in.", "Error");
            }
        }
    }
}