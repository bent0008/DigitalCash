namespace Bank
{
    public partial class BankForm : Form
    {
        public BankForm()
        {
            InitializeComponent();
        }

        private void addCustomerBtn_Click(object sender, EventArgs e)
        {
            AddCustomer addcust = new();
            addcust.Show();
        }
    }
}