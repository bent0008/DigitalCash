namespace Customer
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void AddCustomerBtn_Click(object sender, EventArgs e)
        {
            AddCustomer addCust = new();
            addCust.Show();
        }

        private void MoneyOrderBtn_Click(object sender, EventArgs e)
        {
            MoneyOrder newOrder = new();
            newOrder.Show();
        }

        private void CustomerLoginBtn_Click(object sender, EventArgs e)
        {
            Login custLogin = new();
            custLogin.Show();
        }
    }
}