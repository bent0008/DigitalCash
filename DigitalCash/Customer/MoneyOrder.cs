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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
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
                MessageBox.Show("You cheated!");
                CheatedMoneyOrder();
            }
            else
            {
                CreateMoneyOrder();
            }

        }


        // This will create 100 money orders that are not cheated
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

            if (encryptChkbx.Checked == false)
            {
                for (int i = 1; i < (occurances + 1); i++)
                {
                    Random rand = new();
                    int serial = rand.Next(0, 100000000) + 10000000;
                    string serialNum = serial.ToString();

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
            }
            else
            {
                for (int i = 1; i < (occurances + 1); i++)
                {
                    // create random serial number
                    Random rand = new();
                    int serialNum = rand.Next(0, 100000000) + 10000000;

                    // call the RSAEncryption class to use its functions
                    RSAEncryption rsaEnc = new();

                    // declare the path to the public key and load it in
                    string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\publickey.xml";
                    rsaEnc.LoadPublicFromXml(publicPath);

                    // Encrypt with blind factor
                    rsaEnc.createBlindFactor();
                    //string blindEncAmount = rsaEnc.PublicBlindEncryption(Encoding.UTF8.GetBytes(amount));
                    //string blindEncSerial = rsaEnc.PublicBlindEncryption(Encoding.UTF8.GetBytes(serialNum.ToString()));

                    // Read the XML string of the private and public key from a file
                    string privateKeyXml = File.ReadAllText("privatekey.xml");
                    string publicKeyXml = File.ReadAllText("publickey.xml");

                    // Load the private key from the XML string
                    RSACryptoServiceProvider rsaPrivate = new RSACryptoServiceProvider();
                    rsaPrivate.FromXmlString(privateKeyXml);

                    // Load the public key from the XML string
                    RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider();
                    rsaPublic.FromXmlString(publicKeyXml);

                    // Convert a plaintext message to a byte array
                    byte[] amountBytes = System.Text.Encoding.UTF8.GetBytes(amount);
                    byte[] serialBytes = System.Text.Encoding.UTF8.GetBytes(serialNum.ToString());

                    // Encrypt the plaintext using the private key
                    byte[] cipherAmountBytes = rsaPrivate.Encrypt(amountBytes, false);
                    byte[] cipherSerialBytes = rsaPrivate.Encrypt(serialBytes, false);
                    string cipherAmount = Convert.ToHexString(cipherAmountBytes);
                    string cipherSerial = Convert.ToHexString(cipherSerialBytes);
                    MessageBox.Show(cipherAmount);

                    // Create command for ArchivedBlinds
                    string blindQuery = "INSERT INTO [dbo].[ArchivedBlinds]([index],[serialNumber],[blind]) VALUES(@index,@serialNumber,@blind)";
                    SqlCommand cmdBlind = new SqlCommand(blindQuery, con);

                    // Set the blind to a variable
                    string blind = rsaEnc.retrieveBlindFactor();

                    // Add data to ArchivedBlinds
                    cmdBlind.Parameters.AddWithValue("@index", i);
                    cmdBlind.Parameters.AddWithValue("@serialNumber", cipherSerial);
                    cmdBlind.Parameters.AddWithValue("@blind", blind);

                    cmdBlind.ExecuteNonQuery();

                    // add these to MoneyOrder
                    string query = "INSERT INTO [dbo].[MoneyOrder]([index],[moneyAmount],[serialNumber]) VALUES(@index,@moneyAmount,@serialNumber)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Insert them to MoneyOrder
                    cmd.Parameters.AddWithValue("@index", i);
                    cmd.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                    cmd.Parameters.AddWithValue("@serialNumber", cipherSerial);

                    cmd.ExecuteNonQuery();

                    // add these to ArchivedOrders
                    string archivedQuery = "INSERT INTO [dbo].[ArchivedOrders]([index],[moneyAmount],[serialNumber]) VALUES(@index,@moneyAmount,@serialNumber)";
                    SqlCommand cmd2 = new SqlCommand(archivedQuery, con);

                    // Insert them to ArchivedOrders
                    cmd2.Parameters.AddWithValue("@index", i);
                    cmd2.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                    cmd2.Parameters.AddWithValue("@serialNumber", cipherSerial);

                    cmd2.ExecuteNonQuery();


                }
            }
            this.Close();
        }


        // This will create 100 money orders, but 2 of them will be the same, meaning that cheating has occurred
        private void CheatedMoneyOrder()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string amount = moneyAmountTxtbx.Text;
            int occurances = 100;


            // delete previous MoneyOrder table elements
            string deleteQuery = "DELETE FROM [dbo].[MoneyOrder]";
            SqlCommand comm = new SqlCommand(deleteQuery, con);
            comm.ExecuteNonQuery();

            Random randInt = new();
            int randomRow = randInt.Next(2, 98);
            string duplicate = randInt.Next(1, randomRow - 1).ToString();

            int serial = 0;
            string serialNum = "";

            if (encryptChkbx.Checked == false)
            {
                for (int i = 1; i < (occurances + 1); i++)
                {
                    if (i == randomRow)
                    {
                        string selectQuery = "SELECT * FROM [dbo].[MoneyOrder] WHERE [index] = " + duplicate;
                        MessageBox.Show(selectQuery);
                        MessageBox.Show(randomRow.ToString());
                        SqlCommand selectCmd = new SqlCommand(selectQuery, con);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                amount = reader.GetString(1);
                                serialNum = reader.GetString(2);
                            }
                        }

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
                    else
                    {
                    Random rand = new();
                    serial = rand.Next(0, 100000000) + 10000000;
                    serialNum = serial.ToString();

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
                }
            }
            else
            {
                for (int i = 1; i < (occurances + 1); i++)
                {
                    // Call the RSACryptoServiceProvider namespace
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

                    // create random serial number
                    Random rand = new();
                    serial = rand.Next(0, 100000000) + 10000000;
                    serialNum = serial.ToString();

                    // call the RSAEncryption class to use its functions
                    RSAEncryption rsaEnc = new();

                    // declare the path to the private key and load it in
                    string privatePath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\privatekey.xml";
                    rsaEnc.LoadPrivateFromXml(privatePath);

                    // Encrypt with blind factor
                    rsaEnc.createBlindFactor();
                    string blindEncAmount = rsaEnc.PublicBlindEncryption(Encoding.UTF8.GetBytes(amount));
                    string blindEncSerial = rsaEnc.PublicBlindEncryption(Encoding.UTF8.GetBytes(serialNum));

                    // Create command for ArchivedBlinds
                    string blindQuery = "INSERT INTO [dbo].[ArchivedBlinds]([index],[serialNumber],[blind]) VALUES(@index,@serialNumber,@blind)";
                    SqlCommand cmdBlind = new SqlCommand(blindQuery, con);

                    // Set the blind to a variable
                    string blind = rsaEnc.retrieveBlindFactor();

                    // Add data to ArchivedBlinds
                    cmdBlind.Parameters.AddWithValue("@index", i);
                    cmdBlind.Parameters.AddWithValue("@serialNumber", blindEncSerial);
                    cmdBlind.Parameters.AddWithValue("@blind", blind);

                    cmdBlind.ExecuteNonQuery();

                    if (i == randomRow)
                    {
                        string selectQuery = "SELECT * FROM [dbo].[MoneyOrder] WHERE [index] = " + duplicate;
                        MessageBox.Show(duplicate, "Duplicate");
                        MessageBox.Show(randomRow.ToString(), "Random Row");

                        SqlCommand selectCmd = new SqlCommand(selectQuery, con);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                amount = reader.GetString(1);
                                serialNum = reader.GetString(2);
                            }
                        }

                        // add these to MoneyOrder
                        string queryCheated = "INSERT INTO [dbo].[MoneyOrder]([index],[moneyAmount],[serialNumber]) VALUES(@index,@moneyAmount,@serialNumber) ORDER BY [index]";
                        SqlCommand cmdCheated = new SqlCommand(queryCheated, con);

                        // Insert them to MoneyOrder
                        cmdCheated.Parameters.AddWithValue("@index", i);
                        cmdCheated.Parameters.AddWithValue("@moneyAmount", amount);
                        cmdCheated.Parameters.AddWithValue("@serialNumber", serialNum);

                        cmdCheated.ExecuteNonQuery();

                        // add these to ArchivedOrders
                        string queryCheated2 = "INSERT INTO [dbo].[ArchivedOrders]([index],[moneyAmount],[serialNumber]) VALUES(@index,@moneyAmount,@serialNumber)";
                        SqlCommand cmdCheated2 = new SqlCommand(queryCheated2, con);

                        // Insert them to ArchivedOrders
                        cmdCheated2.Parameters.AddWithValue("@index", i);
                        cmdCheated2.Parameters.AddWithValue("@moneyAmount", amount);
                        cmdCheated2.Parameters.AddWithValue("@serialNumber", serialNum);

                        cmdCheated2.ExecuteNonQuery();
                    }
                    else
                    {
                        // add these to MoneyOrder
                        string query = "INSERT INTO [dbo].[MoneyOrder]([index],[moneyAmount],[serialNumber]) VALUES(@index,@moneyAmount,@serialNumber)";
                        SqlCommand cmd = new SqlCommand(query, con);

                        // Insert them to MoneyOrder
                        cmd.Parameters.AddWithValue("@index", i);
                        cmd.Parameters.AddWithValue("@moneyAmount", blindEncAmount);
                        cmd.Parameters.AddWithValue("@serialNumber", blindEncSerial);

                        cmd.ExecuteNonQuery();

                        // add these to ArchivedOrders
                        string archivedQuery = "INSERT INTO [dbo].[ArchivedOrders]([index],[moneyAmount],[serialNumber]) VALUES(@index,@moneyAmount,@serialNumber)";
                        SqlCommand cmd2 = new SqlCommand(archivedQuery, con);

                        // Insert them to ArchivedOrders
                        cmd2.Parameters.AddWithValue("@index", i);
                        cmd2.Parameters.AddWithValue("@moneyAmount", blindEncAmount);
                        cmd2.Parameters.AddWithValue("@serialNumber", blindEncSerial);

                        cmd2.ExecuteNonQuery();
                    }
                }
            }
            this.Close();
        }
    }
}
