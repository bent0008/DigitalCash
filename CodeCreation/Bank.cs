using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CodeCreation
{
    internal class Bank
    {
        private bool moneyCheated = false;
        private bool serialNumCheated = false;


        public int ChooseMoneyOrder(int n)
        {
            Random rand = new();
            int randomOrder = rand.Next(0, n);

            return randomOrder;
        }

        public bool CheckMoney(int n, List<string> mlist, int order)
        {
            for (int i = 0; i < n; i++)
            {
                if (i == order)
                {
                    Console.WriteLine(order);
                    continue;
                }
                else
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
            }

            if (moneyCheated == true)
            {
                Console.WriteLine("You cheated the money amount!");
            }
            return moneyCheated;
        }

        public bool CheckSerialNumber(int n, List<string> snlist, int order)
        {
            Console.WriteLine(order);
            for (int i = 0; i < n; i++)
            {
                if (i == order)
                {
                    continue;
                }
                else
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (j == order)
                        {
                            continue;
                        }
                        else
                        {
                            if (i != j)
                            {
                                Console.WriteLine("i = " + snlist[i] + " j = " + snlist[j]);
                                if (snlist[i] == snlist[j])
                                {
                                    Console.WriteLine("Serial numbers are the same!");
                                    serialNumCheated = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (serialNumCheated == true)
                    {
                        Console.WriteLine("You cheated the serial number!");
                        break;
                    }
                }
            }
            return serialNumCheated;
        }

        public List<string> CreateMoneyOrder(int n, List<string> mlist , List<string> snlist, int order, bool mCheated, bool snCheated)
        {
            string bankSign = "3531961060";
            List<string>[] moneyOrder = new List<string>[n];

            // amount -> serial number -> bank signature
            if (mCheated == true || snCheated == true)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    var tmpList = new List<string>();
                    tmpList.Add(mlist[i]);
                    tmpList.Add(snlist[i]);
                    tmpList.Add(bankSign);

                    moneyOrder[i] = tmpList;
                }

                List<string> chosenOrder = moneyOrder[order];
                Console.WriteLine(order);
                return chosenOrder;
            }
        }
    }
}
