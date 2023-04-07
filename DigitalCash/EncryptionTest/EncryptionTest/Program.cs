using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.Reflection;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace EncryptionTest
{
    class EncryptionTest
    {
        static void Main(string[] args)
        {
            NewKey();

            Console.WriteLine("Encryption: ");
            Encryption();

            Console.Write("\nCreate XOR: \n");
            CreateXOR();

            Console.ReadKey();
        }

        static void CreateKey()
        {
            // Generate RSA key pair
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(512);

            // Export public key to file
            string publicKeyFile = "publickey.xml";
            File.WriteAllText(publicKeyFile, rsa.ToXmlString(false));

            // Export private key to file
            string privateKeyFile = "privatekey.xml";
            File.WriteAllText(privateKeyFile, rsa.ToXmlString(true));

            // Export public key to separate directory for the bank and merchant
            string bankDirectory = "C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Bank\\bin\\Debug\\net7.0-windows\\";
            string merchantDirectory = "C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Merchant\\bin\\Debug\\net7.0-windows\\";

            File.WriteAllText(Path.Combine(bankDirectory, publicKeyFile), rsa.ToXmlString(false));
            File.WriteAllText(Path.Combine(bankDirectory, privateKeyFile), rsa.ToXmlString(true));
            File.WriteAllText(Path.Combine(merchantDirectory, publicKeyFile), rsa.ToXmlString(false));

            Console.WriteLine("New keys generated");
        }

        static void NewKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            string privateKey = rsa.ToXmlString(true);
            File.WriteAllText("C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\EncryptionTest\\EncryptionTest\\bin\\Debug\\net7.0\\privatekey.xml", privateKey);
            string publicKey = rsa.ToXmlString(false);
            File.WriteAllText("C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\EncryptionTest\\EncryptionTest\\bin\\Debug\\net7.0\\publickey.xml", publicKey);

            //======new key=====
            rsa.PersistKeyInCsp = false;
            rsa.Clear();
            rsa = null;

            Console.WriteLine("New keys generated");
        }

        static BigInteger ConvertToBigInt(string data)
        {
            BigInteger con = new BigInteger();

            for (int i = 0; i < data.Length; i++)
            {
                char c = data[i];
                if (char.IsDigit(c))
                {
                    int num = c - '0';
                    con = con * 10 + num;
                }
            }

            return con;
        }


        static void Encryption()
        {
            RSAEncryption rsa = new();

            string amount = "hello";

            // declare the path to the public key and load it in
            string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\EncryptionTest\EncryptionTest\bin\Debug\net7.0\publickey.xml";
            rsa.LoadPublicFromXml(publicPath);

            // Create the blind factor
            rsa.createBlindFactor();

            string blind = rsa.retrieveBlindFactor();
            BigInteger blindNum = ConvertToBigInt(blind);
            rsa.setBlindFactor(blindNum);

            Console.WriteLine("blind string: " + blind);
            Console.WriteLine("blind bigint: " + blindNum);

            // Encrypt the blind factor with the public key and multiply it by the data
            string blindEncAmount = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(amount));
            Console.WriteLine("Blind Encrypted Amount: " + blindEncAmount);


            string privatePath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\EncryptionTest\EncryptionTest\bin\Debug\net7.0\privatekey.xml";
            rsa.LoadPrivateFromXml(privatePath);

            string signedData = rsa.Sign(ConvertToBigInt(blindEncAmount));

            Console.WriteLine("Signed Blinded: " + signedData);

            rsa.setBlindFactor(blindNum);
            string unblindSigned = rsa.unblind(ConvertToBigInt(signedData));
            Console.WriteLine("Unblinded-Signed: " + unblindSigned);

            BigInteger revealData= ConvertToBigInt(unblindSigned);
            byte[] revealedSigned = rsa.reveal(revealData);
            string revealedString = Encoding.UTF8.GetString(revealedSigned);
            Console.WriteLine("Revealed Signed: " + revealedString);
        }

        static void CreateXOR()
        {
            int userId = 123456789;

            Random rnd = new Random();
            int x = rnd.Next(101, int.MaxValue); // generate a random integer between 1 and int.MaxValue
            int y = userId ^ x; // XOR the random integer with the user ID to obtain the other integer

            Console.WriteLine($"User ID: {userId}");
            Console.WriteLine($"Random Pair: ({x}, {y})");
        }
    }
}