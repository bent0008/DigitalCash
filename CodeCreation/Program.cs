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

namespace DigitalCash
{
    class NewClass
    {
        static void Main(string[] args)
        {
            InputMoney();
            string serialNumber = GenerateSerialNumber();
            Console.WriteLine(serialNumber);

            Console.ReadKey();
        }


        // input amount of money in transaction
        static int InputMoney()
        {
            Console.Write("Amount: $");
            int moneyAmount = Int32.Parse(Console.ReadLine());
            return moneyAmount;
        }


        // generate unique serial ID number as string
        static string GenerateSerialNumber()
        {
            int length = 10;
            const string numberList = "0123456789";
            StringBuilder serialNum = new();
            Random rand = new();

            while (0 < length--)
            {
                serialNum.Append(numberList[rand.Next(numberList.Length)]);
            }
            Console.Write("Serial Number: ");

            return serialNum.ToString();
        }


        // identity strings with identity of customer (remains hidden unless customer tries to use the ecash illicitly more than once)
        static string GenerateIdentification()
        {
            Console.Write("First Name: ")
            string fname = Console.ReadLine();
            Console.Write("Last Name: ")
            string lname = Console.ReadLine();
            Console.Write("Account Number: ")
            string accountNumber = Console.ReadLine();

            string identity = fname + lname + accountNumber;

            int length = identity.Length;
            int separateNum = 2;

            int chars = length / separateNum;

            for (int i = 0; i < chars; i++)
            {
                string leftIdentity = chars[i];
            }

            for (int j = length; j > chars; j--)
            {
                string rightIdentity = chars[i];
            }



        }

        // bank's signature (before customer uses ecash)


    }
}