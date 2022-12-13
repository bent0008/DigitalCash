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
using System.Security.Cryptography;
using CodeCreation;


namespace DigitalCash
{
    class Alice
    {
        static void Main(string[] args)
        {
            // Check RSA
            RSACryptosystem();
            // RSADecrypt(message);

            // How many money orders
            Console.Write("How many money orders? ");
            int n = Int32.Parse(Console.ReadLine());

            // Create lists
            var moneyList = new List<string>();
            var serialNumList = new List<string>();

            // Add inputs to lists
            for (int i = 0; i < n; i++)
            {
                string moneyAmount = InputMoney();
                moneyList.Add(moneyAmount);

                string serialNumber = CreateSerialNumber();
                serialNumList.Add(serialNumber);
            }

            // Test bank class
            Bank Banker = new();
            int randomOrder = Banker.ChooseMoneyOrder(n);
            bool moneyCheated = Banker.CheckMoney(n, moneyList, randomOrder);
            bool serialNumCheated = Banker.CheckSerialNumber(n, serialNumList, randomOrder);

            List<string> moneyOrder = Banker.CreateMoneyOrder(n, moneyList, serialNumList, randomOrder, moneyCheated, serialNumCheated);

            if (moneyOrder != null)
            {
                Console.Write("Alice's private money order: " + moneyOrder[0]);
                for (int j = 1; j < moneyOrder.Count; j++)
                {
                    Console.Write(", " + moneyOrder[j]);
                }
            }
            else
            {
                Console.WriteLine("Money order not approved.");
            }

            Console.ReadKey();
        }


        // Input amount of money in transaction
        static string InputMoney()
        {
            Console.Write("Amount: $");
            string moneyAmount = Console.ReadLine();
            return moneyAmount;
        }


        // Input unique serial ID number as string
        static string CreateSerialNumber()
        {
            Console.Write("Serial Number: ");
            string serialNum = Console.ReadLine();

            return serialNum;

            // Generate random
            //int length = 10;
            //Random rand = new();
            //string serialNum = "";

            //while(0 < length--)
            //{
            //    serialNum += rand.Next(length);
            //}
        }


        static void RSACryptosystem()
        {
            string message = "BenjaminTurnock0485608bturnoc@jacksonville.edu";
            RSA rsa = RSA.Create();

            // Generate a key pair
            rsa.KeySize = 2048;

            // Make public and private keys
            RSAParameters publickey = rsa.ExportParameters(false);
            RSAParameters privateKey = rsa.ExportParameters(true);

            // Encrypt the public key
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] encryptedBytes = rsa.Encrypt(messageBytes, RSAEncryptionPadding.OaepSHA256);

            // Decrypt using private key
            byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA256);
            string decryptSignature = System.Text.Encoding.UTF8.GetString(decryptedBytes);

            // Test
            string encrypted = System.Text.Encoding.UTF8.GetString(encryptedBytes);
            Console.WriteLine(encrypted);
            Console.WriteLine(decryptSignature);
        }


        static string RSADecrypt(byte[] encryptedMessage)
        {
            RSA rsa = RSA.Create();
            rsa.KeySize = 2048;

            // Decrypt using private key
            RSAParameters privatekey = rsa.ExportParameters(true);
            byte[] decryptedBytes = rsa.Decrypt(encryptedMessage, RSAEncryptionPadding.OaepSHA256);
            string decryptSignature = System.Text.Encoding.UTF8.GetString(decryptedBytes);

            return decryptSignature;
        }


        // RSA Encryption using key
        static byte[] RSAEncrypt(byte[] message, RSAParameters publicKey, bool padding)
        {
            byte[] encryptedMessage;
            using (RSACryptoServiceProvider rsa = new())
            {
                rsa.ImportParameters(publicKey);
                encryptedMessage = rsa.Encrypt(message, padding);
            }
            return encryptedMessage;
        }

        // RSA Decryption using key
        static public byte[] Decryption(byte[] message, RSAParameters privateKey, bool padding)
        {
            byte[] decryptedMessage;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(privateKey);
                decryptedMessage = rsa.Decrypt(message, padding);
            }
            return decryptedMessage;
        }
    }
    
    //// identity strings with identity of customer (remains hidden unless customer tries to use the ecash illicitly more than once)
        //private static void GenerateIdentification(out string leftIdentity, out string rightIdentity)
        //{
        //    Console.Write("First Name: ");
        //    string fname = Console.ReadLine();
        //    Console.Write("Last Name: ");
        //    string lname = Console.ReadLine();
        //    Console.Write("Account Number: ");
        //    string accountNumber = Console.ReadLine();

        //    string identity = fname + lname + accountNumber;

        //    int length = identity.Length;
        //    int separateNum = 2;

        //    int chars = length / separateNum;
        //    leftIdentity = "";
        //    rightIdentity = "";

        //    for (int i = 0; i < chars; i++)
        //    {
        //        leftIdentity += identity[i];
        //    }

        //    for (int j = chars; j < length; j++)
        //    {
        //        rightIdentity += identity[j];
        //    }
        //}
}