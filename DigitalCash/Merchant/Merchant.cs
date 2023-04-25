using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Merchant
{
    public partial class Merchant : Form
    {
        public Merchant()
        {
            InitializeComponent();
        }

        public string connectionString = "Data Source=LAPTOP-UOPDFGH4\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";

        public string Username { get; set; }
        public int Balance { get; set; }
        public bool LoggedIn { get; set; }
        private int Amount { get; set; }
        private bool merchantCheat = false;
        private bool customerCheat = false;

        private void CustomerLoginBtn_Click(object sender, EventArgs e)
        {
            Login custLogin = new();
            custLogin.ShowDialog(this);
            UpdateLabels();

            updateBox.AppendText($"Logged in as {Username}\n");
        }

        private void UpdateLabels()
        {
            // update the balance
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "SELECT [balance],[ID] FROM LoginCredentials WHERE username = @username";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@username", Username);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Balance = reader.GetInt32(0);
                }
                con.Close();
            }

            usernameLbl.Text = Username;
            balanceAmountLbl.Text = "$" + Balance.ToString();
        }


        private void itemList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                itemList.View = View.Details;
                itemList.FullRowSelect = true;
                itemList.MultiSelect = true;
                string item = e.Item.Text;
                string price = e.Item.SubItems[1].Text;
            }
        }

        private void Merchant_Load(object sender, EventArgs e)
        {
            // Add items to ListView when Merchant form loads in
            ListViewItem belt = new ListViewItem("Belt");
            belt.SubItems.Add("$25");
            itemList.Items.Add(belt);

            ListViewItem shirt = new ListViewItem("Shirt");
            shirt.SubItems.Add("$40");
            itemList.Items.Add(shirt);

            ListViewItem tie = new ListViewItem("Tie");
            tie.SubItems.Add("$50");
            itemList.Items.Add(tie);

            ListViewItem pants = new ListViewItem("Pants");
            pants.SubItems.Add("$60");
            itemList.Items.Add(pants);

            ListViewItem backpack = new ListViewItem("Backpack");
            backpack.SubItems.Add("$70");
            itemList.Items.Add(backpack);

            ListViewItem jacket = new ListViewItem("Jacket");
            jacket.SubItems.Add("$85");
            itemList.Items.Add(jacket);

            ListViewItem watch = new ListViewItem("Watch");
            watch.SubItems.Add("$110");
            itemList.Items.Add(watch);

            ListViewItem sunglasses = new ListViewItem("Sunglasses");
            sunglasses.SubItems.Add("$130");
            itemList.Items.Add(sunglasses);

            ListViewItem necklace = new ListViewItem("Necklace");
            necklace.SubItems.Add("$200");
            itemList.Items.Add(necklace);
        }

        private void BuyBtn_Click(object sender, EventArgs e)
        {
            merchantCheat = false;
            customerCheat = false;

            UpdateLabels();

            RSAEncryption rsa = new RSAEncryption();

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            Random rand = new Random();

            // create left and right values lists
            List<string> leftValues = new List<string>();
            List<string> rightValues = new List<string>();

            if (!LoggedIn)
            {
                MessageBox.Show("Please log in.", "Error");
            }
            else
            {
                List<int> prices = new List<int>();
                List<string> items = new List<string>();

                foreach (ListViewItem selectedItem in itemList.SelectedItems)
                {
                    // Buy items
                    string costString = selectedItem.SubItems[1].Text.Substring(1, selectedItem.SubItems[1].Text.Length - 1);
                    int costInt = Convert.ToInt32(costString);

                    prices.Add(costInt);

                    string itemString = selectedItem.SubItems[0].Text.ToLower();
                    items.Add(itemString);
                }

                if (items.Count != 0)
                {
                    int total = 0;
                    foreach (int price in prices)  // Loop through the list
                    {
                        total += price;  // Add each integer to the total
                    }

                    // get the selection from the table
                    string query = "SELECT * FROM [dbo].[UnblindedSelection]";

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

                    using SqlCommand cmd = new SqlCommand(query, con);
                    using SqlDataReader reader = cmd.ExecuteReader();

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

                    // declare the path to the public key and load it in
                    string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Merchant\bin\Debug\net7.0-windows\publickey.xml";
                    rsa.LoadPublicFromXml(publicPath);

                    byte[] byteAmount = rsa.reveal(rsa.ConvertToBigInt(amountString));

                    string amount = Encoding.UTF8.GetString(byteAmount);

                    try
                    {
                        Amount = Int32.Parse(amount);
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show("The money order is empty or has incorrect values", "Error");
                        return;
                    }


                    if (Amount + 0.0001 <= total)
                    {
                        MessageBox.Show("Insufficient funds.", "Error");
                    }
                    else
                    {
                        // this will submit the order twice to the bank then delete the payment table
                        if (MerchantChkBx.Checked == true)
                        {
                            // Give the payment to the bank
                            string TwiceQuery = "INSERT INTO [dbo].[UnblindedSelection]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";

                            SqlCommand cmdTwice = new SqlCommand(TwiceQuery, con);

                            // Insert them to ArchivedPayments
                            cmdTwice.Parameters.AddWithValue("@index", index);
                            cmdTwice.Parameters.AddWithValue("@moneyAmount", amountString);
                            cmdTwice.Parameters.AddWithValue("@serialNumber", serialNumberString);
                            cmdTwice.Parameters.AddWithValue("@leftNumber1", left1);
                            cmdTwice.Parameters.AddWithValue("@rightNumber1", right1);
                            cmdTwice.Parameters.AddWithValue("@leftNumber2", left2);
                            cmdTwice.Parameters.AddWithValue("@rightNumber2", right2);
                            cmdTwice.Parameters.AddWithValue("@leftNumber3", left3);
                            cmdTwice.Parameters.AddWithValue("@rightNumber3", right3);
                            cmdTwice.Parameters.AddWithValue("@leftNumber4", left4);
                            cmdTwice.Parameters.AddWithValue("@rightNumber4", right4);
                            cmdTwice.Parameters.AddWithValue("@leftNumber5", left5);
                            cmdTwice.Parameters.AddWithValue("@rightNumber5", right5);
                            cmdTwice.Parameters.AddWithValue("@leftNumber6", left6);
                            cmdTwice.Parameters.AddWithValue("@rightNumber6", right6);
                            cmdTwice.Parameters.AddWithValue("@leftNumber7", left7);
                            cmdTwice.Parameters.AddWithValue("@rightNumber7", right7);
                            cmdTwice.Parameters.AddWithValue("@leftNumber8", left8);
                            cmdTwice.Parameters.AddWithValue("@rightNumber8", right8);
                            cmdTwice.Parameters.AddWithValue("@leftNumber9", left9);
                            cmdTwice.Parameters.AddWithValue("@rightNumber9", right9);
                            cmdTwice.Parameters.AddWithValue("@leftNumber10", left10);
                            cmdTwice.Parameters.AddWithValue("@rightNumber10", right10);
                            cmdTwice.Parameters.AddWithValue("@leftNumber11", left11);
                            cmdTwice.Parameters.AddWithValue("@rightNumber11", right11);
                            cmdTwice.Parameters.AddWithValue("@leftNumber12", left12);
                            cmdTwice.Parameters.AddWithValue("@rightNumber12", right12);
                            cmdTwice.Parameters.AddWithValue("@leftNumber13", left13);
                            cmdTwice.Parameters.AddWithValue("@rightNumber13", right13);
                            cmdTwice.Parameters.AddWithValue("@leftNumber14", left14);
                            cmdTwice.Parameters.AddWithValue("@rightNumber14", right14);
                            cmdTwice.Parameters.AddWithValue("@leftNumber15", left15);
                            cmdTwice.Parameters.AddWithValue("@rightNumber15", right15);

                            cmdTwice.ExecuteNonQuery();


                            // re-input the revealedvalues
                            // get the revealed left and rights from the table
                            string revealedQuery = "SELECT * FROM [dbo].[RevealedValues]";

                            using SqlCommand revealedCmd = new SqlCommand(revealedQuery, con);
                            using SqlDataReader revealedReader = revealedCmd.ExecuteReader();


                            while (revealedReader.Read())
                            {
                                index = revealedReader.GetInt32(0);
                                amountString = revealedReader.GetString(1);
                                serialNumberString = revealedReader.GetString(2);
                                left1 = revealedReader.GetString(3);
                                right1 = revealedReader.GetString(4);
                                left2 = revealedReader.GetString(5);
                                right2 = revealedReader.GetString(6);
                                left3 = revealedReader.GetString(7);
                                right3 = revealedReader.GetString(8);
                                left4 = revealedReader.GetString(9);
                                right4 = revealedReader.GetString(10);
                                left5 = revealedReader.GetString(11);
                                right5 = revealedReader.GetString(12);
                                left6 = revealedReader.GetString(13);
                                right6 = revealedReader.GetString(14);
                                left7 = revealedReader.GetString(15);
                                right7 = revealedReader.GetString(16);
                                left8 = revealedReader.GetString(17);
                                right8 = revealedReader.GetString(18);
                                left9 = revealedReader.GetString(19);
                                right9 = revealedReader.GetString(20);
                                left10 = revealedReader.GetString(21);
                                right10 = revealedReader.GetString(22);
                                left11 = revealedReader.GetString(23);
                                right11 = revealedReader.GetString(24);
                                left12 = revealedReader.GetString(25);
                                right12 = revealedReader.GetString(26);
                                left13 = revealedReader.GetString(27);
                                right13 = revealedReader.GetString(28);
                                left14 = revealedReader.GetString(29);
                                right14 = revealedReader.GetString(30);
                                left15 = revealedReader.GetString(31);
                                right15 = revealedReader.GetString(32);
                            }
                            revealedReader.Close();


                            // Resend the revealed values to the bank
                            string query2 = "INSERT INTO [dbo].[RevealedValues]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";

                            SqlCommand cmd2 = new SqlCommand(query2, con);

                            // Insert them to ArchivedPayments
                            cmd2.Parameters.AddWithValue("@index", index);
                            cmd2.Parameters.AddWithValue("@moneyAmount", amount);
                            cmd2.Parameters.AddWithValue("@serialNumber", serialNumberString);
                            cmd2.Parameters.AddWithValue("@leftNumber1", left1);
                            cmd2.Parameters.AddWithValue("@rightNumber1", right1);
                            cmd2.Parameters.AddWithValue("@leftNumber2", left2);
                            cmd2.Parameters.AddWithValue("@rightNumber2", right2);
                            cmd2.Parameters.AddWithValue("@leftNumber3", left3);
                            cmd2.Parameters.AddWithValue("@rightNumber3", right3);
                            cmd2.Parameters.AddWithValue("@leftNumber4", left4);
                            cmd2.Parameters.AddWithValue("@rightNumber4", right4);
                            cmd2.Parameters.AddWithValue("@leftNumber5", left5);
                            cmd2.Parameters.AddWithValue("@rightNumber5", right5);
                            cmd2.Parameters.AddWithValue("@leftNumber6", left6);
                            cmd2.Parameters.AddWithValue("@rightNumber6", right6);
                            cmd2.Parameters.AddWithValue("@leftNumber7", left7);
                            cmd2.Parameters.AddWithValue("@rightNumber7", right7);
                            cmd2.Parameters.AddWithValue("@leftNumber8", left8);
                            cmd2.Parameters.AddWithValue("@rightNumber8", right8);
                            cmd2.Parameters.AddWithValue("@leftNumber9", left9);
                            cmd2.Parameters.AddWithValue("@rightNumber9", right9);
                            cmd2.Parameters.AddWithValue("@leftNumber10", left10);
                            cmd2.Parameters.AddWithValue("@rightNumber10", right10);
                            cmd2.Parameters.AddWithValue("@leftNumber11", left11);
                            cmd2.Parameters.AddWithValue("@rightNumber11", right11);
                            cmd2.Parameters.AddWithValue("@leftNumber12", left12);
                            cmd2.Parameters.AddWithValue("@rightNumber12", right12);
                            cmd2.Parameters.AddWithValue("@leftNumber13", left13);
                            cmd2.Parameters.AddWithValue("@rightNumber13", right13);
                            cmd2.Parameters.AddWithValue("@leftNumber14", left14);
                            cmd2.Parameters.AddWithValue("@rightNumber14", right14);
                            cmd2.Parameters.AddWithValue("@leftNumber15", left15);
                            cmd2.Parameters.AddWithValue("@rightNumber15", right15);

                            cmd2.ExecuteNonQuery();


                            merchantCheat = true;
                        }

                        // this will submit the order twice to the bank but will have different left and rights revealed
                        if (CustomerChkBx.Checked == true)
                        {
                            // Give the payment to the bank
                            string TwiceQuery = "INSERT INTO [dbo].[UnblindedSelection]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";

                            SqlCommand cmdTwice = new SqlCommand(TwiceQuery, con);

                            // Insert them to ArchivedPayments
                            cmdTwice.Parameters.AddWithValue("@index", index);
                            cmdTwice.Parameters.AddWithValue("@moneyAmount", amountString);
                            cmdTwice.Parameters.AddWithValue("@serialNumber", serialNumberString);
                            cmdTwice.Parameters.AddWithValue("@leftNumber1", left1);
                            cmdTwice.Parameters.AddWithValue("@rightNumber1", right1);
                            cmdTwice.Parameters.AddWithValue("@leftNumber2", left2);
                            cmdTwice.Parameters.AddWithValue("@rightNumber2", right2);
                            cmdTwice.Parameters.AddWithValue("@leftNumber3", left3);
                            cmdTwice.Parameters.AddWithValue("@rightNumber3", right3);
                            cmdTwice.Parameters.AddWithValue("@leftNumber4", left4);
                            cmdTwice.Parameters.AddWithValue("@rightNumber4", right4);
                            cmdTwice.Parameters.AddWithValue("@leftNumber5", left5);
                            cmdTwice.Parameters.AddWithValue("@rightNumber5", right5);
                            cmdTwice.Parameters.AddWithValue("@leftNumber6", left6);
                            cmdTwice.Parameters.AddWithValue("@rightNumber6", right6);
                            cmdTwice.Parameters.AddWithValue("@leftNumber7", left7);
                            cmdTwice.Parameters.AddWithValue("@rightNumber7", right7);
                            cmdTwice.Parameters.AddWithValue("@leftNumber8", left8);
                            cmdTwice.Parameters.AddWithValue("@rightNumber8", right8);
                            cmdTwice.Parameters.AddWithValue("@leftNumber9", left9);
                            cmdTwice.Parameters.AddWithValue("@rightNumber9", right9);
                            cmdTwice.Parameters.AddWithValue("@leftNumber10", left10);
                            cmdTwice.Parameters.AddWithValue("@rightNumber10", right10);
                            cmdTwice.Parameters.AddWithValue("@leftNumber11", left11);
                            cmdTwice.Parameters.AddWithValue("@rightNumber11", right11);
                            cmdTwice.Parameters.AddWithValue("@leftNumber12", left12);
                            cmdTwice.Parameters.AddWithValue("@rightNumber12", right12);
                            cmdTwice.Parameters.AddWithValue("@leftNumber13", left13);
                            cmdTwice.Parameters.AddWithValue("@rightNumber13", right13);
                            cmdTwice.Parameters.AddWithValue("@leftNumber14", left14);
                            cmdTwice.Parameters.AddWithValue("@rightNumber14", right14);
                            cmdTwice.Parameters.AddWithValue("@leftNumber15", left15);
                            cmdTwice.Parameters.AddWithValue("@rightNumber15", right15);

                            cmdTwice.ExecuteNonQuery();

                            // declare the path to the public key and load it in
                            rsa.LoadPublicFromXml(publicPath);

                            byte[] byteSerial = rsa.reveal(rsa.ConvertToBigInt(serialNumberString));

                            string serialNumber = Encoding.UTF8.GetString(byteSerial);

                            for (int i = 1; i <= 15; i++)
                            {
                                int selection = rand.Next(1, 3);
                                string value = "";

                                if (selection == 1)
                                {
                                    string revealQuery = "SELECT * FROM [dbo].[MoneyOrder] WHERE [serialNumber] = @serialNumber";

                                    using SqlCommand revealCmd = new SqlCommand(revealQuery, con);
                                    revealCmd.Parameters.AddWithValue("@serialNumber", serialNumber);
                                    using SqlDataReader revealReader = revealCmd.ExecuteReader();

                                    if (revealReader.Read())
                                    {
                                        // i + 2 because the left values start at index 3 in the database
                                        value = revealReader.GetInt32(i + 2).ToString();
                                        leftValues.Add(value);
                                        rightValues.Add("");
                                    }
                                    revealReader.Close();
                                }
                                else if (selection == 2)
                                {
                                    string revealQuery = "SELECT * FROM [dbo].[MoneyOrder] WHERE [serialNumber] = @serialNumber";

                                    using SqlCommand revealCmd = new SqlCommand(revealQuery, con);
                                    revealCmd.Parameters.AddWithValue("@serialNumber", serialNumber);
                                    using SqlDataReader revealReader = revealCmd.ExecuteReader();

                                    if (revealReader.Read())
                                    {
                                        // i + 3 because the right values start at index 4 in the database
                                        value = revealReader.GetInt32(i + 3).ToString();
                                        rightValues.Add(value);
                                        leftValues.Add("");
                                    }
                                    revealReader.Close();
                                }
                            }

                            // put the data into the archivedpayment table
                            string tempCmdQuery = "INSERT INTO [dbo].[RevealedValues]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";

                            SqlCommand tempCmdPayment = new SqlCommand(tempCmdQuery, con);

                            // Insert them to archivedpayment
                            tempCmdPayment.Parameters.AddWithValue("@index", index);
                            tempCmdPayment.Parameters.AddWithValue("@moneyAmount", amount);
                            tempCmdPayment.Parameters.AddWithValue("@serialNumber", serialNumberString);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber1", leftValues[0]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber1", rightValues[0]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber2", leftValues[1]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber2", rightValues[1]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber3", leftValues[2]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber3", rightValues[2]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber4", leftValues[3]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber4", rightValues[3]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber5", leftValues[4]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber5", rightValues[4]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber6", leftValues[5]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber6", rightValues[5]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber7", leftValues[6]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber7", rightValues[6]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber8", leftValues[7]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber8", rightValues[7]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber9", leftValues[8]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber9", rightValues[8]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber10", leftValues[9]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber10", rightValues[9]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber11", leftValues[10]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber11", rightValues[10]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber12", leftValues[11]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber12", rightValues[11]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber13", leftValues[12]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber13", rightValues[12]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber14", leftValues[13]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber14", rightValues[13]);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber15", leftValues[14]);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber15", rightValues[14]);

                            tempCmdPayment.ExecuteNonQuery();

                            customerCheat = true;
                        }


                        // Create a string to print the items bought and sort them properly to fit a sentence
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (items[i] != "pants" && items[i] != "sunglasses")
                            {
                                items[i] = "a " + items[i];
                            }
                        }

                        string itemsString;

                        if (items.Count == 2)
                        {
                            itemsString = string.Join(" and ", items);
                        }
                        else if (items.Count > 2)
                        {
                            string lastItem = items.Last();
                            items.RemoveAt(items.Count - 1);
                            string remainingItemsString = string.Join(", ", items);
                            itemsString = $"{remainingItemsString}, and {lastItem}";
                        }
                        else
                        {
                            itemsString = string.Join(", ", items);
                        }

                        updateBox.AppendText($"The merchant received a money order for ${Amount}. The customer received {itemsString}\n");
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Please select an item.", "Error");
                }
            }
        }

        private void RevealBtn_Click(object sender, EventArgs e)
        {
            UpdateLabels();

            // Call the RSAEncryption class
            RSAEncryption rsa = new RSAEncryption();

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            // get the selection from the table
            string query = "SELECT * FROM [dbo].[UnblindedSelection]";

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


            // declare the path to the public key and load it in
            string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Merchant\bin\Debug\net7.0-windows\publickey.xml";
            rsa.LoadPublicFromXml(publicPath);

            byte[] byteAmount = rsa.reveal(rsa.ConvertToBigInt(amountString));
            byte[] byteSerialNumber = rsa.reveal(rsa.ConvertToBigInt(serialNumberString));

            string amount = Encoding.UTF8.GetString(byteAmount);
            string serialNumber = Encoding.UTF8.GetString(byteSerialNumber);

            // clear the Payment table elements to prevent any issues
            string deleteQuery = "DELETE FROM [dbo].[Payment]";
            SqlCommand comm = new SqlCommand(deleteQuery, con);
            comm.ExecuteNonQuery();

            // put the data into the payment table
            string signedQuery = "INSERT INTO [dbo].[Payment]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";

            SqlCommand cmdPayment = new SqlCommand(signedQuery, con);

            // Insert them to MoneyOrder
            cmdPayment.Parameters.AddWithValue("@index", index);
            cmdPayment.Parameters.AddWithValue("@moneyAmount", amount);
            cmdPayment.Parameters.AddWithValue("@serialNumber", serialNumber);
            cmdPayment.Parameters.AddWithValue("@leftNumber1", left1);
            cmdPayment.Parameters.AddWithValue("@rightNumber1", right1);
            cmdPayment.Parameters.AddWithValue("@leftNumber2", left2);
            cmdPayment.Parameters.AddWithValue("@rightNumber2", right2);
            cmdPayment.Parameters.AddWithValue("@leftNumber3", left3);
            cmdPayment.Parameters.AddWithValue("@rightNumber3", right3);
            cmdPayment.Parameters.AddWithValue("@leftNumber4", left4);
            cmdPayment.Parameters.AddWithValue("@rightNumber4", right4);
            cmdPayment.Parameters.AddWithValue("@leftNumber5", left5);
            cmdPayment.Parameters.AddWithValue("@rightNumber5", right5);
            cmdPayment.Parameters.AddWithValue("@leftNumber6", left6);
            cmdPayment.Parameters.AddWithValue("@rightNumber6", right6);
            cmdPayment.Parameters.AddWithValue("@leftNumber7", left7);
            cmdPayment.Parameters.AddWithValue("@rightNumber7", right7);
            cmdPayment.Parameters.AddWithValue("@leftNumber8", left8);
            cmdPayment.Parameters.AddWithValue("@rightNumber8", right8);
            cmdPayment.Parameters.AddWithValue("@leftNumber9", left9);
            cmdPayment.Parameters.AddWithValue("@rightNumber9", right9);
            cmdPayment.Parameters.AddWithValue("@leftNumber10", left10);
            cmdPayment.Parameters.AddWithValue("@rightNumber10", right10);
            cmdPayment.Parameters.AddWithValue("@leftNumber11", left11);
            cmdPayment.Parameters.AddWithValue("@rightNumber11", right11);
            cmdPayment.Parameters.AddWithValue("@leftNumber12", left12);
            cmdPayment.Parameters.AddWithValue("@rightNumber12", right12);
            cmdPayment.Parameters.AddWithValue("@leftNumber13", left13);
            cmdPayment.Parameters.AddWithValue("@rightNumber13", right13);
            cmdPayment.Parameters.AddWithValue("@leftNumber14", left14);
            cmdPayment.Parameters.AddWithValue("@rightNumber14", right14);
            cmdPayment.Parameters.AddWithValue("@leftNumber15", left15);
            cmdPayment.Parameters.AddWithValue("@rightNumber15", right15);

            cmdPayment.ExecuteNonQuery();


            // put the data into the temppayment table
            string tempQuery = "INSERT INTO [dbo].[TempPayment]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";

            SqlCommand cmdTemp = new SqlCommand(tempQuery, con);

            // Insert them to temppayment
            cmdTemp.Parameters.AddWithValue("@index", index);
            cmdTemp.Parameters.AddWithValue("@moneyAmount", amount);
            cmdTemp.Parameters.AddWithValue("@serialNumber", serialNumber);
            cmdTemp.Parameters.AddWithValue("@leftNumber1", left1);
            cmdTemp.Parameters.AddWithValue("@rightNumber1", right1);
            cmdTemp.Parameters.AddWithValue("@leftNumber2", left2);
            cmdTemp.Parameters.AddWithValue("@rightNumber2", right2);
            cmdTemp.Parameters.AddWithValue("@leftNumber3", left3);
            cmdTemp.Parameters.AddWithValue("@rightNumber3", right3);
            cmdTemp.Parameters.AddWithValue("@leftNumber4", left4);
            cmdTemp.Parameters.AddWithValue("@rightNumber4", right4);
            cmdTemp.Parameters.AddWithValue("@leftNumber5", left5);
            cmdTemp.Parameters.AddWithValue("@rightNumber5", right5);
            cmdTemp.Parameters.AddWithValue("@leftNumber6", left6);
            cmdTemp.Parameters.AddWithValue("@rightNumber6", right6);
            cmdTemp.Parameters.AddWithValue("@leftNumber7", left7);
            cmdTemp.Parameters.AddWithValue("@rightNumber7", right7);
            cmdTemp.Parameters.AddWithValue("@leftNumber8", left8);
            cmdTemp.Parameters.AddWithValue("@rightNumber8", right8);
            cmdTemp.Parameters.AddWithValue("@leftNumber9", left9);
            cmdTemp.Parameters.AddWithValue("@rightNumber9", right9);
            cmdTemp.Parameters.AddWithValue("@leftNumber10", left10);
            cmdTemp.Parameters.AddWithValue("@rightNumber10", right10);
            cmdTemp.Parameters.AddWithValue("@leftNumber11", left11);
            cmdTemp.Parameters.AddWithValue("@rightNumber11", right11);
            cmdTemp.Parameters.AddWithValue("@leftNumber12", left12);
            cmdTemp.Parameters.AddWithValue("@rightNumber12", right12);
            cmdTemp.Parameters.AddWithValue("@leftNumber13", left13);
            cmdTemp.Parameters.AddWithValue("@rightNumber13", right13);
            cmdTemp.Parameters.AddWithValue("@leftNumber14", left14);
            cmdTemp.Parameters.AddWithValue("@rightNumber14", right14);
            cmdTemp.Parameters.AddWithValue("@leftNumber15", left15);
            cmdTemp.Parameters.AddWithValue("@rightNumber15", right15);

            cmdTemp.ExecuteNonQuery();

            UpdateLabels();

            updateBox.AppendText($"The money order is decrypted, the amount is ${amount}\n");
        }

        private void HashBtn_Click(object sender, EventArgs e)
        {
            UpdateLabels();

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            // get the selection from the table
            string query = "SELECT * FROM [dbo].[UnblindedSelection]";

            Random rand = new Random();

            // make left and right values list
            List<string> leftValues = new List<string>();
            List<string> rightValues = new List<string>();

            using SqlCommand cmd = new SqlCommand(query, con);
            using SqlDataReader reader = cmd.ExecuteReader();

            int index = 0;
            string amount = "";
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
                amount = reader.GetString(1);
                serialNumberString = reader.GetString(2);
            }
            reader.Close();

            RSAEncryption rsa = new();

            // declare the path to the public key and load it in
            string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Merchant\bin\Debug\net7.0-windows\publickey.xml";
            rsa.LoadPublicFromXml(publicPath);

            byte[] byteSerial = rsa.reveal(rsa.ConvertToBigInt(serialNumberString));

            string serialNumber = Encoding.UTF8.GetString(byteSerial);

            for (int i = 1; i <= 15; i++)
            {
                int selection = rand.Next(1, 3);
                string value = "";

                if (selection == 1)
                {
                    string revealQuery = "SELECT * FROM [dbo].[MoneyOrder] WHERE [serialNumber] = @serialNumber";

                    using SqlCommand revealCmd = new SqlCommand(revealQuery, con);
                    revealCmd.Parameters.AddWithValue("@serialNumber", serialNumber);
                    using SqlDataReader revealReader = revealCmd.ExecuteReader();

                    if (revealReader.Read())
                    {
                        // i + 2 because the left values start at index 3 in the database
                        value = revealReader.GetInt32(i + 2).ToString();
                        leftValues.Add(value);
                        rightValues.Add("");
                    }
                    revealReader.Close();
                }
                else if (selection == 2)
                {
                    string revealQuery = "SELECT * FROM [dbo].[MoneyOrder] WHERE [serialNumber] = @serialNumber";

                    using SqlCommand revealCmd = new SqlCommand(revealQuery, con);
                    revealCmd.Parameters.AddWithValue("@serialNumber", serialNumber);
                    using SqlDataReader revealReader = revealCmd.ExecuteReader();

                    if (revealReader.Read())
                    {
                        // i + 3 because the right values start at index 4 in the database
                        value = revealReader.GetInt32(i + 3).ToString();
                        rightValues.Add(value);
                        leftValues.Add("");
                    }
                    revealReader.Close();
                }
            }

            // put the data into the archivedpayment table
            string tempCmdQuery = "INSERT INTO [dbo].[RevealedValues]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";

            SqlCommand tempCmdPayment = new SqlCommand(tempCmdQuery, con);

            // Insert them to archivedpayment
            tempCmdPayment.Parameters.AddWithValue("@index", index);
            tempCmdPayment.Parameters.AddWithValue("@moneyAmount", amount);
            tempCmdPayment.Parameters.AddWithValue("@serialNumber", serialNumberString);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber1", leftValues[0]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber1", rightValues[0]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber2", leftValues[1]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber2", rightValues[1]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber3", leftValues[2]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber3", rightValues[2]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber4", leftValues[3]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber4", rightValues[3]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber5", leftValues[4]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber5", rightValues[4]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber6", leftValues[5]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber6", rightValues[5]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber7", leftValues[6]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber7", rightValues[6]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber8", leftValues[7]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber8", rightValues[7]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber9", leftValues[8]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber9", rightValues[8]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber10", leftValues[9]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber10", rightValues[9]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber11", leftValues[10]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber11", rightValues[10]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber12", leftValues[11]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber12", rightValues[11]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber13", leftValues[12]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber13", rightValues[12]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber14", leftValues[13]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber14", rightValues[13]);
            tempCmdPayment.Parameters.AddWithValue("@leftNumber15", leftValues[14]);
            tempCmdPayment.Parameters.AddWithValue("@rightNumber15", rightValues[14]);

            tempCmdPayment.ExecuteNonQuery();

            UpdateLabels();

            updateBox.AppendText("Random left and right values in the money order are revealed\n");
        }

        private void ToBankBtn_Click(object sender, EventArgs e)
        {
            UpdateLabels();

            if (merchantCheat)
            {
                updateBox.AppendText("The signed money order and revealed left and rights have been sent to the bank twice\n");
            }
            else if (customerCheat)
            {
                updateBox.AppendText("The signed money order has been sent to the bank and the left and rights have been revealed differently for each money order sent");
            }
            else
            {
                updateBox.AppendText("The signed money order and revealed left and rights have been sent to the bank\n");
            }
        }

        private void VerifyHashesBtn_Click(object sender, EventArgs e)
        {
            UpdateLabels();

            // this is our bool we return true if hashes match and false if they dont
            bool cheated = false;

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            // join the two tables of MoneyOrder and ArchivedBlinds on the serialNumber
            string selectQuery = "SELECT * FROM [dbo].[MoneyOrder] JOIN [dbo].[HashMoneyOrder] ON [dbo].[MoneyOrder].[serialNumber] = [dbo].[HashMoneyOrder].[serialNumber]";

            using SqlCommand cmd = new SqlCommand(selectQuery, con);
            using SqlDataReader reader = cmd.ExecuteReader();


            // Choose a random order to be the selection and we wont check that order
            Random rand = new();

            // if there are no blinds, return a message that informs the user and then return the data from the table
            if (!reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("The tables did not join properly");
            }
            else
            {
                // read the data from money order to get money amount and serial
                while (reader.Read())
                {
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
                reader.Close();
            }

            if (cheated)
            {
                updateBox.AppendText("The customer sent fake revealed hashes\n");
            }
            else
            {
                updateBox.AppendText("The revealed values match the hashed values\n");
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