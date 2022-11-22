// Ben Turnock
// Digital Cash

using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using CodeCreation;

namespace DigitalCash
{
    class Alice
    {
        static void Main(string[] args)
        {
            string leftIdentity, rightIdentity;

            Console.Write("How many money orders? ");
            int n = Int32.Parse(Console.ReadLine());

            var moneyList = new List<string>();
            var serialNumList = new List<string>();

            for (int i = 0; i < n; i++)
            {
                string moneyAmount = InputMoney();
                moneyList.Add(moneyAmount);

                string serialNumber = CreateSerialNumber();
                serialNumList.Add(serialNumber);

                //CreateIdentification(out leftIdentity, out rightIdentity);

                //Console.WriteLine("Left: " + leftIdentity);
                //Console.WriteLine("Right: " + rightIdentity);
            }

            Bank Banker = new();
            Banker.CheckMoney(n, moneyList);
            Banker.CheckSerialNumber(n, serialNumList);

            Console.ReadKey();
        }


        // input amount of money in transaction
        static string InputMoney()
        {
            Console.Write("Amount: $");
            string moneyAmount = Console.ReadLine();
            return moneyAmount;
        }


        // generate unique serial ID number as string
        static string CreateSerialNumber()
        {
            Console.Write("Serial Number: ");
            string serialNum = Console.ReadLine();

            return serialNum;

            //int length = 10;
            //Random rand = new();
            //string serialNum = "";

            //while(0 < length--)
            //{
            //    serialNum += rand.Next(length);
            //}
        }


        // identity strings with identity of customer (remains hidden unless customer tries to use the ecash illicitly more than once)
        private static void GenerateIdentification(out string leftIdentity, out string rightIdentity)
        {
            Console.Write("First Name: ");
            string fname = Console.ReadLine();
            Console.Write("Last Name: ");
            string lname = Console.ReadLine();
            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();

            string identity = fname + lname + accountNumber;

            int length = identity.Length;
            int separateNum = 2;

            int chars = length / separateNum;
            leftIdentity = "";
            rightIdentity = "";

            for (int i = 0; i < chars; i++)
            {
                leftIdentity += identity[i];
            }

            for (int j = chars; j < length; j++)
            {
                rightIdentity += identity[j];
            }
        }



    }
}