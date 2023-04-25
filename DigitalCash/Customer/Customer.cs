using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security.Cryptography.Pkcs;
using System.Drawing;

namespace Customer
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        public string connectionString = "Data Source=LAPTOP-UOPDFGH4\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";
        public string Username { get; set; }
        public int Balance { get; set; }
        public bool LoggedIn { get; set; }
        public int ID { get; set; }
        public string FraudType { get; set; }
        public static CustomerForm SelectedCustomer { get; set; }

        public void Select()
        {
            SelectedCustomer = this;
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
            balanceAmountLbl.Text = "$" + Balance;
        }

        private void MoneyOrderBtn_Click(object sender, EventArgs e)
        {
            if (LoggedIn)
            {
                UpdateLabels();

                MoneyOrder newOrder = new();

                newOrder.Show();

                // wait for the OrderComplete event to finish
                newOrder.OrderComplete += new EventHandler(MoneyOrderComplete);
            }
            else
            {
                MessageBox.Show("Please log in.", "Error");
            }
        }

        private void MoneyOrderComplete(object sender, EventArgs e)
        {
            UpdateLabels();
            updateBox.AppendText("Generated 100 money orders\n");
            updateBox.AppendText(FraudType + "\n");
        }

        private void CustomerLoginBtn_Click(object sender, EventArgs e)
        {
            Login custLogin = new();
            custLogin.ShowDialog(this);
            UpdateLabels();
            updateBox.AppendText($"Logged in as {Username}\n");
        }

        private void UnblindBtn_Click(object sender, EventArgs e)
        {
            UpdateLabels();

            // Call the RSAEncryption class
            RSAEncryption rsa = new RSAEncryption();

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            // get the selection from the table
            string query = "SELECT * FROM [dbo].[SignedSelection] JOIN [dbo].[ArchivedBlinds] ON [dbo].[SignedSelection].[index] = [dbo].[ArchivedBlinds].[index]";

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
            string blind = "";

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
                blind = reader.GetString(36);
            }
            reader.Close();

            // declare the path to the public key and load it in
            string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\publickey.xml";
            rsa.LoadPublicFromXml(publicPath);

            rsa.setBlindFactor(rsa.ConvertToBigInt(blind));
            string unblindedAmount = rsa.unblind(rsa.ConvertToBigInt(amountString));
            string unblindedSerial = rsa.unblind(rsa.ConvertToBigInt(serialNumberString));
            string unblindLeft1 = rsa.unblind(rsa.ConvertToBigInt(left1));
            string unblindRight1 = rsa.unblind(rsa.ConvertToBigInt(right1));
            string unblindLeft2 = rsa.unblind(rsa.ConvertToBigInt(left2));
            string unblindRight2 = rsa.unblind(rsa.ConvertToBigInt(right2));
            string unblindLeft3 = rsa.unblind(rsa.ConvertToBigInt(left3));
            string unblindRight3 = rsa.unblind(rsa.ConvertToBigInt(right3));
            string unblindLeft4 = rsa.unblind(rsa.ConvertToBigInt(left4));
            string unblindRight4 = rsa.unblind(rsa.ConvertToBigInt(right4));
            string unblindLeft5 = rsa.unblind(rsa.ConvertToBigInt(left5));
            string unblindRight5 = rsa.unblind(rsa.ConvertToBigInt(right5));
            string unblindLeft6 = rsa.unblind(rsa.ConvertToBigInt(left6));
            string unblindRight6 = rsa.unblind(rsa.ConvertToBigInt(right6));
            string unblindLeft7 = rsa.unblind(rsa.ConvertToBigInt(left7));
            string unblindRight7 = rsa.unblind(rsa.ConvertToBigInt(right7));
            string unblindLeft8 = rsa.unblind(rsa.ConvertToBigInt(left8));
            string unblindRight8 = rsa.unblind(rsa.ConvertToBigInt(right8));
            string unblindLeft9 = rsa.unblind(rsa.ConvertToBigInt(left9));
            string unblindRight9 = rsa.unblind(rsa.ConvertToBigInt(right9));
            string unblindLeft10 = rsa.unblind(rsa.ConvertToBigInt(left10));
            string unblindRight10 = rsa.unblind(rsa.ConvertToBigInt(right10));
            string unblindLeft11 = rsa.unblind(rsa.ConvertToBigInt(left11));
            string unblindRight11 = rsa.unblind(rsa.ConvertToBigInt(right11));
            string unblindLeft12 = rsa.unblind(rsa.ConvertToBigInt(left12));
            string unblindRight12 = rsa.unblind(rsa.ConvertToBigInt(right12));
            string unblindLeft13 = rsa.unblind(rsa.ConvertToBigInt(left13));
            string unblindRight13 = rsa.unblind(rsa.ConvertToBigInt(right13));
            string unblindLeft14 = rsa.unblind(rsa.ConvertToBigInt(left14));
            string unblindRight14 = rsa.unblind(rsa.ConvertToBigInt(right14));
            string unblindLeft15 = rsa.unblind(rsa.ConvertToBigInt(left15));
            string unblindRight15 = rsa.unblind(rsa.ConvertToBigInt(right15));

            // put the unblinded signed data into a new table
            string queryTwo = "INSERT INTO [dbo].[UnblindedSelection]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
            using SqlCommand cmdTwo = new SqlCommand(queryTwo, con);
            {
                // Insert them to MoneyOrder
                cmdTwo.Parameters.AddWithValue("@index", index);
                cmdTwo.Parameters.AddWithValue("@moneyAmount", unblindedAmount);
                cmdTwo.Parameters.AddWithValue("@serialNumber", unblindedSerial);
                cmdTwo.Parameters.AddWithValue("@leftNumber1", unblindLeft1);
                cmdTwo.Parameters.AddWithValue("@rightNumber1", unblindRight1);
                cmdTwo.Parameters.AddWithValue("@leftNumber2", unblindLeft2);
                cmdTwo.Parameters.AddWithValue("@rightNumber2", unblindRight2);
                cmdTwo.Parameters.AddWithValue("@leftNumber3", unblindLeft3);
                cmdTwo.Parameters.AddWithValue("@rightNumber3", unblindRight3);
                cmdTwo.Parameters.AddWithValue("@leftNumber4", unblindLeft4);
                cmdTwo.Parameters.AddWithValue("@rightNumber4", unblindRight4);
                cmdTwo.Parameters.AddWithValue("@leftNumber5", unblindLeft5);
                cmdTwo.Parameters.AddWithValue("@rightNumber5", unblindRight5);
                cmdTwo.Parameters.AddWithValue("@leftNumber6", unblindLeft6);
                cmdTwo.Parameters.AddWithValue("@rightNumber6", unblindRight6);
                cmdTwo.Parameters.AddWithValue("@leftNumber7", unblindLeft7);
                cmdTwo.Parameters.AddWithValue("@rightNumber7", unblindRight7);
                cmdTwo.Parameters.AddWithValue("@leftNumber8", unblindLeft8);
                cmdTwo.Parameters.AddWithValue("@rightNumber8", unblindRight8);
                cmdTwo.Parameters.AddWithValue("@leftNumber9", unblindLeft9);
                cmdTwo.Parameters.AddWithValue("@rightNumber9", unblindRight9);
                cmdTwo.Parameters.AddWithValue("@leftNumber10", unblindLeft10);
                cmdTwo.Parameters.AddWithValue("@rightNumber10", unblindRight10);
                cmdTwo.Parameters.AddWithValue("@leftNumber11", unblindLeft11);
                cmdTwo.Parameters.AddWithValue("@rightNumber11", unblindRight11);
                cmdTwo.Parameters.AddWithValue("@leftNumber12", unblindLeft12);
                cmdTwo.Parameters.AddWithValue("@rightNumber12", unblindRight12);
                cmdTwo.Parameters.AddWithValue("@leftNumber13", unblindLeft13);
                cmdTwo.Parameters.AddWithValue("@rightNumber13", unblindRight13);
                cmdTwo.Parameters.AddWithValue("@leftNumber14", unblindLeft14);
                cmdTwo.Parameters.AddWithValue("@rightNumber14", unblindRight14);
                cmdTwo.Parameters.AddWithValue("@leftNumber15", unblindLeft15);
                cmdTwo.Parameters.AddWithValue("@rightNumber15", unblindRight15);

                cmdTwo.ExecuteNonQuery();
            }

            UpdateLabels();

            updateBox.AppendText("The signed money order has been unblinded\n");
        }

        private void ToBankBtn_Click(object sender, EventArgs e)
        {
            UpdateLabels();
            updateBox.AppendText("The money order was sent to the bank\n");
        }

        private void ToMerchantBtn_Click(object sender, EventArgs e)
        {
            UpdateLabels();
            updateBox.AppendText("The money order was sent to the merchant\n");
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