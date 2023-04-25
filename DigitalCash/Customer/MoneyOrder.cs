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
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Customer
{
    public partial class MoneyOrder : Form
    {
        public MoneyOrder()
        {
            InitializeComponent();
        }


        public string connectionString = "Data Source=LAPTOP-UOPDFGH4\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";
        public event EventHandler OrderComplete;


        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (cheatChkbx.Checked == true)
            {
                CheatedMoneyOrder();
            }
            else
            {
                CreateMoneyOrder();
            }

            OnOrderComplete();
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

            // delete previous CipherMoneyOrder table elements
            string deleteHashQuery = "DELETE FROM [dbo].[HashMoneyOrder]";
            SqlCommand comm3 = new SqlCommand(deleteHashQuery, con);
            comm3.ExecuteNonQuery();

            // delete previous ArchivedBlinds table elements
            string deleteBlindsQuery = "DELETE FROM [dbo].[ArchivedBlinds]";
            SqlCommand comm4 = new SqlCommand(deleteBlindsQuery, con);
            comm4.ExecuteNonQuery();

            // load in the customers information
            int balance = CustomerForm.SelectedCustomer.Balance;

            // make sure the amount text box is not empty (numeric check is in designer)
            if (string.IsNullOrEmpty(moneyAmountTxtbx.Text))
            {
                MessageBox.Show("Please enter a valid amount.", "Error");
                moneyAmountTxtbx.Text = string.Empty;
            }
            // Check if the text box contains non-numeric characters
            else if (!System.Text.RegularExpressions.Regex.IsMatch(moneyAmountTxtbx.Text, "^[0-9]*$"))
            {
                MessageBox.Show("Please enter only numeric characters.", "Error");
                moneyAmountTxtbx.Text = string.Empty;
            }
            // check if the customer has enough money in their balance to make the order
            else if (balance < int.Parse(amount))
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
                    string blindQuery = "INSERT INTO [dbo].[ArchivedBlinds]([index],[serialNumber],[encryptedSerialNumber],[blind]) VALUES(@index,@serialNumber,@encryptedSerialNumber,@blind)";
                    using SqlCommand cmdBlind = new SqlCommand(blindQuery, con);
                    {
                        // Add data to ArchivedBlinds
                        cmdBlind.Parameters.AddWithValue("@index", i);
                        cmdBlind.Parameters.AddWithValue("@serialNumber", serialNum);
                        cmdBlind.Parameters.AddWithValue("@encryptedSerialNumber", cipherSerial);
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

                    // add these to HashMoneyOrder
                    string query = "INSERT INTO [dbo].[HashMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
                    using SqlCommand cmd = new SqlCommand(query, con);
                    {
                        // Insert them to MoneyOrder
                        cmd.Parameters.AddWithValue("@index", i);
                        cmd.Parameters.AddWithValue("@moneyAmount", amount);
                        cmd.Parameters.AddWithValue("@serialNumber", serialNum);
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

                    // create the left and right blind encrypted hash lists
                    List<string> leftCipher = new List<string>();
                    List<string> rightCipher = new List<string>();

                    for (int l = 0; l < 15; l++)
                    {
                        // this is just in case, we set the blind as the bigint number
                        rsa.setBlindFactor(blindNum);

                        // Encrypt the blind factor with the public key and multiply it by the left and rights
                        string encLeft = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(leftHashes[l]));
                        string encright = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(rightHashes[l]));

                        leftCipher.Add(encLeft);
                        rightCipher.Add(encright);
                    }

                    // add these to CipherMoneyOrder
                    string cipherQuery = "INSERT INTO [dbo].[CipherMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
                    using SqlCommand cipherCmd = new SqlCommand(cipherQuery, con);
                    {
                        // Insert them to MoneyOrder
                        cipherCmd.Parameters.AddWithValue("@index", i);
                        cipherCmd.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                        cipherCmd.Parameters.AddWithValue("@serialNumber", cipherSerial);
                        cipherCmd.Parameters.AddWithValue("@leftNumber1", leftCipher[0]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber1", rightCipher[0]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber2", leftCipher[1]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber2", rightCipher[1]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber3", leftCipher[2]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber3", rightCipher[2]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber4", leftCipher[3]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber4", rightCipher[3]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber5", leftCipher[4]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber5", rightCipher[4]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber6", leftCipher[5]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber6", rightCipher[5]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber7", leftCipher[6]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber7", rightCipher[6]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber8", leftCipher[7]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber8", rightCipher[7]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber9", leftCipher[8]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber9", rightCipher[8]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber10", leftCipher[9]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber10", rightCipher[9]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber11", leftCipher[10]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber11", rightCipher[10]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber12", leftCipher[11]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber12", rightCipher[11]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber13", leftCipher[12]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber13", rightCipher[12]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber14", leftCipher[13]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber14", rightCipher[13]);
                        cipherCmd.Parameters.AddWithValue("@leftNumber15", leftCipher[14]);
                        cipherCmd.Parameters.AddWithValue("@rightNumber15", rightCipher[14]);

                        cipherCmd.ExecuteNonQuery();
                    }

                    progressBar.Value += 1;
                }

                CustomerForm.SelectedCustomer.FraudType = "No cheating occurred";
            }
            this.Close();
        }

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
            string deleteCipherQuery = "DELETE FROM [dbo].[CipherMoneyOrder]";
            SqlCommand comm2 = new SqlCommand(deleteCipherQuery, con);
            comm2.ExecuteNonQuery();

            // delete previous HashMoneyOrder table elements
            string deleteHashQuery = "DELETE FROM [dbo].[HashMoneyOrder]";
            SqlCommand comm3 = new SqlCommand(deleteHashQuery, con);
            comm3.ExecuteNonQuery();

            // delete previous ArchivedBlinds table elements
            string deleteBlindsQuery = "DELETE FROM [dbo].[ArchivedBlinds]";
            SqlCommand comm4 = new SqlCommand(deleteBlindsQuery, con);
            comm4.ExecuteNonQuery();

            Random randInt = new();
            int randomRow = randInt.Next(2, 98);
            string duplicate = randInt.Next(1, randomRow - 1).ToString();

            int serial = 0;
            string serialNum = "";

            // load in the customers information
            int balance = CustomerForm.SelectedCustomer.Balance;

            // make sure the amount text box is not empty
            if (string.IsNullOrEmpty(moneyAmountTxtbx.Text))
            {
                MessageBox.Show("Please enter a valid amount.", "Error");
                moneyAmountTxtbx.Text = string.Empty;
            }
            // Check if the text box contains non-numeric characters
            else if (!System.Text.RegularExpressions.Regex.IsMatch(moneyAmountTxtbx.Text, "^[0-9]*$"))
            {
                MessageBox.Show("Please enter only numeric characters.", "Error");
                moneyAmountTxtbx.Text = string.Empty;
            }
            // check if the customer has enough money in their balance to make the order
            else if (balance < int.Parse(amount))
            {
                MessageBox.Show("Not enough available funds.", "Error");
            }
            else
            {
                // make random number to determine how to cheat
                int cheatType = randInt.Next(1, 5);
                if (cheatType == 1)
                {
                    IDCheat(amount);
                    CustomerForm.SelectedCustomer.FraudType = "The ID was cheated";
                }
                else if (cheatType == 2)
                {
                    HashCheat(amount);
                    CustomerForm.SelectedCustomer.FraudType = "The hashed left and rights were cheated";
                }
                else
                {
                    for (int i = 0; i < (occurances); i++)
                    {
                        // call the RSAEncryption class to use its functions
                        RSAEncryption rsa = new();

                        if (i == randomRow)
                        {
                            if (cheatType == 3)
                            {
                                SerialCheat(i);
                                CustomerForm.SelectedCustomer.FraudType = "There is a duplicate serial number";
                            }
                            else if (cheatType == 4)
                            {
                                AmountCheat(i, amount);
                                CustomerForm.SelectedCustomer.FraudType = "An amount is different from the rest";
                            }
                        }
                        else
                        {
                            // create random serial number
                            serial = randInt.Next(0, 100000000) + 10000000;

                            // store left and rights in lists
                            List<int> leftValues = new List<int>();
                            List<int> rightValues = new List<int>();
                            for (int j = 0; j < 15; j++)
                            {
                                // create a random number to be the left and the XOR for the right
                                int left = randInt.Next(101, int.MaxValue);
                                int right = CustomerForm.SelectedCustomer.ID ^ left;

                                leftValues.Add(left);
                                rightValues.Add(right);
                            }

                            // create a plaintext money order
                            string plainQuery3 = "INSERT INTO [dbo].[MoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
                            using SqlCommand plainCmd3 = new SqlCommand(plainQuery3, con);
                            {
                                // Insert them to MoneyOrder
                                plainCmd3.Parameters.AddWithValue("@index", i);
                                plainCmd3.Parameters.AddWithValue("@moneyAmount", amount);
                                plainCmd3.Parameters.AddWithValue("@serialNumber", serial);
                                plainCmd3.Parameters.AddWithValue("@leftNumber1", leftValues[0]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber1", rightValues[0]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber2", leftValues[1]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber2", rightValues[1]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber3", leftValues[2]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber3", rightValues[2]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber4", leftValues[3]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber4", rightValues[3]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber5", leftValues[4]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber5", rightValues[4]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber6", leftValues[5]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber6", rightValues[5]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber7", leftValues[6]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber7", rightValues[6]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber8", leftValues[7]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber8", rightValues[7]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber9", leftValues[8]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber9", rightValues[8]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber10", leftValues[9]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber10", rightValues[9]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber11", leftValues[10]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber11", rightValues[10]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber12", leftValues[11]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber12", rightValues[11]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber13", leftValues[12]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber13", rightValues[12]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber14", leftValues[13]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber14", rightValues[13]);
                                plainCmd3.Parameters.AddWithValue("@leftNumber15", leftValues[14]);
                                plainCmd3.Parameters.AddWithValue("@rightNumber15", rightValues[14]);

                                plainCmd3.ExecuteNonQuery();
                            }

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
                            string cipherSerial = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(serial.ToString()));
                            // Encrypt the blind factor with the public key and multiply it by the amount
                            string cipherAmount = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(amount));

                            // Create command for ArchivedBlinds
                            string blindQuery = "INSERT INTO [dbo].[ArchivedBlinds]([index],[serialNumber],[encryptedSerialNumber],[blind]) VALUES(@index,@serialNumber,@encryptedSerialNumber,@blind)";
                            using SqlCommand cmdBlind = new SqlCommand(blindQuery, con);
                            {
                                // Add data to ArchivedBlinds
                                cmdBlind.Parameters.AddWithValue("@index", i);
                                cmdBlind.Parameters.AddWithValue("@serialNumber", serial);
                                cmdBlind.Parameters.AddWithValue("@encryptedSerialNumber", cipherSerial);
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

                            // add these to HashMoneyOrder
                            string query = "INSERT INTO [dbo].[HashMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
                            using SqlCommand cmd = new SqlCommand(query, con);
                            {
                                // Insert them to MoneyOrder
                                cmd.Parameters.AddWithValue("@index", i);
                                cmd.Parameters.AddWithValue("@moneyAmount", amount);
                                cmd.Parameters.AddWithValue("@serialNumber", serialNum);
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

                            // create the left and right blind encrypted hash lists
                            List<string> leftCipher = new List<string>();
                            List<string> rightCipher = new List<string>();

                            for (int l = 0; l < 15; l++)
                            {
                                // this is just in case, we set the blind as the bigint number
                                rsa.setBlindFactor(blindNum);

                                // Encrypt the blind factor with the public key and multiply it by the left and rights
                                string encLeft = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(leftHashes[l]));
                                string encright = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(rightHashes[l]));

                                leftCipher.Add(encLeft);
                                rightCipher.Add(encright);
                            }

                            // add these to CipherMoneyOrder
                            string cipherQuery = "INSERT INTO [dbo].[CipherMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
                            using SqlCommand cipherCmd = new SqlCommand(cipherQuery, con);
                            {
                                // Insert them to MoneyOrder
                                cipherCmd.Parameters.AddWithValue("@index", i);
                                cipherCmd.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                                cipherCmd.Parameters.AddWithValue("@serialNumber", cipherSerial);
                                cipherCmd.Parameters.AddWithValue("@leftNumber1", leftCipher[0]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber1", rightCipher[0]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber2", leftCipher[1]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber2", rightCipher[1]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber3", leftCipher[2]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber3", rightCipher[2]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber4", leftCipher[3]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber4", rightCipher[3]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber5", leftCipher[4]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber5", rightCipher[4]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber6", leftCipher[5]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber6", rightCipher[5]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber7", leftCipher[6]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber7", rightCipher[6]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber8", leftCipher[7]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber8", rightCipher[7]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber9", leftCipher[8]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber9", rightCipher[8]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber10", leftCipher[9]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber10", rightCipher[9]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber11", leftCipher[10]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber11", rightCipher[10]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber12", leftCipher[11]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber12", rightCipher[11]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber13", leftCipher[12]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber13", rightCipher[12]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber14", leftCipher[13]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber14", rightCipher[13]);
                                cipherCmd.Parameters.AddWithValue("@leftNumber15", leftCipher[14]);
                                cipherCmd.Parameters.AddWithValue("@rightNumber15", rightCipher[14]);

                                cipherCmd.ExecuteNonQuery();
                            }
                        }

                        if (progressBar.Value < 99)
                        {
                            progressBar.Value += 1;
                        }
                    }
                }
            }
            this.Close();
        }

        private void SerialCheat(int i)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            Random randInt = new();
            string duplicate = randInt.Next(1, i - 1).ToString();

            string amount = "";
            string serialNum = "";
            int left1 = 0;
            int right1 = 0;
            int left2 = 0;
            int right2 = 0;
            int left3 = 0;
            int right3 = 0;
            int left4 = 0;
            int right4 = 0;
            int left5 = 0;
            int right5 = 0;
            int left6 = 0;
            int right6 = 0;
            int left7 = 0;
            int right7 = 0;
            int left8 = 0;
            int right8 = 0;
            int left9 = 0;
            int right9 = 0;
            int left10 = 0;
            int right10 = 0;
            int left11 = 0;
            int right11 = 0;
            int left12 = 0;
            int right12 = 0;
            int left13 = 0;
            int right13 = 0;
            int left14 = 0;
            int right14 = 0;
            int left15 = 0;
            int right15 = 0;

            // call the RSAEncryption class to use its functions
            RSAEncryption rsa = new();

            string selectQuery = $"SELECT * FROM [dbo].[MoneyOrder] WHERE [index] = {duplicate}";

            SqlCommand selectCmd = new SqlCommand(selectQuery, con);

            using (SqlDataReader reader = selectCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    amount = reader.GetString(1);
                    serialNum = reader.GetString(2);
                    left1 = reader.GetInt32(3);
                    right1 = reader.GetInt32(4);
                    left2 = reader.GetInt32(5);
                    right2 = reader.GetInt32(6);
                    left3 = reader.GetInt32(7);
                    right3 = reader.GetInt32(8);
                    left4 = reader.GetInt32(9);
                    right4 = reader.GetInt32(10);
                    left5 = reader.GetInt32(11);
                    right5 = reader.GetInt32(12);
                    left6 = reader.GetInt32(13);
                    right6 = reader.GetInt32(14);
                    left7 = reader.GetInt32(15);
                    right7 = reader.GetInt32(16);
                    left8 = reader.GetInt32(17);
                    right8 = reader.GetInt32(18);
                    left9 = reader.GetInt32(19);
                    right9 = reader.GetInt32(20);
                    left10 = reader.GetInt32(21);
                    right10 = reader.GetInt32(22);
                    left11 = reader.GetInt32(23);
                    right11 = reader.GetInt32(24);
                    left12 = reader.GetInt32(25);
                    right12 = reader.GetInt32(26);
                    left13 = reader.GetInt32(27);
                    right13 = reader.GetInt32(28);
                    left14 = reader.GetInt32(29);
                    right14 = reader.GetInt32(30);
                    left15 = reader.GetInt32(31);
                    right15 = reader.GetInt32(32);
                }
            }

            // add the left and rights to a list
            List<int> leftValues = new();
            List<int> rightValues = new();

            leftValues.AddRange(new int[] { left1, left2, left3, left4, left5, left6, left7, left8, left9, left10, left11, left12, left13, left14, left15 });
            rightValues.AddRange(new int[] { right1, right2, right3, right4, right5, right6, right7, right8, right9, right10, right11, right12, right13, right14, right15 });

            // create a plaintext money order
            string plainQuery2 = "INSERT INTO [dbo].[MoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
            using SqlCommand plainCmd2 = new SqlCommand(plainQuery2, con);
            {
                // Insert them to MoneyOrder
                plainCmd2.Parameters.AddWithValue("@index", i);
                plainCmd2.Parameters.AddWithValue("@moneyAmount", amount);
                plainCmd2.Parameters.AddWithValue("@serialNumber", serialNum);
                plainCmd2.Parameters.AddWithValue("@leftNumber1", left1);
                plainCmd2.Parameters.AddWithValue("@rightNumber1", right1);
                plainCmd2.Parameters.AddWithValue("@leftNumber2", left2);
                plainCmd2.Parameters.AddWithValue("@rightNumber2", right2);
                plainCmd2.Parameters.AddWithValue("@leftNumber3", left3);
                plainCmd2.Parameters.AddWithValue("@rightNumber3", right3);
                plainCmd2.Parameters.AddWithValue("@leftNumber4", left4);
                plainCmd2.Parameters.AddWithValue("@rightNumber4", right4);
                plainCmd2.Parameters.AddWithValue("@leftNumber5", left5);
                plainCmd2.Parameters.AddWithValue("@rightNumber5", right5);
                plainCmd2.Parameters.AddWithValue("@leftNumber6", left6);
                plainCmd2.Parameters.AddWithValue("@rightNumber6", right6);
                plainCmd2.Parameters.AddWithValue("@leftNumber7", left7);
                plainCmd2.Parameters.AddWithValue("@rightNumber7", right7);
                plainCmd2.Parameters.AddWithValue("@leftNumber8", left8);
                plainCmd2.Parameters.AddWithValue("@rightNumber8", right8);
                plainCmd2.Parameters.AddWithValue("@leftNumber9", left9);
                plainCmd2.Parameters.AddWithValue("@rightNumber9", right9);
                plainCmd2.Parameters.AddWithValue("@leftNumber10", left10);
                plainCmd2.Parameters.AddWithValue("@rightNumber10", right10);
                plainCmd2.Parameters.AddWithValue("@leftNumber11", left11);
                plainCmd2.Parameters.AddWithValue("@rightNumber11", right11);
                plainCmd2.Parameters.AddWithValue("@leftNumber12", left12);
                plainCmd2.Parameters.AddWithValue("@rightNumber12", right12);
                plainCmd2.Parameters.AddWithValue("@leftNumber13", left13);
                plainCmd2.Parameters.AddWithValue("@rightNumber13", right13);
                plainCmd2.Parameters.AddWithValue("@leftNumber14", left14);
                plainCmd2.Parameters.AddWithValue("@rightNumber14", right14);
                plainCmd2.Parameters.AddWithValue("@leftNumber15", left15);
                plainCmd2.Parameters.AddWithValue("@rightNumber15", right15);

                plainCmd2.ExecuteNonQuery();
            }

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
            string blindQuery = "INSERT INTO [dbo].[ArchivedBlinds]([index],[serialNumber],[encryptedSerialNumber],[blind]) VALUES(@index,@serialNumber,@encryptedSerialNumber,@blind)";
            using SqlCommand cmdBlind = new SqlCommand(blindQuery, con);
            {
                // Add data to ArchivedBlinds
                cmdBlind.Parameters.AddWithValue("@index", i);
                cmdBlind.Parameters.AddWithValue("@serialNumber", serialNum);
                cmdBlind.Parameters.AddWithValue("@encryptedSerialNumber", cipherSerial);
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

            // add these to HashMoneyOrder
            string query = "INSERT INTO [dbo].[HashMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
            using SqlCommand cmd = new SqlCommand(query, con);
            {
                // Insert them to MoneyOrder
                cmd.Parameters.AddWithValue("@index", i);
                cmd.Parameters.AddWithValue("@moneyAmount", amount);
                cmd.Parameters.AddWithValue("@serialNumber", serialNum);
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

            // create the left and right blind encrypted hash lists
            List<string> leftCipher = new List<string>();
            List<string> rightCipher = new List<string>();

            for (int l = 0; l < 15; l++)
            {
                // this is just in case, we set the blind as the bigint number
                rsa.setBlindFactor(blindNum);

                // Encrypt the blind factor with the public key and multiply it by the left and rights
                string encLeft = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(leftHashes[l]));
                string encright = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(rightHashes[l]));

                leftCipher.Add(encLeft);
                rightCipher.Add(encright);
            }

            // add these to CipherMoneyOrder
            string cipherQuery = "INSERT INTO [dbo].[CipherMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
            using SqlCommand cipherCmd = new SqlCommand(cipherQuery, con);
            {
                // Insert them to MoneyOrder
                cipherCmd.Parameters.AddWithValue("@index", i);
                cipherCmd.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                cipherCmd.Parameters.AddWithValue("@serialNumber", cipherSerial);
                cipherCmd.Parameters.AddWithValue("@leftNumber1", leftCipher[0]);
                cipherCmd.Parameters.AddWithValue("@rightNumber1", rightCipher[0]);
                cipherCmd.Parameters.AddWithValue("@leftNumber2", leftCipher[1]);
                cipherCmd.Parameters.AddWithValue("@rightNumber2", rightCipher[1]);
                cipherCmd.Parameters.AddWithValue("@leftNumber3", leftCipher[2]);
                cipherCmd.Parameters.AddWithValue("@rightNumber3", rightCipher[2]);
                cipherCmd.Parameters.AddWithValue("@leftNumber4", leftCipher[3]);
                cipherCmd.Parameters.AddWithValue("@rightNumber4", rightCipher[3]);
                cipherCmd.Parameters.AddWithValue("@leftNumber5", leftCipher[4]);
                cipherCmd.Parameters.AddWithValue("@rightNumber5", rightCipher[4]);
                cipherCmd.Parameters.AddWithValue("@leftNumber6", leftCipher[5]);
                cipherCmd.Parameters.AddWithValue("@rightNumber6", rightCipher[5]);
                cipherCmd.Parameters.AddWithValue("@leftNumber7", leftCipher[6]);
                cipherCmd.Parameters.AddWithValue("@rightNumber7", rightCipher[6]);
                cipherCmd.Parameters.AddWithValue("@leftNumber8", leftCipher[7]);
                cipherCmd.Parameters.AddWithValue("@rightNumber8", rightCipher[7]);
                cipherCmd.Parameters.AddWithValue("@leftNumber9", leftCipher[8]);
                cipherCmd.Parameters.AddWithValue("@rightNumber9", rightCipher[8]);
                cipherCmd.Parameters.AddWithValue("@leftNumber10", leftCipher[9]);
                cipherCmd.Parameters.AddWithValue("@rightNumber10", rightCipher[9]);
                cipherCmd.Parameters.AddWithValue("@leftNumber11", leftCipher[10]);
                cipherCmd.Parameters.AddWithValue("@rightNumber11", rightCipher[10]);
                cipherCmd.Parameters.AddWithValue("@leftNumber12", leftCipher[11]);
                cipherCmd.Parameters.AddWithValue("@rightNumber12", rightCipher[11]);
                cipherCmd.Parameters.AddWithValue("@leftNumber13", leftCipher[12]);
                cipherCmd.Parameters.AddWithValue("@rightNumber13", rightCipher[12]);
                cipherCmd.Parameters.AddWithValue("@leftNumber14", leftCipher[13]);
                cipherCmd.Parameters.AddWithValue("@rightNumber14", rightCipher[13]);
                cipherCmd.Parameters.AddWithValue("@leftNumber15", leftCipher[14]);
                cipherCmd.Parameters.AddWithValue("@rightNumber15", rightCipher[14]);

                cipherCmd.ExecuteNonQuery();
            }

            progressBar.Value += 1;
        }

        private void AmountCheat(int i, string amount)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            // create random serial number
            Random rand = new();
            int serialNum = rand.Next(0, 100000000) + 10000000;

            // cheat amount to be multiplied randomly
            amount = (int.Parse(amount) * rand.Next(2, 10)).ToString();

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
            string blindQuery = "INSERT INTO [dbo].[ArchivedBlinds]([index],[serialNumber],[encryptedSerialNumber],[blind]) VALUES(@index,@serialNumber,@encryptedSerialNumber,@blind)";
            using SqlCommand cmdBlind = new SqlCommand(blindQuery, con);
            {
                // Add data to ArchivedBlinds
                cmdBlind.Parameters.AddWithValue("@index", i);
                cmdBlind.Parameters.AddWithValue("@serialNumber", serialNum);
                cmdBlind.Parameters.AddWithValue("@encryptedSerialNumber", cipherSerial);
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

            // add these to HashMoneyOrder
            string query = "INSERT INTO [dbo].[HashMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
            using SqlCommand cmd = new SqlCommand(query, con);
            {
                // Insert them to MoneyOrder
                cmd.Parameters.AddWithValue("@index", i);
                cmd.Parameters.AddWithValue("@moneyAmount", amount);
                cmd.Parameters.AddWithValue("@serialNumber", serialNum);
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

            // create the left and right blind encrypted hash lists
            List<string> leftCipher = new List<string>();
            List<string> rightCipher = new List<string>();

            for (int l = 0; l < 15; l++)
            {
                // this is just in case, we set the blind as the bigint number
                rsa.setBlindFactor(blindNum);

                // Encrypt the blind factor with the public key and multiply it by the left and rights
                string encLeft = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(leftHashes[l]));
                string encright = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(rightHashes[l]));

                leftCipher.Add(encLeft);
                rightCipher.Add(encright);
            }

            // add these to CipherMoneyOrder
            string cipherQuery = "INSERT INTO [dbo].[CipherMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
            using SqlCommand cipherCmd = new SqlCommand(cipherQuery, con);
            {
                // Insert them to MoneyOrder
                cipherCmd.Parameters.AddWithValue("@index", i);
                cipherCmd.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                cipherCmd.Parameters.AddWithValue("@serialNumber", cipherSerial);
                cipherCmd.Parameters.AddWithValue("@leftNumber1", leftCipher[0]);
                cipherCmd.Parameters.AddWithValue("@rightNumber1", rightCipher[0]);
                cipherCmd.Parameters.AddWithValue("@leftNumber2", leftCipher[1]);
                cipherCmd.Parameters.AddWithValue("@rightNumber2", rightCipher[1]);
                cipherCmd.Parameters.AddWithValue("@leftNumber3", leftCipher[2]);
                cipherCmd.Parameters.AddWithValue("@rightNumber3", rightCipher[2]);
                cipherCmd.Parameters.AddWithValue("@leftNumber4", leftCipher[3]);
                cipherCmd.Parameters.AddWithValue("@rightNumber4", rightCipher[3]);
                cipherCmd.Parameters.AddWithValue("@leftNumber5", leftCipher[4]);
                cipherCmd.Parameters.AddWithValue("@rightNumber5", rightCipher[4]);
                cipherCmd.Parameters.AddWithValue("@leftNumber6", leftCipher[5]);
                cipherCmd.Parameters.AddWithValue("@rightNumber6", rightCipher[5]);
                cipherCmd.Parameters.AddWithValue("@leftNumber7", leftCipher[6]);
                cipherCmd.Parameters.AddWithValue("@rightNumber7", rightCipher[6]);
                cipherCmd.Parameters.AddWithValue("@leftNumber8", leftCipher[7]);
                cipherCmd.Parameters.AddWithValue("@rightNumber8", rightCipher[7]);
                cipherCmd.Parameters.AddWithValue("@leftNumber9", leftCipher[8]);
                cipherCmd.Parameters.AddWithValue("@rightNumber9", rightCipher[8]);
                cipherCmd.Parameters.AddWithValue("@leftNumber10", leftCipher[9]);
                cipherCmd.Parameters.AddWithValue("@rightNumber10", rightCipher[9]);
                cipherCmd.Parameters.AddWithValue("@leftNumber11", leftCipher[10]);
                cipherCmd.Parameters.AddWithValue("@rightNumber11", rightCipher[10]);
                cipherCmd.Parameters.AddWithValue("@leftNumber12", leftCipher[11]);
                cipherCmd.Parameters.AddWithValue("@rightNumber12", rightCipher[11]);
                cipherCmd.Parameters.AddWithValue("@leftNumber13", leftCipher[12]);
                cipherCmd.Parameters.AddWithValue("@rightNumber13", rightCipher[12]);
                cipherCmd.Parameters.AddWithValue("@leftNumber14", leftCipher[13]);
                cipherCmd.Parameters.AddWithValue("@rightNumber14", rightCipher[13]);
                cipherCmd.Parameters.AddWithValue("@leftNumber15", leftCipher[14]);
                cipherCmd.Parameters.AddWithValue("@rightNumber15", rightCipher[14]);

                cipherCmd.ExecuteNonQuery();
            }

            progressBar.Value += 1;
        }

        private void IDCheat(string amount)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            int occurances = 100;

            // create fakeID to be updated
            int fakeID = 0;

            // randomly have the same ID or a different one
            Random randNum = new();
            int sameID = randNum.Next(1, 3);
            if (sameID == 1)
            {
                // create a random fake customer ID
                Random rand = new();
                fakeID = rand.Next(100000000, 999999999);
                
            }
            for (int i = 0; i < (occurances); i++)
            {
                // create random serial number
                Random rand = new();
                int serialNum = rand.Next(0, 100000000) + 10000000;

                // create new fakeID every moneyOrder
                if (sameID == 2)
                {
                    fakeID = rand.Next(100000000, 999999999);
                }

                // store left and rights in lists
                List<int> leftValues = new List<int>();
                List<int> rightValues = new List<int>();
                for (int j = 0; j < 15; j++)
                {
                    // create a random number to be the left and the XOR for the right
                    int left = rand.Next(101, int.MaxValue);
                    int right = fakeID ^ left;

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
                string blindQuery = "INSERT INTO [dbo].[ArchivedBlinds]([index],[serialNumber],[encryptedSerialNumber],[blind]) VALUES(@index,@serialNumber,@encryptedSerialNumber,@blind)";
                using SqlCommand cmdBlind = new SqlCommand(blindQuery, con);
                {
                    // Add data to ArchivedBlinds
                    cmdBlind.Parameters.AddWithValue("@index", i);
                    cmdBlind.Parameters.AddWithValue("@serialNumber", serialNum);
                    cmdBlind.Parameters.AddWithValue("@encryptedSerialNumber", cipherSerial);
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

                // add these to HashMoneyOrder
                string query = "INSERT INTO [dbo].[HashMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
                using SqlCommand cmd = new SqlCommand(query, con);
                {
                    // Insert them to MoneyOrder
                    cmd.Parameters.AddWithValue("@index", i);
                    cmd.Parameters.AddWithValue("@moneyAmount", amount);
                    cmd.Parameters.AddWithValue("@serialNumber", serialNum);
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

                // create the left and right blind encrypted hash lists
                List<string> leftCipher = new List<string>();
                List<string> rightCipher = new List<string>();

                for (int l = 0; l < 15; l++)
                {
                    // this is just in case, we set the blind as the bigint number
                    rsa.setBlindFactor(blindNum);

                    // Encrypt the blind factor with the public key and multiply it by the left and rights
                    string encLeft = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(leftHashes[l]));
                    string encright = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(rightHashes[l]));

                    leftCipher.Add(encLeft);
                    rightCipher.Add(encright);
                }

                // add these to CipherMoneyOrder
                string cipherQuery = "INSERT INTO [dbo].[CipherMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
                using SqlCommand cipherCmd = new SqlCommand(cipherQuery, con);
                {
                    // Insert them to MoneyOrder
                    cipherCmd.Parameters.AddWithValue("@index", i);
                    cipherCmd.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                    cipherCmd.Parameters.AddWithValue("@serialNumber", cipherSerial);
                    cipherCmd.Parameters.AddWithValue("@leftNumber1", leftCipher[0]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber1", rightCipher[0]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber2", leftCipher[1]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber2", rightCipher[1]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber3", leftCipher[2]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber3", rightCipher[2]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber4", leftCipher[3]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber4", rightCipher[3]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber5", leftCipher[4]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber5", rightCipher[4]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber6", leftCipher[5]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber6", rightCipher[5]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber7", leftCipher[6]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber7", rightCipher[6]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber8", leftCipher[7]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber8", rightCipher[7]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber9", leftCipher[8]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber9", rightCipher[8]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber10", leftCipher[9]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber10", rightCipher[9]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber11", leftCipher[10]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber11", rightCipher[10]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber12", leftCipher[11]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber12", rightCipher[11]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber13", leftCipher[12]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber13", rightCipher[12]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber14", leftCipher[13]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber14", rightCipher[13]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber15", leftCipher[14]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber15", rightCipher[14]);

                    cipherCmd.ExecuteNonQuery();
                }

                progressBar.Value += 1;
            }
            this.Close();
        }

        private void HashCheat(string amount)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            int occurances = 100;

            // create a fakeID
            Random randInt = new();
            int fakeID = randInt.Next(100000000, 999999999);

            // load in the customers information
            CustomerForm cust = new();

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
                string blindQuery = "INSERT INTO [dbo].[ArchivedBlinds]([index],[serialNumber],[encryptedSerialNumber],[blind]) VALUES(@index,@serialNumber,@encryptedSerialNumber,@blind)";
                using SqlCommand cmdBlind = new SqlCommand(blindQuery, con);
                {
                    // Add data to ArchivedBlinds
                    cmdBlind.Parameters.AddWithValue("@index", i);
                    cmdBlind.Parameters.AddWithValue("@serialNumber", serialNum);
                    cmdBlind.Parameters.AddWithValue("@encryptedSerialNumber", cipherSerial);
                    cmdBlind.Parameters.AddWithValue("@blind", blind);

                    cmdBlind.ExecuteNonQuery();
                }

                // create fake left and rights and store them in lists
                List<int> fakeLeftValues = new List<int>();
                List<int> fakeRightValues = new List<int>();
                for (int j = 0; j < 15; j++)
                {
                    // create a random number to be the left and the XOR for the right
                    int left = rand.Next(101, int.MaxValue);
                    int right = fakeID ^ left;

                    fakeLeftValues.Add(left);
                    fakeRightValues.Add(right);
                }

                // create the left and right hash lists
                List<string> leftHashes = new List<string>();
                List<string> rightHashes = new List<string>();

                for (int k = 0; k < 15; k++)
                {
                    // hash the left and right numbers
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        byte[] leftBytes = Encoding.UTF8.GetBytes(fakeLeftValues[k].ToString());
                        byte[] computedLeftHash = sha256.ComputeHash(leftBytes);
                        leftHashes.Add(Convert.ToBase64String(computedLeftHash));

                        byte[] rightBytes = Encoding.UTF8.GetBytes(fakeRightValues[k].ToString());
                        byte[] computedRightHash = sha256.ComputeHash(rightBytes);
                        rightHashes.Add(Convert.ToBase64String(computedRightHash));
                    }
                }

                // add these to HashMoneyOrder
                string query = "INSERT INTO [dbo].[HashMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
                using SqlCommand cmd = new SqlCommand(query, con);
                {
                    // Insert them to MoneyOrder
                    cmd.Parameters.AddWithValue("@index", i);
                    cmd.Parameters.AddWithValue("@moneyAmount", amount);
                    cmd.Parameters.AddWithValue("@serialNumber", serialNum);
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

                // create the left and right blind encrypted hash lists
                List<string> leftCipher = new List<string>();
                List<string> rightCipher = new List<string>();

                for (int l = 0; l < 15; l++)
                {
                    // this is just in case, we set the blind as the bigint number
                    rsa.setBlindFactor(blindNum);

                    // Encrypt the blind factor with the public key and multiply it by the left and rights
                    string encLeft = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(leftHashes[l]));
                    string encright = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(rightHashes[l]));

                    leftCipher.Add(encLeft);
                    rightCipher.Add(encright);
                }

                // add these to CipherMoneyOrder
                string cipherQuery = "INSERT INTO [dbo].[CipherMoneyOrder]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
                using SqlCommand cipherCmd = new SqlCommand(cipherQuery, con);
                {
                    // Insert them to MoneyOrder
                    cipherCmd.Parameters.AddWithValue("@index", i);
                    cipherCmd.Parameters.AddWithValue("@moneyAmount", cipherAmount);
                    cipherCmd.Parameters.AddWithValue("@serialNumber", cipherSerial);
                    cipherCmd.Parameters.AddWithValue("@leftNumber1", leftCipher[0]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber1", rightCipher[0]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber2", leftCipher[1]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber2", rightCipher[1]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber3", leftCipher[2]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber3", rightCipher[2]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber4", leftCipher[3]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber4", rightCipher[3]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber5", leftCipher[4]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber5", rightCipher[4]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber6", leftCipher[5]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber6", rightCipher[5]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber7", leftCipher[6]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber7", rightCipher[6]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber8", leftCipher[7]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber8", rightCipher[7]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber9", leftCipher[8]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber9", rightCipher[8]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber10", leftCipher[9]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber10", rightCipher[9]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber11", leftCipher[10]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber11", rightCipher[10]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber12", leftCipher[11]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber12", rightCipher[11]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber13", leftCipher[12]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber13", rightCipher[12]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber14", leftCipher[13]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber14", rightCipher[13]);
                    cipherCmd.Parameters.AddWithValue("@leftNumber15", leftCipher[14]);
                    cipherCmd.Parameters.AddWithValue("@rightNumber15", rightCipher[14]);

                    cipherCmd.ExecuteNonQuery();
                }
                progressBar.Value += 1;
            }
            this.Close();
        }

        protected virtual void OnOrderComplete()
        {
            OrderComplete?.Invoke(this, EventArgs.Empty);
        }

    }
}
