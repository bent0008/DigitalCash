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

            if (LoggedIn)
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
                    string query = "SELECT * FROM [dbo].[SignedSelection]";

                    int index = 0;
                    string amountString = "";
                    string encryptedAmount = "";
                    string serialNumberString = "";

                    using SqlCommand cmd = new SqlCommand(query, con);
                    using SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        index = reader.GetInt32(0);
                        amountString = reader.GetString(1);
                        encryptedAmount = reader.GetString(2);
                        serialNumberString = reader.GetString(3);
                    }
                    reader.Close();
                    // convert the data to bigints
                    BigInteger encAmount = rsa.ConvertToBigInt(encryptedAmount);
                    BigInteger serialNumber = rsa.ConvertToBigInt(serialNumberString);

                    int amount = Convert.ToInt32(amountString);

                    if (amount <= total)
                    {
                        MessageBox.Show("Insufficient funds.", "Error");
                    }
                    else
                    {
                        // put the data into the signed selection table
                        string signedQuery = "INSERT INTO [dbo].[Payment]([index],[moneyAmount],[encryptedAmount],[serialNumber]) VALUES(@index,@moneyAmount,@encryptedAmount,@serialNumber)";

                        SqlCommand cmdSigned = new SqlCommand(signedQuery, con);

                        // Insert them to MoneyOrder
                        cmdSigned.Parameters.AddWithValue("@index", index);
                        cmdSigned.Parameters.AddWithValue("@moneyAmount", amountString);
                        cmdSigned.Parameters.AddWithValue("@encryptedAmount", encryptedAmount);
                        cmdSigned.Parameters.AddWithValue("@serialNumber", serialNumberString);

                        cmdSigned.ExecuteNonQuery();

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


                        MessageBox.Show("The merchant received a money order for $" + amount + "\nThe customer received " + itemsString, "Success");
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Please select an item.", "Error");
                }
            }
            else
            {
                MessageBox.Show("Please log in.", "Error");
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


            // declare the path to the public key and load it in
            string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Merchant\bin\Debug\net7.0-windows\publickey.xml";
            rsa.LoadPublicFromXml(publicPath);

            byte[] amount = rsa.reveal(rsa.ConvertToBigInt(amountString));
            byte[] serialNumber = rsa.reveal(rsa.ConvertToBigInt(serialNumberString));

            MessageBox.Show($"Amount: {Encoding.UTF8.GetString(amount)}\nSerial Number: {Encoding.UTF8.GetString(serialNumber)}");
        }
    }
}