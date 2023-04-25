using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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

        public string connectionString = "Data Source=LAPTOP-UOPDFGH4\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";
        public int OrderAmount { get; set; }
        public string Username { get; set; }
        public int Balance { get; set; }
        public bool LoggedIn { get; set; }
        public int ID { get; set; }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Login custLogin = new();
            custLogin.ShowDialog(this);
            UpdateLabels();
            updateBox.AppendText($"Logged in as {Username}\n");
        }

        private void UpdateLabels()
        {
            usernameLbl.Text = Username;
            balanceAmountLbl.Text = "$" + Balance;
        }

        private void DepositBtn_Click(object sender, EventArgs e)
        {
            int current = Balance;

            Deposit depositBalance = new();
            depositBalance.ShowDialog(this);
            UpdateLabels();

            updateBox.AppendText($"${Balance - current} was deposited. Balance updated to ${Balance}\n");
        }

        private void NewCustBtn_Click(object sender, EventArgs e)
        {
            AddCustomer addCust = new();
            addCust.ShowDialog(this);
            UpdateLabels();

            updateBox.AppendText($"{Username} added as a new customer");
        }

        private void GenKeysBtn_Click(object sender, EventArgs e)
        {
            if (LoggedIn)
            {
                if (Username == "Admin")
                {
                    NewKey();
                    updateBox.AppendText("New key pair generated.");
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
                updateBox.AppendText("No cheating has occurred!\n");
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

                // create list for merchant check
                var leftList = new List<string>();
                var rightList = new List<string>();

                for (int i = 1; i <= 15; i++)
                {
                    var leftPropName = "Left" + i;
                    var rightPropName = "Right" + i;

                    var leftValue1 = duplicatePayments[0].GetType().GetProperty(leftPropName).GetValue(duplicatePayments[0]);
                    var leftValue2 = duplicatePayments[1].GetType().GetProperty(leftPropName).GetValue(duplicatePayments[1]);
                    var rightValue1 = duplicatePayments[0].GetType().GetProperty(rightPropName).GetValue(duplicatePayments[0]);
                    var rightValue2 = duplicatePayments[1].GetType().GetProperty(rightPropName).GetValue(duplicatePayments[1]);

                    leftList.Add(leftValue1.ToString());
                    leftList.Add(leftValue2.ToString());
                    rightList.Add(rightValue1.ToString());
                    rightList.Add(rightValue2.ToString());


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
                        catch (OverflowException ex)
                        {
                            // just means the value is really large... so a hash value
                        }
                        try
                        {
                            leftValue2 = Convert.ToInt32(leftValue2);
                        }
                        catch (FormatException ex)
                        {
                            // just means its a hash value
                        }
                        catch (OverflowException ex)
                        {
                            // just means the value is really large... so a hash value
                        }
                        try
                        {
                            rightValue1 = Convert.ToInt32(rightValue1);
                        }
                        catch (FormatException ex)
                        {
                            // just means its a hash value
                        }
                        catch (OverflowException ex)
                        {
                            // just means the value is really large... so a hash value
                        }
                        try
                        {
                            rightValue2 = Convert.ToInt32(rightValue2);
                        }
                        catch (FormatException ex)
                        {
                            // just means its a hash value
                        }
                        catch (OverflowException ex)
                        {
                            // just means the value is really large... so a hash value
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
                        catch (OverflowException ex)
                        {
                            // just means the value is really large... so a hash value
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
                        catch (OverflowException ex)
                        {
                            // just means the value is really large... so a hash value
                        }
                        catch (InvalidCastException exc)
                        {
                            // one or more of them is a hash
                        }
                    }
                }

                if ((leftList.Distinct().Count() == 15) && (rightList.Distinct().Count() == 15) && !merchantCheated)
                {
                    updateBox.AppendText("The merchant cheated!\n");
                    merchantCheated = true;
                }
                else
                {
                    updateBox.AppendText("The merchant did not cheat.\n");
                }

                // check for duplicates
                var duplicates = xorResults.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key);

                if (duplicates.Any())
                {
                    var topDuplicate = duplicates.OrderByDescending(x => xorResults.Count(y => y == x)).First();
                    string username = "";

                    string loginQuery = "SELECT [username] FROM LoginCredentials WHERE ID = @ID";

                    SqlCommand loginCmd = new SqlCommand(loginQuery, con);

                    loginCmd.Parameters.AddWithValue("@ID", topDuplicate);

                    using (SqlDataReader loginReader = loginCmd.ExecuteReader())
                    {
                        while (loginReader.Read())
                        {
                            username = loginReader.GetString(0);
                        }
                    }

                    updateBox.AppendText($"The customer who cheated is {username}\n");
                }
                else
                {
                    updateBox.AppendText("The customer did not cheat.\n");
                }
                
                // delete the cheated orders from ArchivedPayments table elements
                string delQuery = "DELETE TOP(1) FROM [dbo].[ArchivedPayments] WHERE [serialNumber] = @serialNumber";
                SqlCommand comm2 = new SqlCommand(delQuery, con);
                comm2.Parameters.AddWithValue("@serialNumber", duplicatePayments[0].SerialNumber);
                comm2.ExecuteNonQuery();

            }
            con.Close();
        }


        private void RevealBtn_Click(object sender, EventArgs e)
        {
            if (LoggedIn)
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

                // Make a list of amount, serial numbers, and left and right values to reference against other amounts and serial numbers
                List<string> amountSet = new List<string>();
                List<string> serialNumberSet = new List<string>();
                List<int> xorSet = new List<int>();

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

                            // The left and rights should all XOR to the same value AND that value needs to be the user's ID
                            // add the left and right XOR results to a list
                            xorSet.AddRange(new int[] { (left1 ^ right1), (left2 ^ right2), (left3 ^ right3), (left4 ^ right4), (left5 ^ right5), (left6 ^ right6), (left7 ^ right7), (left8 ^ right8), (left9 ^ right9), (left10 ^ right10), (left11 ^ right11), (left12 ^ right12), (left13 ^ right13), (left14 ^ right14), (left15 ^ right15) });
                        }
                    }
                    reader.Close();
                }

                // The amount should be the same for each one in the lists. The serialNumber will be different for each, but if the amount changes, then the user cheated.
                // Check if all the elements in the set are different
                if (serialNumberSet.Any(x => string.IsNullOrEmpty(x) || x == "0") || amountSet.Any(x => string.IsNullOrEmpty(x) || x == "0"))
                {
                    updateBox.AppendText("There was an error with populating the database table. Please restart the program\n");
                    MessageBox.Show("Please restart the program", "Error");
                }
                else if (serialNumberSet.Distinct().Count() != 99)
                {
                    updateBox.AppendText("There are duplicate serial numbers. You cheated.\n");
                }
                // Check if all the elements in the set are the same
                else if (amountSet.Distinct().Count() != 1)
                {
                    updateBox.AppendText("The amounts are not all the same. You cheated.\n");
                }
                else if (xorSet.Distinct().Count() != 1)
                {
                    updateBox.AppendText("The left and right values do not all return the same ID. You cheated.\n");
                }
                else if (xorSet[0] != ID)
                {
                    updateBox.AppendText("The ID revealed is not your ID. You cheated.\n");
                }
                else if (IDCheated(selection))
                {
                    updateBox.AppendText("The revealed left and rights do not match the hashed left and rights. You cheated.\n");
                }
                else
                {
                    // delete previous OrderSelection table elements
                    string deleteQuery = "DELETE FROM [dbo].[OrderSelection]";
                    SqlCommand comm = new SqlCommand(deleteQuery, con);
                    comm.ExecuteNonQuery();

                    selectedAmount = amountSet.First();
                    OrderAmount = Int32.Parse(selectedAmount);

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

                    updateBox.AppendText("No fraud has been committed. The order has been selected\n");
                    updateBox.AppendText($"The selected money order is {selectedIndex}\n");
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Please log in.", "Error");
            }
        }


        private void SignBtn_Click(object sender, EventArgs e)
        {
            if (LoggedIn)
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



                Balance = Balance - OrderAmount;
                // update the logincredentials table to subtract the money order from balance
                string balanceQuery = "UPDATE[dbo].[LoginCredentials] SET[balance] = @balance WHERE[ID] = @ID";
                SqlCommand balanceCmd = new SqlCommand(balanceQuery, con);

                balanceCmd.Parameters.AddWithValue("@balance", Balance);
                balanceCmd.Parameters.AddWithValue("@ID", ID);

                balanceCmd.ExecuteNonQuery();
                UpdateLabels();


                con.Close();

                updateBox.AppendText("The money order has been signed.\n");
                updateBox.AppendText($"${OrderAmount} was deducted from the account\n");
            }
            else
            {
                MessageBox.Show("Please log in.", "Error");
            }
        }


        private void NewKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            string bankDirectory = "C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Bank\\bin\\Debug\\net7.0-windows";
            string customerDirectory = "C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Customer\\bin\\Debug\\net7.0-windows";
            string merchantDirectory = "C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Merchant\\bin\\Debug\\net7.0-windows";

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


        private bool IDCheated(int selection)
        {
            // this is our bool we return true if hashes match and false if they dont
            bool cheated = false;

            // Call the RSAEncryption class
            RSAEncryption rsa = new RSAEncryption();

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            // join the two tables of MoneyOrder and ArchivedBlinds on the serialNumber
            string selectQuery = "SELECT * FROM [dbo].[MoneyOrder] JOIN [dbo].[HashMoneyOrder] ON [dbo].[MoneyOrder].[serialNumber] = [dbo].[HashMoneyOrder].[serialNumber]";

            using SqlCommand cmd = new SqlCommand(selectQuery, con);
            using SqlDataReader reader = cmd.ExecuteReader();


            // Choose a random order to be the selection and we wont check that order
            Random rand = new();

            int selectedIndex = 0;

            // if there are no blinds, return a message that informs the user and then return the data from the table
            if (!reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("The tables did not join correctly");
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
                    string tempLeft1 = reader.GetString(36);
                    string tempRight1 = reader.GetString(37);
                    string tempLeft2 = reader.GetString(38);
                    string tempRight2 = reader.GetString(39);
                    string tempLeft3 = reader.GetString(40);
                    string tempRight3 = reader.GetString(41);
                    string tempLeft4 = reader.GetString(42);
                    string tempRight4 = reader.GetString(43);
                    string tempLeft5 = reader.GetString(44);
                    string tempRight5 = reader.GetString(45);
                    string tempLeft6 = reader.GetString(46);
                    string tempRight6 = reader.GetString(47);
                    string tempLeft7 = reader.GetString(48);
                    string tempRight7 = reader.GetString(49);
                    string tempLeft8 = reader.GetString(50);
                    string tempRight8 = reader.GetString(51);
                    string tempLeft9 = reader.GetString(52);
                    string tempRight9 = reader.GetString(53);
                    string tempLeft10 = reader.GetString(54);
                    string tempRight10 = reader.GetString(55);
                    string tempLeft11 = reader.GetString(56);
                    string tempRight11 = reader.GetString(57);
                    string tempLeft12 = reader.GetString(58);
                    string tempRight12 = reader.GetString(59);
                    string tempLeft13 = reader.GetString(60);
                    string tempRight13 = reader.GetString(61);
                    string tempLeft14 = reader.GetString(62);
                    string tempRight14 = reader.GetString(63);
                    string tempLeft15 = reader.GetString(64);
                    string tempRight15 = reader.GetString(65);

                    // when the index is the selection
                    if (index == selection)
                    {
                        selectedIndex = index;
                    }
                    else
                    {
                        // hash the left and right numbers
                        SHA256 sha256 = SHA256.Create();
                        string leftHash1 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left1.ToString()))));
                        string rightHash1 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right1.ToString()))));
                        string leftHash2 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left2.ToString()))));
                        string rightHash2 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right2.ToString()))));
                        string leftHash3 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left3.ToString()))));
                        string rightHash3 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right3.ToString()))));
                        string leftHash4 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left4.ToString()))));
                        string rightHash4 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right4.ToString()))));
                        string leftHash5 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left5.ToString()))));
                        string rightHash5 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right5.ToString()))));
                        string leftHash6 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left6.ToString()))));
                        string rightHash6 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right6.ToString()))));
                        string leftHash7 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left7.ToString()))));
                        string rightHash7 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right7.ToString()))));
                        string leftHash8 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left8.ToString()))));
                        string rightHash8 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right8.ToString()))));
                        string leftHash9 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left9.ToString()))));
                        string rightHash9 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right9.ToString()))));
                        string leftHash10 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left10.ToString()))));
                        string rightHash10 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right10.ToString()))));
                        string leftHash11 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left11.ToString()))));
                        string rightHash11 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right11.ToString()))));
                        string leftHash12 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left12.ToString()))));
                        string rightHash12 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right12.ToString()))));
                        string leftHash13 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left13.ToString()))));
                        string rightHash13 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right13.ToString()))));
                        string leftHash14 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left14.ToString()))));
                        string rightHash14 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right14.ToString()))));
                        string leftHash15 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(left15.ToString()))));
                        string rightHash15 = (Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(right15.ToString()))));


                        for (int i = 1; i <= 15; i++)
                        {
                            string checkleft1 = "";
                            string checkright1 = "";
                            string checkleft2 = "";
                            string checkright2 = "";

                            switch (i)
                            {
                                case 1:
                                    checkleft1 = tempLeft1;
                                    checkright1 = tempRight1;
                                    checkleft2 = leftHash1;
                                    checkright2 = rightHash1;
                                    break;
                                case 2:
                                    checkleft1 = tempLeft2;
                                    checkright1 = tempRight2;
                                    checkleft2 = leftHash2;
                                    checkright2 = rightHash2;
                                    break;
                                case 3:
                                    checkleft1 = tempLeft3;
                                    checkright1 = tempRight3;
                                    checkleft2 = leftHash3;
                                    checkright2 = rightHash3;
                                    break;
                                case 4:
                                    checkleft1 = tempLeft4;
                                    checkright1 = tempRight4;
                                    checkleft2 = leftHash4;
                                    checkright2 = rightHash4;
                                    break;
                                case 5:
                                    checkleft1 = tempLeft5;
                                    checkright1 = tempRight5;
                                    checkleft2 = leftHash5;
                                    checkright2 = rightHash5;
                                    break;
                                case 6:
                                    checkleft1 = tempLeft6;
                                    checkright1 = tempRight6;
                                    checkleft2 = leftHash6;
                                    checkright2 = rightHash6;
                                    break;
                                case 7:
                                    checkleft1 = tempLeft7;
                                    checkright1 = tempRight7;
                                    checkleft2 = leftHash7;
                                    checkright2 = rightHash7;
                                    break;
                                case 8:
                                    checkleft1 = tempLeft8;
                                    checkright1 = tempRight8;
                                    checkleft2 = leftHash8;
                                    checkright2 = rightHash8;
                                    break;
                                case 9:
                                    checkleft1 = tempLeft9;
                                    checkright1 = tempRight9;
                                    checkleft2 = leftHash9;
                                    checkright2 = rightHash9;
                                    break;
                                case 10:
                                    checkleft1 = tempLeft10;
                                    checkright1 = tempRight10;
                                    checkleft2 = leftHash10;
                                    checkright2 = rightHash10;
                                    break;
                                case 11:
                                    checkleft1 = tempLeft11;
                                    checkright1 = tempRight11;
                                    checkleft2 = leftHash11;
                                    checkright2 = rightHash11;
                                    break;
                                case 12:
                                    checkleft1 = tempLeft12;
                                    checkright1 = tempRight12;
                                    checkleft2 = leftHash12;
                                    checkright2 = rightHash12;
                                    break;
                                case 13:
                                    checkleft1 = tempLeft13;
                                    checkright1 = tempRight13;
                                    checkleft2 = leftHash13;
                                    checkright2 = rightHash13;
                                    break;
                                case 14:
                                    checkleft1 = tempLeft14;
                                    checkright1 = tempRight14;
                                    checkleft2 = leftHash14;
                                    checkright2 = rightHash14;
                                    break;
                                case 15:
                                    checkleft1 = tempLeft15;
                                    checkright1 = tempRight15;
                                    checkleft2 = leftHash15;
                                    checkright2 = rightHash15;
                                    break;
                            }

                            //MessageBox.Show(checkleft1 + " " + checkleft2, "Left");
                            //MessageBox.Show(checkright1 + " " + checkright2, "Right");
                            if (checkleft1 == checkleft2 && checkright1 == checkright2 && !cheated)
                            {
                                cheated = false;
                            }
                            else
                            {
                                cheated = true;
                                break;
                            }
                        }
                    }
                }
                reader.Close();
            }

            return cheated;
        }

        private void ToCustomerBtn_Click(object sender, EventArgs e)
        {
            updateBox.AppendText("The signed money order was sent to the customer\n");
        }

        private void UnsignBtn_Click(object sender, EventArgs e)
        {
            // Call the RSAEncryption class
            RSAEncryption rsa = new RSAEncryption();

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            List<Payment> paymentList = new List<Payment>();

            // get the selection from the table
            string query = "SELECT * FROM [dbo].[UnblindedSelection]";

            using SqlCommand cmd = new SqlCommand(query, con);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Payment payment = new Payment();

                    payment.Amount = reader.GetString(1);
                    payment.SerialNumber = reader.GetString(2);
                    payment.Left1 = reader.GetString(3);
                    payment.Right1 = reader.GetString(4);
                    payment.Left2 = reader.GetString(5);
                    payment.Right2 = reader.GetString(6);
                    payment.Left3 = reader.GetString(7);
                    payment.Right3 = reader.GetString(8);
                    payment.Left4 = reader.GetString(9);
                    payment.Right4 = reader.GetString(10);
                    payment.Left5 = reader.GetString(11);
                    payment.Right5 = reader.GetString(12);
                    payment.Left6 = reader.GetString(13);
                    payment.Right6 = reader.GetString(14);
                    payment.Left7 = reader.GetString(15);
                    payment.Right7 = reader.GetString(16);
                    payment.Left8 = reader.GetString(17);
                    payment.Right8 = reader.GetString(18);
                    payment.Left9 = reader.GetString(19);
                    payment.Right9 = reader.GetString(20);
                    payment.Left10 = reader.GetString(21);
                    payment.Right10 = reader.GetString(22);
                    payment.Left11 = reader.GetString(23);
                    payment.Right11 = reader.GetString(24);
                    payment.Left12 = reader.GetString(25);
                    payment.Right12 = reader.GetString(26);
                    payment.Left13 = reader.GetString(27);
                    payment.Right13 = reader.GetString(28);
                    payment.Left14 = reader.GetString(29);
                    payment.Right14 = reader.GetString(30);
                    payment.Left15 = reader.GetString(31);
                    payment.Right15 = reader.GetString(32);

                    paymentList.Add(payment);
                }
                reader.Close();
            }

            string money = paymentList[0].Amount;

            // declare the path to the public key and load it in
            string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Merchant\bin\Debug\net7.0-windows\publickey.xml";
            rsa.LoadPublicFromXml(publicPath);

            // decrypt the data from the payment(s) and reinsert them
            for (int i = 0; i < paymentList.Count(); i++)
            {
                byte[] byteAmount = rsa.reveal(rsa.ConvertToBigInt(paymentList[i].Amount));
                byte[] byteSerialNumber = rsa.reveal(rsa.ConvertToBigInt(paymentList[i].SerialNumber));

                paymentList[i].Amount = Encoding.UTF8.GetString(byteAmount);
                paymentList[i].SerialNumber = Encoding.UTF8.GetString(byteSerialNumber);
            }

            // delete previous UnblindedSelection table elements
            string delQuery = "DELETE FROM [dbo].[UnblindedSelection]";
            SqlCommand comm2 = new SqlCommand(delQuery, con);
            comm2.ExecuteNonQuery();


            // get the revealed left and rights from the table
            for (int j = 0; j < paymentList.Count(); j++)
            {
                if (j == 1)
                {
                    money = paymentList[0].Amount;
                }

                string revealedQuery = "SELECT * FROM [dbo].[RevealedValues] WHERE moneyAmount = @moneyAmount";

                using SqlCommand revealedCmd = new SqlCommand(revealedQuery, con);
                revealedCmd.Parameters.AddWithValue("@moneyAmount", money);

                using (SqlDataReader revealedReader = revealedCmd.ExecuteReader())
                    {
                    while (revealedReader.Read())
                    {
                        for (int a = 1; a <= 15; a++)
                        {
                            switch (a)
                            {
                                case 1:
                                    if (revealedReader.GetString(3) != "")
                                    {
                                        paymentList[j].Left1 = revealedReader.GetString(3);
                                    }
                                    if (revealedReader.GetString(4) != "")
                                    {
                                        paymentList[j].Right1 = revealedReader.GetString(4);
                                    }
                                    break;
                                case 2:
                                    if (revealedReader.GetString(5) != "")
                                    {
                                        paymentList[j].Left2 = revealedReader.GetString(5);
                                    }
                                    if (revealedReader.GetString(6) != "")
                                    {
                                        paymentList[j].Right2 = revealedReader.GetString(6);
                                    }
                                    break;
                                case 3:
                                    if (revealedReader.GetString(7) != "")
                                    {
                                        paymentList[j].Left3 = revealedReader.GetString(7);
                                    }
                                    if (revealedReader.GetString(8) != "")
                                    {
                                        paymentList[j].Right3 = revealedReader.GetString(8);
                                    }
                                    break;
                                case 4:
                                    if (revealedReader.GetString(9) != "")
                                    {
                                        paymentList[j].Left4 = revealedReader.GetString(9);
                                    }
                                    if (revealedReader.GetString(10) != "")
                                    {
                                        paymentList[j].Right4 = revealedReader.GetString(10);
                                    }
                                    break;
                                case 5:
                                    if (revealedReader.GetString(11) != "")
                                    {
                                        paymentList[j].Left5 = revealedReader.GetString(11);
                                    }
                                    if (revealedReader.GetString(12) != "")
                                    {
                                        paymentList[j].Right5 = revealedReader.GetString(12);
                                    }
                                    break;
                                case 6:
                                    if (revealedReader.GetString(13) != "")
                                    {
                                        paymentList[j].Left6 = revealedReader.GetString(13);
                                    }
                                    if (revealedReader.GetString(14) != "")
                                    {
                                        paymentList[j].Right6 = revealedReader.GetString(14);
                                    }
                                    break;
                                case 7:
                                    if (revealedReader.GetString(15) != "")
                                    {
                                        paymentList[j].Left7 = revealedReader.GetString(15);
                                    }
                                    if (revealedReader.GetString(16) != "")
                                    {
                                        paymentList[j].Right7 = revealedReader.GetString(16);
                                    }
                                    break;
                                case 8:
                                    if (revealedReader.GetString(17) != "")
                                    {
                                        paymentList[j].Left8 = revealedReader.GetString(17);
                                    }
                                    if (revealedReader.GetString(18) != "")
                                    {
                                        paymentList[j].Right8 = revealedReader.GetString(18);
                                    }
                                    break;
                                case 9:
                                    if (revealedReader.GetString(19) != "")
                                    {
                                        paymentList[j].Left9 = revealedReader.GetString(19);
                                    }
                                    if (revealedReader.GetString(20) != "")
                                    {
                                        paymentList[j].Right9 = revealedReader.GetString(20);
                                    }
                                    break;
                                case 10:
                                    if (revealedReader.GetString(21) != "")
                                    {
                                        paymentList[j].Left10 = revealedReader.GetString(21);
                                    }
                                    if (revealedReader.GetString(22) != "")
                                    {
                                        paymentList[j].Right10 = revealedReader.GetString(22);
                                    }
                                    break;
                                case 11:
                                    if (revealedReader.GetString(23) != "")
                                    {
                                        paymentList[j].Left11 = revealedReader.GetString(23);
                                    }
                                    if (revealedReader.GetString(24) != "")
                                    {
                                        paymentList[j].Right11 = revealedReader.GetString(24);
                                    }
                                    break;
                                case 12:
                                    if (revealedReader.GetString(25) != "")
                                    {
                                        paymentList[j].Left12 = revealedReader.GetString(25);
                                    }
                                    if (revealedReader.GetString(26) != "")
                                    {
                                        paymentList[j].Right12 = revealedReader.GetString(26);
                                    }
                                    break;
                                case 13:
                                    if (revealedReader.GetString(27) != "")
                                    {
                                        paymentList[j].Left13 = revealedReader.GetString(27);
                                    }
                                    if (revealedReader.GetString(28) != "")
                                    {
                                        paymentList[j].Right13 = revealedReader.GetString(28);
                                    }
                                    break;
                                case 14:
                                    if (revealedReader.GetString(29) != "")
                                    {
                                        paymentList[j].Left14 = revealedReader.GetString(29);
                                    }
                                    if (revealedReader.GetString(30) != "")
                                    {
                                        paymentList[j].Right14 = revealedReader.GetString(30);
                                    }
                                    break;
                                case 15:
                                    if (revealedReader.GetString(31) != "")
                                    {
                                        paymentList[j].Left15 = revealedReader.GetString(31);
                                    }
                                    if (revealedReader.GetString(32) != "")
                                    {
                                        paymentList[j].Right15 = revealedReader.GetString(32);
                                    }
                                    break;
                            }
                        }
                    }
                    revealedReader.Close();
                }
            }

            // delete previous UnblindedSelection table elements
            string delQuery2 = "DELETE FROM [dbo].[RevealedValues]";
            SqlCommand comm3 = new SqlCommand(delQuery2, con);
            comm3.ExecuteNonQuery();

            foreach (Payment payment in paymentList)
            {
                string signedQuery = "INSERT INTO [dbo].[ArchivedPayments]([moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";

                SqlCommand cmdPayment = new SqlCommand(signedQuery, con);

                cmdPayment.Parameters.AddWithValue("@moneyAmount", payment.Amount);
                cmdPayment.Parameters.AddWithValue("@serialNumber", payment.SerialNumber);
                cmdPayment.Parameters.AddWithValue("@leftNumber1", payment.Left1);
                cmdPayment.Parameters.AddWithValue("@rightNumber1", payment.Right1);
                cmdPayment.Parameters.AddWithValue("@leftNumber2", payment.Left2);
                cmdPayment.Parameters.AddWithValue("@rightNumber2", payment.Right2);
                cmdPayment.Parameters.AddWithValue("@leftNumber3", payment.Left3);
                cmdPayment.Parameters.AddWithValue("@rightNumber3", payment.Right3);
                cmdPayment.Parameters.AddWithValue("@leftNumber4", payment.Left4);
                cmdPayment.Parameters.AddWithValue("@rightNumber4", payment.Right4);
                cmdPayment.Parameters.AddWithValue("@leftNumber5", payment.Left5);
                cmdPayment.Parameters.AddWithValue("@rightNumber5", payment.Right5);
                cmdPayment.Parameters.AddWithValue("@leftNumber6", payment.Left6);
                cmdPayment.Parameters.AddWithValue("@rightNumber6", payment.Right6);
                cmdPayment.Parameters.AddWithValue("@leftNumber7", payment.Left7);
                cmdPayment.Parameters.AddWithValue("@rightNumber7", payment.Right7);
                cmdPayment.Parameters.AddWithValue("@leftNumber8", payment.Left8);
                cmdPayment.Parameters.AddWithValue("@rightNumber8", payment.Right8);
                cmdPayment.Parameters.AddWithValue("@leftNumber9", payment.Left9);
                cmdPayment.Parameters.AddWithValue("@rightNumber9", payment.Right9);
                cmdPayment.Parameters.AddWithValue("@leftNumber10", payment.Left10);
                cmdPayment.Parameters.AddWithValue("@rightNumber10", payment.Right10);
                cmdPayment.Parameters.AddWithValue("@leftNumber11", payment.Left11);
                cmdPayment.Parameters.AddWithValue("@rightNumber11", payment.Right11);
                cmdPayment.Parameters.AddWithValue("@leftNumber12", payment.Left12);
                cmdPayment.Parameters.AddWithValue("@rightNumber12", payment.Right12);
                cmdPayment.Parameters.AddWithValue("@leftNumber13", payment.Left13);
                cmdPayment.Parameters.AddWithValue("@rightNumber13", payment.Right13);
                cmdPayment.Parameters.AddWithValue("@leftNumber14", payment.Left14);
                cmdPayment.Parameters.AddWithValue("@rightNumber14", payment.Right14);
                cmdPayment.Parameters.AddWithValue("@leftNumber15", payment.Left15);
                cmdPayment.Parameters.AddWithValue("@rightNumber15", payment.Right15);

                cmdPayment.ExecuteNonQuery();

                updateBox.AppendText($"The payment has been decrypted, the amount is ${payment.Amount}\n");
            }
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            if (LoggedIn)
            {
                UpdateLabels();
            }
        }
    }
}