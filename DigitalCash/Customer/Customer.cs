using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
            string left = "";
            string right = "";
            string blind = "";

            while (reader.Read())
            {
                index = reader.GetInt32(0);
                amountString = reader.GetString(1);
                serialNumberString = reader.GetString(2);
                left = reader.GetString(3);
                right = reader.GetString(4);
                blind = reader.GetString(6);
            }
            reader.Close();


            // declare the path to the public key and load it in
            string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\publickey.xml";
            rsa.LoadPublicFromXml(publicPath);

            rsa.setBlindFactor(rsa.ConvertToBigInt(blind));
            string unblindedAmount = rsa.unblind(rsa.ConvertToBigInt(amountString));
            string unblindedSerial = rsa.unblind(rsa.ConvertToBigInt(serialNumberString));

            // put the unblinded signed data into a new table
            string queryTwo = "INSERT INTO [dbo].[UnblindedSelection]([index],[moneyAmount],[serialNumber],[leftNumber],[rightNumber]) VALUES(@index,@moneyAmount,@serialNumber,@leftNumber,@rightNumber)";
            using SqlCommand cmdTwo = new SqlCommand(queryTwo, con);
            {
                // Insert them to MoneyOrder
                cmdTwo.Parameters.AddWithValue("@index", index);
                cmdTwo.Parameters.AddWithValue("@moneyAmount", unblindedAmount);
                cmdTwo.Parameters.AddWithValue("@serialNumber", unblindedSerial);
                cmdTwo.Parameters.AddWithValue("@leftNumber", left);
                cmdTwo.Parameters.AddWithValue("@rightNumber", right);

                cmdTwo.ExecuteNonQuery();
            }
        }
    }
}