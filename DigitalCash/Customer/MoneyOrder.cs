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
            string deleteCipherQuery = "DELETE FROM [dbo].[CipherMoneyOrder]";
            SqlCommand comm2 = new SqlCommand(deleteCipherQuery, con);
            comm2.ExecuteNonQuery();

            // delete previous ArchivedBlinds table elements
            string deleteBlindsQuery = "DELETE FROM [dbo].[ArchivedBlinds]";
            SqlCommand comm3 = new SqlCommand(deleteBlindsQuery, con);
            comm3.ExecuteNonQuery();

            // load in the customers information
            CustomerForm cust = new();
            int balance = CustomerForm.SelectedCustomer.Balance;

            // check if the customer has enough money in their balance to make the order
            if (balance < int.Parse(amount))
            {
                MessageBox.Show("Not enough available funds.", "Error");
            }
            else
            {
                for (int i = 0; i < (occurances); i++)
                {
                    // create random serial number
                    Random rand = new();
                    int serialNum = rand.Next(0, 100000000) + 10000000;

                    // store left and rights in lists
                    List<int> leftValues = new List<int>();
                    List<int> rightValues = new List<int>();
                    for (int j = 0; j < 15; j++)
                    {
                        // create a random number to be the left and the XOR for the right
                        int left = rand.Next(101, int.MaxValue);
                        int right = CustomerForm.SelectedCustomer.ID ^ left;

                        leftValues.Add(left);
                        rightValues.Add(right);
                    }


                    // create a plaintext money order
                    string plainQuery = "INSERT INTO [dbo].[MoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
                    using SqlCommand plainCmd = new SqlCommand(plainQuery, con);
                    {
                        // Insert them to MoneyOrder
                        plainCmd.Parameters.AddWithValue("@index", i);
                        plainCmd.Parameters.AddWithValue("@moneyAmount", amount);
                        plainCmd.Parameters.AddWithValue("@serialNumber", serialNum);
                        plainCmd.Parameters.AddWithValue("@leftNumber1", leftValues[0]);
                        plainCmd.Parameters.AddWithValue("@rightNumber1", rightValues[0]);
                        plainCmd.Parameters.AddWithValue("@leftNumber2", leftValues[1]);
                        plainCmd.Parameters.AddWithValue("@rightNumber2", rightValues[1]);
                        plainCmd.Parameters.AddWithValue("@leftNumber3", leftValues[2]);
                        plainCmd.Parameters.AddWithValue("@rightNumber3", rightValues[2]);
                        plainCmd.Parameters.AddWithValue("@leftNumber4", leftValues[3]);
                        plainCmd.Parameters.AddWithValue("@rightNumber4", rightValues[3]);
                        plainCmd.Parameters.AddWithValue("@leftNumber5", leftValues[4]);
                        plainCmd.Parameters.AddWithValue("@rightNumber5", rightValues[4]);
                        plainCmd.Parameters.AddWithValue("@leftNumber6", leftValues[5]);
                        plainCmd.Parameters.AddWithValue("@rightNumber6", rightValues[5]);
                        plainCmd.Parameters.AddWithValue("@leftNumber7", leftValues[6]);
                        plainCmd.Parameters.AddWithValue("@rightNumber7", rightValues[6]);
                        plainCmd.Parameters.AddWithValue("@leftNumber8", leftValues[7]);
                        plainCmd.Parameters.AddWithValue("@rightNumber8", rightValues[7]);
                        plainCmd.Parameters.AddWithValue("@leftNumber9", leftValues[8]);
                        plainCmd.Parameters.AddWithValue("@rightNumber9", rightValues[8]);
                        plainCmd.Parameters.AddWithValue("@leftNumber10", leftValues[9]);
                        plainCmd.Parameters.AddWithValue("@rightNumber10", rightValues[9]);
                        plainCmd.Parameters.AddWithValue("@leftNumber11", leftValues[10]);
                        plainCmd.Parameters.AddWithValue("@rightNumber11", rightValues[10]);
                        plainCmd.Parameters.AddWithValue("@leftNumber12", leftValues[11]);
                        plainCmd.Parameters.AddWithValue("@rightNumber12", rightValues[11]);
                        plainCmd.Parameters.AddWithValue("@leftNumber13", leftValues[12]);
                        plainCmd.Parameters.AddWithValue("@rightNumber13", rightValues[12]);
                        plainCmd.Parameters.AddWithValue("@leftNumber14", leftValues[13]);
                        plainCmd.Parameters.AddWithValue("@rightNumber14", rightValues[13]);
                        plainCmd.Parameters.AddWithValue("@leftNumber15", leftValues[14]);
                        plainCmd.Parameters.AddWithValue("@rightNumber15", rightValues[14]);



                        plainCmd.ExecuteNonQuery();
                    }

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
                    using SqlCommand cmdBlind = new SqlCommand(blindQuery, con);
                    {
                        // Add data to ArchivedBlinds
                        cmdBlind.Parameters.AddWithValue("@index", i);
                        cmdBlind.Parameters.AddWithValue("@serialNumber", serialNum);
                        cmdBlind.Parameters.AddWithValue("@blind", blind);

                        cmdBlind.ExecuteNonQuery();
                    }

                    // create the left and right hash lists
                    List<string> leftHashes = new List<string>();
                    List<string> rightHashes = new List<string>();

                    for (int k = 0; k < 15; k++)
                    {
                        // hash the left and right numbers
                        using (SHA256 sha256 = SHA256.Create())
                        {
                            byte[] leftBytes = Encoding.UTF8.GetBytes(leftValues[k].ToString());
                            byte[] computedLeftHash = sha256.ComputeHash(leftBytes);
                            leftHashes.Add(Convert.ToBase64String(computedLeftHash));

                            byte[] rightBytes = Encoding.UTF8.GetBytes(rightValues[k].ToString());
                            byte[] computedRightHash = sha256.ComputeHash(rightBytes);
                            rightHashes.Add(Convert.ToBase64String(computedRightHash));
                        }
                    }

                    // add these to CipherMoneyOrder
                    string query = "INSERT INTO [dbo].[CipherMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
                    using SqlCommand cmd = new SqlCommand(query, con);
                    {
                        // Insert them to MoneyOrder
                        cmd.Parameters.AddWithValue("@index", i);
                        cmd.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                        cmd.Parameters.AddWithValue("@serialNumber", cipherSerial);
                        cmd.Parameters.AddWithValue("@leftNumber1", leftHashes[0]);
                        cmd.Parameters.AddWithValue("@rightNumber1", rightHashes[0]);
                        cmd.Parameters.AddWithValue("@leftNumber2", leftHashes[1]);
                        cmd.Parameters.AddWithValue("@rightNumber2", rightHashes[1]);
                        cmd.Parameters.AddWithValue("@leftNumber3", leftHashes[2]);
                        cmd.Parameters.AddWithValue("@rightNumber3", rightHashes[2]);
                        cmd.Parameters.AddWithValue("@leftNumber4", leftHashes[3]);
                        cmd.Parameters.AddWithValue("@rightNumber4", rightHashes[3]);
                        cmd.Parameters.AddWithValue("@leftNumber5", leftHashes[4]);
                        cmd.Parameters.AddWithValue("@rightNumber5", rightHashes[4]);
                        cmd.Parameters.AddWithValue("@leftNumber6", leftHashes[5]);
                        cmd.Parameters.AddWithValue("@rightNumber6", rightHashes[5]);
                        cmd.Parameters.AddWithValue("@leftNumber7", leftHashes[6]);
                        cmd.Parameters.AddWithValue("@rightNumber7", rightHashes[6]);
                        cmd.Parameters.AddWithValue("@leftNumber8", leftHashes[7]);
                        cmd.Parameters.AddWithValue("@rightNumber8", rightHashes[7]);
                        cmd.Parameters.AddWithValue("@leftNumber9", leftHashes[8]);
                        cmd.Parameters.AddWithValue("@rightNumber9", rightHashes[8]);
                        cmd.Parameters.AddWithValue("@leftNumber10", leftHashes[9]);
                        cmd.Parameters.AddWithValue("@rightNumber10", rightHashes[9]);
                        cmd.Parameters.AddWithValue("@leftNumber11", leftHashes[10]);
                        cmd.Parameters.AddWithValue("@rightNumber11", rightHashes[10]);
                        cmd.Parameters.AddWithValue("@leftNumber12", leftHashes[11]);
                        cmd.Parameters.AddWithValue("@rightNumber12", rightHashes[11]);
                        cmd.Parameters.AddWithValue("@leftNumber13", leftHashes[12]);
                        cmd.Parameters.AddWithValue("@rightNumber13", rightHashes[12]);
                        cmd.Parameters.AddWithValue("@leftNumber14", leftHashes[13]);
                        cmd.Parameters.AddWithValue("@rightNumber14", rightHashes[13]);
                        cmd.Parameters.AddWithValue("@leftNumber15", leftHashes[14]);
                        cmd.Parameters.AddWithValue("@rightNumber15", rightHashes[14]);

                        cmd.ExecuteNonQuery();
                    }

                    // add these to ArchivedOrders
                    string archivedQuery = "INSERT INTO [dbo].[ArchivedOrders]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
                    using SqlCommand cmd2 = new SqlCommand(archivedQuery, con);
                    {
                        // Insert them to ArchivedOrders
                        cmd2.Parameters.AddWithValue("@index", i);
                        cmd2.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                        cmd2.Parameters.AddWithValue("@serialNumber", cipherSerial);
                        cmd2.Parameters.AddWithValue("@leftNumber1", leftHashes[0]);
                        cmd2.Parameters.AddWithValue("@rightNumber1", rightHashes[0]);
                        cmd2.Parameters.AddWithValue("@leftNumber2", leftHashes[1]);
                        cmd2.Parameters.AddWithValue("@rightNumber2", rightHashes[1]);
                        cmd2.Parameters.AddWithValue("@leftNumber3", leftHashes[2]);
                        cmd2.Parameters.AddWithValue("@rightNumber3", rightHashes[2]);
                        cmd2.Parameters.AddWithValue("@leftNumber4", leftHashes[3]);
                        cmd2.Parameters.AddWithValue("@rightNumber4", rightHashes[3]);
                        cmd2.Parameters.AddWithValue("@leftNumber5", leftHashes[4]);
                        cmd2.Parameters.AddWithValue("@rightNumber5", rightHashes[4]);
                        cmd2.Parameters.AddWithValue("@leftNumber6", leftHashes[5]);
                        cmd2.Parameters.AddWithValue("@rightNumber6", rightHashes[5]);
                        cmd2.Parameters.AddWithValue("@leftNumber7", leftHashes[6]);
                        cmd2.Parameters.AddWithValue("@rightNumber7", rightHashes[6]);
                        cmd2.Parameters.AddWithValue("@leftNumber8", leftHashes[7]);
                        cmd2.Parameters.AddWithValue("@rightNumber8", rightHashes[7]);
                        cmd2.Parameters.AddWithValue("@leftNumber9", leftHashes[8]);
                        cmd2.Parameters.AddWithValue("@rightNumber9", rightHashes[8]);
                        cmd2.Parameters.AddWithValue("@leftNumber10", leftHashes[9]);
                        cmd2.Parameters.AddWithValue("@rightNumber10", rightHashes[9]);
                        cmd2.Parameters.AddWithValue("@leftNumber11", leftHashes[10]);
                        cmd2.Parameters.AddWithValue("@rightNumber11", rightHashes[10]);
                        cmd2.Parameters.AddWithValue("@leftNumber12", leftHashes[11]);
                        cmd2.Parameters.AddWithValue("@rightNumber12", rightHashes[11]);
                        cmd2.Parameters.AddWithValue("@leftNumber13", leftHashes[12]);
                        cmd2.Parameters.AddWithValue("@rightNumber13", rightHashes[12]);
                        cmd2.Parameters.AddWithValue("@leftNumber14", leftHashes[13]);
                        cmd2.Parameters.AddWithValue("@rightNumber14", rightHashes[13]);
                        cmd2.Parameters.AddWithValue("@leftNumber15", leftHashes[14]);
                        cmd2.Parameters.AddWithValue("@rightNumber15", rightHashes[14]);

                        cmd2.ExecuteNonQuery();
                    }
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
                        using (SHA256 sha256 = SHA256.Create())
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
                        using (SHA256 sha256 = SHA256.Create())
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
