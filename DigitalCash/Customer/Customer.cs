using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security.Cryptography.Pkcs;

namespace Customer
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        public string connectionString = "Data Source=BEN_T\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";
        public string Username { get; set; }
        public int Balance { get; set; }
        public bool LoggedIn { get; set; }
        public int ID { get; set; }
        public static CustomerForm SelectedCustomer { get; set; }

        public void Select()
        {
            SelectedCustomer = this;
        }

        private void UpdateLabels()
        {
            usernameLbl.Text = Username;
            balanceAmountLbl.Text = "$" + Balance;
        }

        private void AddCustomerBtn_Click(object sender, EventArgs e)
        {
            AddCustomer addCust = new();
            addCust.Show();
        }

        private void MoneyOrderBtn_Click(object sender, EventArgs e)
        {
            if (LoggedIn)
            {
                MoneyOrder newOrder = new();
                newOrder.Show();
            }
            else
            {
                MessageBox.Show("Please log in.", "Error");
            }
        }

        private void CustomerLoginBtn_Click(object sender, EventArgs e)
        {
            Login custLogin = new();
            custLogin.ShowDialog(this);
            UpdateLabels();
        }

        private void EncryptBtn_Click(object sender, EventArgs e)
        {
            Encrypt encr = new();
            encr.Show();
        }

        private void UnblindBtn_Click(object sender, EventArgs e)
        {
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

            // put the unblinded signed data into a new table
            string queryTwo = "INSERT INTO [dbo].[UnblindedSelection]([index],[moneyAmount],[serialNumber],[leftNumber1],[rightNumber1],[leftNumber2],[rightNumber2],[leftNumber3],[rightNumber3],[leftNumber4],[rightNumber4],[leftNumber5],[rightNumber5],[leftNumber6],[rightNumber6],[leftNumber7],[rightNumber7],[leftNumber8],[rightNumber8],[leftNumber9],[rightNumber9],[leftNumber10],[rightNumber10],[leftNumber11],[rightNumber11],[leftNumber12],[rightNumber12],[leftNumber13],[rightNumber13],[leftNumber14],[rightNumber14],[leftNumber15],[rightNumber15]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber1,@rightNumber1,@leftNumber2,@rightNumber2,@leftNumber3,@rightNumber3,@leftNumber4,@rightNumber4,@leftNumber5,@rightNumber5,@leftNumber6,@rightNumber6,@leftNumber7,@rightNumber7,@leftNumber8,@rightNumber8,@leftNumber9,@rightNumber9,@leftNumber10,@rightNumber10,@leftNumber11,@rightNumber11,@leftNumber12,@rightNumber12,@leftNumber13,@rightNumber13,@leftNumber14,@rightNumber14,@leftNumber15,@rightNumber15)";
            using SqlCommand cmdTwo = new SqlCommand(queryTwo, con);
            {
                // Insert them to MoneyOrder
                cmdTwo.Parameters.AddWithValue("@index", index);
                cmdTwo.Parameters.AddWithValue("@moneyAmount", unblindedAmount);
                cmdTwo.Parameters.AddWithValue("@serialNumber", unblindedSerial);
                cmdTwo.Parameters.AddWithValue("@leftNumber1", left1);
                cmdTwo.Parameters.AddWithValue("@rightNumber1", right1);
                cmdTwo.Parameters.AddWithValue("@leftNumber2", left2);
                cmdTwo.Parameters.AddWithValue("@rightNumber2", right2);
                cmdTwo.Parameters.AddWithValue("@leftNumber3", left3);
                cmdTwo.Parameters.AddWithValue("@rightNumber3", right3);
                cmdTwo.Parameters.AddWithValue("@leftNumber4", left4);
                cmdTwo.Parameters.AddWithValue("@rightNumber4", right4);
                cmdTwo.Parameters.AddWithValue("@leftNumber5", left5);
                cmdTwo.Parameters.AddWithValue("@rightNumber5", right5);
                cmdTwo.Parameters.AddWithValue("@leftNumber6", left6);
                cmdTwo.Parameters.AddWithValue("@rightNumber6", right6);
                cmdTwo.Parameters.AddWithValue("@leftNumber7", left7);
                cmdTwo.Parameters.AddWithValue("@rightNumber7", right7);
                cmdTwo.Parameters.AddWithValue("@leftNumber8", left8);
                cmdTwo.Parameters.AddWithValue("@rightNumber8", right8);
                cmdTwo.Parameters.AddWithValue("@leftNumber9", left9);
                cmdTwo.Parameters.AddWithValue("@rightNumber9", right9);
                cmdTwo.Parameters.AddWithValue("@leftNumber10", left10);
                cmdTwo.Parameters.AddWithValue("@rightNumber10", right10);
                cmdTwo.Parameters.AddWithValue("@leftNumber11", left11);
                cmdTwo.Parameters.AddWithValue("@rightNumber11", right11);
                cmdTwo.Parameters.AddWithValue("@leftNumber12", left12);
                cmdTwo.Parameters.AddWithValue("@rightNumber12", right12);
                cmdTwo.Parameters.AddWithValue("@leftNumber13", left13);
                cmdTwo.Parameters.AddWithValue("@rightNumber13", right13);
                cmdTwo.Parameters.AddWithValue("@leftNumber14", left14);
                cmdTwo.Parameters.AddWithValue("@rightNumber14", right14);
                cmdTwo.Parameters.AddWithValue("@leftNumber15", left15);
                cmdTwo.Parameters.AddWithValue("@rightNumber15", right15);

                cmdTwo.ExecuteNonQuery();
            }
        }
    }
}