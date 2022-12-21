using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank
{
    public partial class MoneyOrder : Form
    {
        public MoneyOrder()
        {
            InitializeComponent();
        }


        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (cheatChkbx.Checked == true)
            {
                MessageBox.Show("You Cheated");
            }
            else
            {
                CreateMoneyOrder();
            }

        }

        private void CreateMoneyOrder()
        {
            string amount = moneyAmountTxtbx.Text;
            int occurances = 100;

            Random rand = new();
            int serialNum = rand.Next(100000000, 999999999);

            MessageBox.Show(serialNum.ToString());

            for (int i = 0; i < occurances; i++)
            {
                // index, amount, serialNum
            }
        }
    }
}
