using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCreation
{
    internal class Bank
    {
        private bool moneyCheated = false;
        private bool serialNumCheated = false;

        static int ChooseMoneyOrder(int n)
        {
            Random rand = new();
            int chosenOrder = rand.Next(n);

            return chosenOrder;
        }
        public bool CheckMoney(int n, List<string> mlist)
        {
            for (int i = 0; i < n; i++)
            {
                if (mlist[0] != mlist[i])
                {
                    Console.WriteLine("Money amounts do not match!");
                    moneyCheated = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Money amounts match. Pass!");
                }
            }

            if (moneyCheated == true)
            {
                Console.WriteLine("You cheated the money amount!");
            }
            return moneyCheated;
        }

        public bool CheckSerialNumber(int n, List<string> snlist)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //Console.WriteLine("i = " + snlist[i] + " j = " + snlist[j]);
                    if (i != j)
                    {
                        Console.WriteLine("i = " + snlist[i] + " j = " + snlist[j]);
                        if (snlist[i] == snlist[j])
                        {
                            Console.WriteLine("You cheated the serial number!");
                            serialNumCheated = true;
                            break;
                        }
                    }
                }
            }
            return serialNumCheated;
        }

        public void BankSignature(int n, List<string> mlist , List<string> snlist)
        {
            string bankSign = "3531961060";

            // amount > serial number > bank signature
            string[] moneyOrder = {bankSign};

        }
    }
}
