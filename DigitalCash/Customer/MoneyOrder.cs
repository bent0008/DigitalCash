using Microsoft.VisualBasic.ApplicationServices;
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

            // delete previous CipherMoneyOrder table elements
            string deleteCipherQuery = "DELETE FROM [dbo].[MoneyOrder]";
            SqlCommand comm2 = new SqlCommand(deleteCipherQuery, con);
            comm2.ExecuteNonQuery();

            // load in the customers information
            CustomerForm cust = new();
            // check if the customer has enough money in their balance to make the order
            if (cust.Balance < int.Parse(amount))
            {
                MessageBox.Show("Not enough available funds.", "Error");
            }
            else
            {
                for (int i = 1; i < (occurances + 1); i++)
                {
                    // create random serial number
                    Random rand = new();
                    int serialNum = rand.Next(0, 100000000) + 10000000;

                    // create a random number to be the left and the XOR for the right
                    int left = rand.Next(101, int.MaxValue);
                    int right = cust.ID ^ left;

                    // create a plaintext money order
                    string plainQuery = "INSERT INTO [dbo].[MoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber],[rightNumber]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber,@rightNumber)";
                    SqlCommand plainCmd = new SqlCommand(plainQuery, con);

                    // Insert them to MoneyOrder
                    plainCmd.Parameters.AddWithValue("@index", i);
                    plainCmd.Parameters.AddWithValue("@moneyAmount", amount);
                    plainCmd.Parameters.AddWithValue("@serialNumber", serialNum);
                    plainCmd.Parameters.AddWithValue("@leftNumber", left);
                    plainCmd.Parameters.AddWithValue("@rightNumber", right);

                    plainCmd.ExecuteNonQuery();


                    // call the RSAEncryption class to use its functions
                    RSAEncryption rsa = new();

                    // declare the path to the public key and load it in
                    string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\publickey.xml";
                    rsa.LoadPublicFromXml(publicPath);

                    // Create the blind factor
                    rsa.createBlindFactor();

                    // get the blind factor and set it to a variable
                    string blind = rsa.retrieveBlindFactor();
                    // change the blind factor to a bigint
                    BigInteger blindNum = rsa.ConvertToBigInt(blind);
                    // this is just in case, we set the blind as the bigint number
                    rsa.setBlindFactor(blindNum);

                    // Encrypt the blind factor with the public key and multiply it by the serialNumber
                    string cipherSerial = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(serialNum.ToString()));
                    // Encrypt the blind factor with the public key and multiply it by the amount
                    string cipherAmount = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(amount));


                    // Create command for ArchivedBlinds
                    string blindQuery = "INSERT INTO [dbo].[ArchivedBlinds]([index],[serialNumber],[blind]) VALUES(@index,@serialNumber,@blind)";
                    SqlCommand cmdBlind = new SqlCommand(blindQuery, con);

                    // Add data to ArchivedBlinds
                    cmdBlind.Parameters.AddWithValue("@index", i);
                    cmdBlind.Parameters.AddWithValue("@serialNumber", cipherSerial);
                    cmdBlind.Parameters.AddWithValue("@blind", blind);

                    cmdBlind.ExecuteNonQuery();

                    // create the left and right hash to be changed
                    string leftHash, rightHash;

                    // hash the left and right numbers
                    using (SHA256 sha256 = SHA256Managed.Create())
                    {
                        byte[] leftBytes = Encoding.UTF8.GetBytes(left.ToString());
                        byte[] computedLeftHash = sha256.ComputeHash(leftBytes);
                        leftHash = Convert.ToBase64String(computedLeftHash);

                        byte[] rightBytes = Encoding.UTF8.GetBytes(right.ToString());
                        byte[] computedRightHash = sha256.ComputeHash(rightBytes);
                        rightHash = Convert.ToBase64String(computedRightHash);
                    }

                    // add these to CipherMoneyOrder
                    string query = "INSERT INTO [dbo].[CipherMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber],[rightNumber]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber,@rightNumber)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Insert them to MoneyOrder
                    cmd.Parameters.AddWithValue("@index", i);
                    cmd.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                    cmd.Parameters.AddWithValue("@serialNumber", cipherSerial);
                    cmd.Parameters.AddWithValue("@leftNumber", leftHash);
                    cmd.Parameters.AddWithValue("@rightNumber", rightHash);

                    cmd.ExecuteNonQuery();

                    // add these to ArchivedOrders
                    string archivedQuery = "INSERT INTO [dbo].[ArchivedOrders]([index],[moneyAmount],[serialNumber],[leftNumber],[rightNumber]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber,@rightNumber)";
                    SqlCommand cmd2 = new SqlCommand(archivedQuery, con);

                    // Insert them to ArchivedOrders
                    cmd2.Parameters.AddWithValue("@index", i);
                    cmd2.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                    cmd2.Parameters.AddWithValue("@serialNumber", cipherSerial);
                    cmd.Parameters.AddWithValue("@leftNumber", leftHash);
                    cmd.Parameters.AddWithValue("@rightNumber", rightHash);

                    cmd2.ExecuteNonQuery();
                }
            }
            this.Close();
        }


        // This will create 100 money orders, but 2 of them will have the same amount, meaning that cheating has occurred
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

            // delete previous CipherMoneyOrder table elements
            string deleteCipherQuery = "DELETE FROM [dbo].[MoneyOrder]";
            SqlCommand comm2 = new SqlCommand(deleteCipherQuery, con);
            comm2.ExecuteNonQuery();

            Random randInt = new();
            int randomRow = randInt.Next(2, 98);
            string duplicate = randInt.Next(1, randomRow - 1).ToString();

            int serial = 0;
            string serialNum = "";

            // load in the customers information
            CustomerForm cust = new();
            // check if the customer has enough money in their balance to make the order
            if (cust.Balance < int.Parse(amount))
            {
                MessageBox.Show("Not enough available funds.", "Error");
            }
            else
            {
                for (int i = 0; i < (occurances); i++)
                {
                    // create random serial number
                    Random rand = new();
                    serial = rand.Next(0, 100000000) + 10000000;
                    serialNum = serial.ToString();

                    // create a random number to be the left and the XOR for the right
                    int left = rand.Next(101, int.MaxValue);
                    int right = cust.ID ^ left;


                    // call the RSAEncryption class to use its functions
                    RSAEncryption rsa = new();

                    // declare the path to the public key and load it in
                    string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\publickey.xml";
                    rsa.LoadPublicFromXml(publicPath);

                    // Create the blind factor
                    rsa.createBlindFactor();

                    // get the blind factor and set it to a variable
                    string blind = rsa.retrieveBlindFactor();
                    // change the blind factor to a bigint
                    BigInteger blindNum = rsa.ConvertToBigInt(blind);
                    // this is just in case, we set the blind as the bigint number
                    rsa.setBlindFactor(blindNum);

                    // Encrypt the blind factor with the public key and multiply it by the serialNumber
                    string cipherSerial = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(serialNum.ToString()));
                    // Encrypt the blind factor with the public key and multiply it by the amount
                    string cipherAmount = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(amount));

                    // Create command for ArchivedBlinds
                    string blindQuery = "INSERT INTO [dbo].[ArchivedBlinds]([index],[serialNumber],[blind]) VALUES(@index,@serialNumber,@blind)";
                    SqlCommand cmdBlind = new SqlCommand(blindQuery, con);

                    // Add data to ArchivedBlinds
                    cmdBlind.Parameters.AddWithValue("@index", i);
                    cmdBlind.Parameters.AddWithValue("@serialNumber", cipherSerial);
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

                        // create the left and right hash to be changed
                        string leftHash, rightHash;

                        // hash the left and right numbers
                        using (SHA256 sha256 = SHA256Managed.Create())
                        {
                            byte[] leftBytes = Encoding.UTF8.GetBytes(left.ToString());
                            byte[] computedLeftHash = sha256.ComputeHash(leftBytes);
                            leftHash = Convert.ToBase64String(computedLeftHash);

                            byte[] rightBytes = Encoding.UTF8.GetBytes(right.ToString());
                            byte[] computedRightHash = sha256.ComputeHash(rightBytes);
                            rightHash = Convert.ToBase64String(computedRightHash);
                        }

                        string selectQuery2 = "SELECT * FROM [dbo].[CipherMoneyOrder] WHERE [index] = " + duplicate;
                        SqlCommand selectCmd2 = new SqlCommand(selectQuery2, con);

                        using (SqlDataReader reader = selectCmd2.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cipherAmount = reader.GetString(1);
                                cipherSerial = reader.GetString(2);
                            }
                        }

                        // add these to CipherMoneyOrder
                        string query = "INSERT INTO [dbo].[CipherMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber],[rightNumber]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber,@rightNumber)";
                        SqlCommand cmd = new SqlCommand(query, con);

                        // Insert them to MoneyOrder
                        cmd.Parameters.AddWithValue("@index", i);
                        cmd.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                        cmd.Parameters.AddWithValue("@serialNumber", cipherSerial);
                        cmd.Parameters.AddWithValue("@leftNumber", leftHash);
                        cmd.Parameters.AddWithValue("@rightNumber", rightHash);

                        cmd.ExecuteNonQuery();

                        // add these to ArchivedOrders
                        string queryCheated2 = "INSERT INTO [dbo].[ArchivedOrders]([index],[moneyAmount],[serialNumber],[leftNumber],[rightNumber]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber,@rightNumber)";
                        SqlCommand cmdCheated2 = new SqlCommand(queryCheated2, con);

                        // Insert them to ArchivedOrders
                        cmdCheated2.Parameters.AddWithValue("@index", i);
                        cmdCheated2.Parameters.AddWithValue("@moneyAmount", amount);
                        cmdCheated2.Parameters.AddWithValue("@serialNumber", serialNum);

                        cmdCheated2.ExecuteNonQuery();
                    }
                    else
                    {
                        // create a plaintext money order
                        string plainQuery = "INSERT INTO [dbo].[MoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber],[rightNumber]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber,@rightNumber)";
                        SqlCommand plainCmd = new SqlCommand(plainQuery, con);

                        // Insert them to MoneyOrder
                        plainCmd.Parameters.AddWithValue("@index", i);
                        plainCmd.Parameters.AddWithValue("@moneyAmount", amount);
                        plainCmd.Parameters.AddWithValue("@serialNumber", serialNum);
                        plainCmd.Parameters.AddWithValue("@leftNumber", left);
                        plainCmd.Parameters.AddWithValue("@rightNumber", right);

                        plainCmd.ExecuteNonQuery();

                        // create the left and right hash to be changed
                        string leftHash, rightHash;

                        // hash the left and right numbers
                        using (SHA256 sha256 = SHA256Managed.Create())
                        {
                            byte[] leftBytes = Encoding.UTF8.GetBytes(left.ToString());
                            byte[] computedLeftHash = sha256.ComputeHash(leftBytes);
                            leftHash = Convert.ToBase64String(computedLeftHash);

                            byte[] rightBytes = Encoding.UTF8.GetBytes(right.ToString());
                            byte[] computedRightHash = sha256.ComputeHash(rightBytes);
                            rightHash = Convert.ToBase64String(computedRightHash);
                        }

                        // add these to CipherMoneyOrder
                        string query = "INSERT INTO [dbo].[CipherMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber],[rightNumber]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber,@rightNumber)";
                        SqlCommand cmd = new SqlCommand(query, con);

                        // Insert them to MoneyOrder
                        cmd.Parameters.AddWithValue("@index", i);
                        cmd.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                        cmd.Parameters.AddWithValue("@serialNumber", cipherSerial);
                        cmd.Parameters.AddWithValue("@leftNumber", leftHash);
                        cmd.Parameters.AddWithValue("@rightNumber", rightHash);

                        cmd.ExecuteNonQuery();

                        // add these to ArchivedOrders
                        string archivedQuery = "INSERT INTO [dbo].[ArchivedOrders]([index],[moneyAmount],[serialNumber],[leftNumber],[rightNumber]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber,@rightNumber)";
                        SqlCommand cmd2 = new SqlCommand(archivedQuery, con);

                        // Insert them to ArchivedOrders
                        cmd2.Parameters.AddWithValue("@index", i);
                        cmd2.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                        cmd2.Parameters.AddWithValue("@serialNumber", cipherSerial);
                        cmd2.Parameters.AddWithValue("@leftNumber", leftHash);
                        cmd2.Parameters.AddWithValue("@rightNumber", rightHash);

                        cmd2.ExecuteNonQuery();
                    }
                }
            }
            this.Close();
        }
    }
}
