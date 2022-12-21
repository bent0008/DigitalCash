namespace Bank
{
    public partial class BankForm : Form
    {
        public BankForm()
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
    }
}