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

namespace Merchant
{
    public partial class Merchant : Form
    {
        public Merchant()
        {
            InitializeComponent();
        }

        public string connectionString = "Data Source=BEN_T\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";

        public string Username { get; set; }
        public int Balance { get; set; }
        public bool LoggedIn { get; set; }
        private int Amount { get; set; }

        private void CustomerLoginBtn_Click(object sender, EventArgs e)
        {
            Login custLogin = new();
            custLogin.ShowDialog(this);
            UpdateLabels();
        }

        private void UpdateLabels()
        {
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
            RSAEncryption rsa = new RSAEncryption();

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
                    MessageBox.Show(selectedItem.Text + " " + selectedItem.SubItems[1].Text, "Item");

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

                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();

                    // get the selection from the table
                    string query = "SELECT * FROM [dbo].[Payment]";

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

                    Amount = Int32.Parse(amountString);

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
                            string TwiceQuery = "INSERT INTO [dbo].[ArchivedPayments]([moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";

                            SqlCommand cmdTwice = new SqlCommand(TwiceQuery, con);

                            // Insert them to ArchivedPayments
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
                        }

                        // this will submit the order twice to the bank but will have different left and rights revealed
                        if (CustomerChkBx.Checked == true)
                        {
                            // get the selection from the table
                            string tempQuery = "SELECT * FROM [dbo].[TempPayment]";

                            using SqlCommand cmdTemp = new SqlCommand(tempQuery, con);
                            using SqlDataReader tempReader = cmdTemp.ExecuteReader();

                            int tempIndex = 0;
                            string tempAmountString = "";
                            string tempSerialNumberString = "";
                            string tempLeft1 = "";
                            string tempRight1 = "";
                            string tempLeft2 = "";
                            string tempRight2 = "";
                            string tempLeft3 = "";
                            string tempRight3 = "";
                            string tempLeft4 = "";
                            string tempRight4 = "";
                            string tempLeft5 = "";
                            string tempRight5 = "";
                            string tempLeft6 = "";
                            string tempRight6 = "";
                            string tempLeft7 = "";
                            string tempRight7 = "";
                            string tempLeft8 = "";
                            string tempRight8 = "";
                            string tempLeft9 = "";
                            string tempRight9 = "";
                            string tempLeft10 = "";
                            string tempRight10 = "";
                            string tempLeft11 = "";
                            string tempRight11 = "";
                            string tempLeft12 = "";
                            string tempRight12 = "";
                            string tempLeft13 = "";
                            string tempRight13 = "";
                            string tempLeft14 = "";
                            string tempRight14 = "";
                            string tempLeft15 = "";
                            string tempRight15 = "";

                            while (tempReader.Read())
                            {
                                tempIndex = tempReader.GetInt32(0);
                                tempAmountString = tempReader.GetString(1);
                                tempSerialNumberString = tempReader.GetString(2);
                                tempLeft1 = tempReader.GetString(3);
                                tempRight1 = tempReader.GetString(4);
                                tempLeft2 = tempReader.GetString(5);
                                tempRight2 = tempReader.GetString(6);
                                tempLeft3 = tempReader.GetString(7);
                                tempRight3 = tempReader.GetString(8);
                                tempLeft4 = tempReader.GetString(9);
                                tempRight4 = tempReader.GetString(10);
                                tempLeft5 = tempReader.GetString(11);
                                tempRight5 = tempReader.GetString(12);
                                tempLeft6 = tempReader.GetString(13);
                                tempRight6 = tempReader.GetString(14);
                                tempLeft7 = tempReader.GetString(15);
                                tempRight7 = tempReader.GetString(16);
                                tempLeft8 = tempReader.GetString(17);
                                tempRight8 = tempReader.GetString(18);
                                tempLeft9 = tempReader.GetString(19);
                                tempRight9 = tempReader.GetString(20);
                                tempLeft10 = tempReader.GetString(21);
                                tempRight10 = tempReader.GetString(22);
                                tempLeft11 = tempReader.GetString(23);
                                tempRight11 = tempReader.GetString(24);
                                tempLeft12 = tempReader.GetString(25);
                                tempRight12 = tempReader.GetString(26);
                                tempLeft13 = tempReader.GetString(27);
                                tempRight13 = tempReader.GetString(28);
                                tempLeft14 = tempReader.GetString(29);
                                tempRight14 = tempReader.GetString(30);
                                tempLeft15 = tempReader.GetString(31);
                                tempRight15 = tempReader.GetString(32);
                            }
                            tempReader.Close();

                            for (int i = 1; i <= 15; i++)
                            {
                                Random rand = new Random();
                                int selection = rand.Next(1, 3);

                                if (selection == 1)
                                {
                                    string revealQuery = "SELECT * FROM [dbo].[MoneyOrder] WHERE [serialNumber] = @serialNumber";

                                    using SqlCommand revealCmd = new SqlCommand(revealQuery, con);
                                    revealCmd.Parameters.AddWithValue("@serialNumber", serialNumberString);
                                    using SqlDataReader revealReader = revealCmd.ExecuteReader();

                                    if (revealReader.Read())
                                    {
                                        string leftValue = revealReader.GetInt32(i + 2).ToString();
                                        // i + 2 because the left values start at index 3 in the database
                                        switch (i)
                                        {
                                            case 1:
                                                tempLeft1 = leftValue;
                                                break;
                                            case 2:
                                                tempLeft2 = leftValue;
                                                break;
                                            case 3:
                                                tempLeft3 = leftValue;
                                                break;
                                            case 4:
                                                tempLeft4 = leftValue;
                                                break;
                                            case 5:
                                                tempLeft5 = leftValue;
                                                break;
                                            case 6:
                                                tempLeft6 = leftValue;
                                                break;
                                            case 7:
                                                tempLeft7 = leftValue;
                                                break;
                                            case 8:
                                                tempLeft8 = leftValue;
                                                break;
                                            case 9:
                                                tempLeft9 = leftValue;
                                                break;
                                            case 10:
                                                tempLeft10 = leftValue;
                                                break;
                                            case 11:
                                                tempLeft11 = leftValue;
                                                break;
                                            case 12:
                                                tempLeft12 = leftValue;
                                                break;
                                            case 13:
                                                tempLeft13 = leftValue;
                                                break;
                                            case 14:
                                                tempLeft14 = leftValue;
                                                break;
                                            case 15:
                                                tempLeft15 = leftValue;
                                                break;
                                        }
                                    }
                                    revealReader.Close();
                                }
                                else if (selection == 2)
                                {
                                    string revealQuery = "SELECT * FROM [dbo].[MoneyOrder] WHERE [serialNumber] = @serialNumber";

                                    using SqlCommand revealCmd = new SqlCommand(revealQuery, con);
                                    revealCmd.Parameters.AddWithValue("@serialNumber", serialNumberString);
                                    using SqlDataReader revealReader = revealCmd.ExecuteReader();

                                    if (revealReader.Read())
                                    {
                                        string rightValue = revealReader.GetInt32(i + 3).ToString();
                                        // i + 3 because the right values start at index 4 in the database
                                        switch (i)
                                        {
                                            case 1:
                                                tempRight1 = rightValue;
                                                break;
                                            case 2:
                                                tempRight2 = rightValue;
                                                break;
                                            case 3:
                                                tempRight3 = rightValue;
                                                break;
                                            case 4:
                                                tempRight4 = rightValue;
                                                break;
                                            case 5:
                                                tempRight5 = rightValue;
                                                break;
                                            case 6:
                                                tempRight6 = rightValue;
                                                break;
                                            case 7:
                                                tempRight7 = rightValue;
                                                break;
                                            case 8:
                                                tempRight8 = rightValue;
                                                break;
                                            case 9:
                                                tempRight9 = rightValue;
                                                break;
                                            case 10:
                                                tempRight10 = rightValue;
                                                break;
                                            case 11:
                                                tempRight11 = rightValue;
                                                break;
                                            case 12:
                                                tempRight12 = rightValue;
                                                break;
                                            case 13:
                                                tempRight13 = rightValue;
                                                break;
                                            case 14:
                                                tempRight14 = rightValue;
                                                break;
                                            case 15:
                                                tempRight15 = rightValue;
                                                break;
                                        }
                                    }
                                    revealReader.Close();
                                }
                            }

                            // delete previous Payment table elements
                            string delQuery = "DELETE FROM [dbo].[TempPayment]";
                            SqlCommand comm2 = new SqlCommand(delQuery, con);
                            comm2.ExecuteNonQuery();

                            // put the data into the archivedpayment table
                            string tempCmdQuery = "INSERT INTO [dbo].[ArchivedPayments]([moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";

                            SqlCommand tempCmdPayment = new SqlCommand(tempCmdQuery, con);

                            // Insert them to archivedpayment
                            tempCmdPayment.Parameters.AddWithValue("@moneyAmount", tempAmountString);
                            tempCmdPayment.Parameters.AddWithValue("@serialNumber", tempSerialNumberString);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber1", tempLeft1);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber1", tempRight1);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber2", tempLeft2);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber2", tempRight2);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber3", tempLeft3);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber3", tempRight3);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber4", tempLeft4);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber4", tempRight4);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber5", tempLeft5);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber5", tempRight5);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber6", tempLeft6);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber6", tempRight6);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber7", tempLeft7);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber7", tempRight7);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber8", tempLeft8);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber8", tempRight8);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber9", tempLeft9);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber9", tempRight9);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber10", tempLeft10);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber10", tempRight10);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber11", tempLeft11);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber11", tempRight11);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber12", tempLeft12);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber12", tempRight12);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber13", tempLeft13);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber13", tempRight13);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber14", tempLeft14);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber14", tempRight14);
                            tempCmdPayment.Parameters.AddWithValue("@leftNumber15", tempLeft15);
                            tempCmdPayment.Parameters.AddWithValue("@rightNumber15", tempRight15);

                            tempCmdPayment.ExecuteNonQuery();
                        }

                        // Give the payment to the bank
                        string paymentQuery = "INSERT INTO [dbo].[ArchivedPayments]([moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";

                        SqlCommand cmdPayment = new SqlCommand(paymentQuery, con);

                        // Insert them to ArchivedPayments
                        cmdPayment.Parameters.AddWithValue("@moneyAmount", amountString);
                        cmdPayment.Parameters.AddWithValue("@serialNumber", serialNumberString);
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

                        // delete previous Payment table elements
                        string deleteQuery = "DELETE FROM [dbo].[Payment]";
                        SqlCommand comm = new SqlCommand(deleteQuery, con);
                        comm.ExecuteNonQuery();


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

                        MessageBox.Show("The merchant received a money order for $" + Amount + "\nThe customer received " + itemsString, "Success");
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

            // delete previous Payment table elements
            string delQuery = "DELETE FROM [dbo].[UnblindedSelection]";
            SqlCommand comm2 = new SqlCommand(delQuery, con);
            comm2.ExecuteNonQuery();


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
        }

        private void HashBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            // get the selection from the table
            string query = "SELECT * FROM [dbo].[Payment]";

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

            for (int i = 1; i <= 15; i++)
            {
                Random rand = new Random();
                int selection = rand.Next(1, 3);

                if (selection == 1)
                {
                    string revealQuery = "SELECT * FROM [dbo].[MoneyOrder] WHERE [serialNumber] = @serialNumber";

                    using SqlCommand revealCmd = new SqlCommand(revealQuery, con);
                    revealCmd.Parameters.AddWithValue("@serialNumber", serialNumberString);
                    using SqlDataReader revealReader = revealCmd.ExecuteReader();

                    if (revealReader.Read())
                    {
                        string leftValue = revealReader.GetInt32(i + 2).ToString();
                        // i + 2 because the left values start at index 3 in the database
                        switch (i)
                        {
                            case 1:
                                left1 = leftValue;
                                break;
                            case 2:
                                left2 = leftValue;
                                break;
                            case 3:
                                left3 = leftValue;
                                break;
                            case 4:
                                left4 = leftValue;
                                break;
                            case 5:
                                left5 = leftValue;
                                break;
                            case 6:
                                left6 = leftValue;
                                break;
                            case 7:
                                left7 = leftValue;
                                break;
                            case 8:
                                left8 = leftValue;
                                break;
                            case 9:
                                left9 = leftValue;
                                break;
                            case 10:
                                left10 = leftValue;
                                break;
                            case 11:
                                left11 = leftValue;
                                break;
                            case 12:
                                left12 = leftValue;
                                break;
                            case 13:
                                left13 = leftValue;
                                break;
                            case 14:
                                left14 = leftValue;
                                break;
                            case 15:
                                left15 = leftValue;
                                break;
                        }
                    }
                    revealReader.Close();
                }
                else if (selection == 2)
                {
                    string revealQuery = "SELECT * FROM [dbo].[MoneyOrder] WHERE [serialNumber] = @serialNumber";

                    using SqlCommand revealCmd = new SqlCommand(revealQuery, con);
                    revealCmd.Parameters.AddWithValue("@serialNumber", serialNumberString);
                    using SqlDataReader revealReader = revealCmd.ExecuteReader();

                    if (revealReader.Read())
                    {
                        string rightValue = revealReader.GetInt32(i + 3).ToString();
                        // i + 3 because the right values start at index 4 in the database
                        switch (i)
                        {
                            case 1:
                                right1 = rightValue;
                                break;
                            case 2:
                                right2 = rightValue;
                                break;
                            case 3:
                                right3 = rightValue;
                                break;
                            case 4:
                                right4 = rightValue;
                                break;
                            case 5:
                                right5 = rightValue;
                                break;
                            case 6:
                                right6 = rightValue;
                                break;
                            case 7:
                                right7 = rightValue;
                                break;
                            case 8:
                                right8 = rightValue;
                                break;
                            case 9:
                                right9 = rightValue;
                                break;
                            case 10:
                                right10 = rightValue;
                                break;
                            case 11:
                                right11 = rightValue;
                                break;
                            case 12:
                                right12 = rightValue;
                                break;
                            case 13:
                                right13 = rightValue;
                                break;
                            case 14:
                                right14 = rightValue;
                                break;
                            case 15:
                                right15 = rightValue;
                                break;
                        }
                    }
                    revealReader.Close();
                }
            }

            // delete previous Payment table elements
            string delQuery = "DELETE FROM [dbo].[Payment]";
            SqlCommand comm2 = new SqlCommand(delQuery, con);
            comm2.ExecuteNonQuery();

            // put the data into the payment table
            string signedQuery = "INSERT INTO [dbo].[Payment]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";

            SqlCommand cmdPayment = new SqlCommand(signedQuery, con);

            // Insert them to payment
            cmdPayment.Parameters.AddWithValue("@index", index);
            cmdPayment.Parameters.AddWithValue("@moneyAmount", amountString);
            cmdPayment.Parameters.AddWithValue("@serialNumber", serialNumberString);
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
        }
    }
}