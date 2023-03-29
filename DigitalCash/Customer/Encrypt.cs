using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.Reflection;
using System.Xml.Serialization;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Customer
{
    public partial class Encrypt : Form
    {
        public Encrypt()
        {
            InitializeComponent();
        }


        private void CreateKey()
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
        }

        private void NewKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            string privateKey = rsa.ToXmlString(true);
            File.WriteAllText("C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Customer\\bin\\Debug\\net7.0-windows\\privatekey.xml", privateKey);
            string publicKey = rsa.ToXmlString(false);
            File.WriteAllText("C:\\Users\\bentu\\OneDrive\\Documents\\GitHub\\DigitalCash\\DigitalCash\\Customer\\bin\\Debug\\net7.0-windows\\publickey.xml", publicKey);

            //======new key=====
            rsa.PersistKeyInCsp = false;
            rsa.Clear();
            rsa = null;
        }



        private void EncryptBtn_Click(object sender, EventArgs e)
        {
            RSAEncryption rsa = new();

            string amount = "hello";

            // declare the path to the private key and load it in
            string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\publickey.xml";
            rsa.LoadPublicFromXml(publicPath);

            // Create the blind factor
            rsa.createBlindFactor();
            // Encrypt the blind factor with the public key and multiply it by the data
            string blindEncAmount = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(amount));
            MessageBox.Show(blindEncAmount, "Blinded Amount");

            string blind = rsa.retrieveBlindFactor();
            BigInteger blindNum = new BigInteger(Encoding.UTF8.GetBytes(blind));
            rsa.setBlindFactor(blindNum);

            string privatePath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\privatekey.xml";
            rsa.LoadPrivateFromXml(privatePath);

            byte[] temp = rsa.PrivateDecryption(Encoding.UTF8.GetBytes(blindEncAmount));
            MessageBox.Show(Encoding.UTF8.GetString(temp), "Blind-Decrypted");

            string unblindedSigned = rsa.unblind(new BigInteger(temp));
            MessageBox.Show(unblindedSigned, "Blind-Decrypted-Unblinded");

            string signedData = rsa.Sign(new BigInteger(Encoding.UTF8.GetBytes(blindEncAmount)));

            MessageBox.Show(signedData, "Signed");

            byte[] revealedUnblinded = rsa.reveal(new BigInteger(Encoding.UTF8.GetBytes(unblindedSigned)));
            MessageBox.Show(Encoding.UTF8.GetString(revealedUnblinded), "Revealed");


            BigInteger blindAmount = new BigInteger(Encoding.UTF8.GetBytes(blindEncAmount));
            string unblindAmount = rsa.unblind(blindAmount);
            string decAmo = Encoding.UTF8.GetString(rsa.PrivateDecryption(Encoding.UTF8.GetBytes(unblindAmount)));

            byte[] decryptedAmount = rsa.PrivateDecryption(Encoding.UTF8.GetBytes(blindEncAmount));
            BigInteger decrypted = new BigInteger(decryptedAmount);
            MessageBox.Show(unblindAmount, "Blind-Unblind");
            MessageBox.Show(decAmo, "Blind-Unblind-Decrypted");
            MessageBox.Show(Encoding.UTF8.GetString(decryptedAmount), "Blind-Decrypt");
        }

        private void DecryptBtn_Click(object sender, EventArgs e)
        {
            RSAEncryption rsa = new();

            string amount = "Hello";

            // declare the path to the private key and load it in
            string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\publickey.xml";
            rsa.LoadPublicFromXml(publicPath);

            rsa.createBlindFactor();

            string encryptedAmount = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(amount));

            MessageBox.Show(encryptedAmount, "Encrypted");

            string privatePath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\privatekey.xml";
            rsa.LoadPrivateFromXml(privatePath);

            byte[] decryptedAmount = rsa.PrivateDecryption(Encoding.UTF8.GetBytes(encryptedAmount));
            BigInteger decrypt = new BigInteger(decryptedAmount);
            MessageBox.Show(Encoding.UTF8.GetString(decryptedAmount));
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            string plaintText = "Hello";

            RSAEncryption rsa = new RSAEncryption();

            string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\publickey.xml";
            rsa.LoadPublicFromXml(publicPath);
            byte[] cipherText = rsa.PublicEncryption(Encoding.UTF8.GetBytes(plaintText));

            string privatePath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\privatekey.xml";
            rsa.LoadPrivateFromXml(privatePath);
            byte[] decryptedText = rsa.PrivateDecryption(cipherText);
            MessageBox.Show(Encoding.UTF8.GetString(decryptedText));
        }


        private void BasicBtn_Click(object sender, EventArgs e)
        {
            string plaintText = "Hello";

            RSAEncryption rsa = new RSAEncryption();

            string privatePath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\privatekey.xml";
            rsa.LoadPrivateFromXml(privatePath);
            byte[] cipherText = rsa.PrivateEncryption(Encoding.UTF8.GetBytes(plaintText));

            string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\publickey.xml";
            rsa.LoadPublicFromXml(publicPath);
            byte[] decryptedText = rsa.PublicDecryption(cipherText);
            BigInteger finalText = new BigInteger(decryptedText);
            MessageBox.Show(finalText.ToString(), "end");
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            RSACryptography rsa = new RSACryptography();

            string publicKey = rsa.GetPublicKey();

            string encryptedData = rsa.Encrypt("100", publicKey);
            Debug.WriteLine(encryptedData);

            string privateKey = rsa.GetPrivateKey();
            string decrypted = rsa.Decrypt(encryptedData, privateKey);
            Debug.WriteLine(decrypted);
        }

        private void GenKeysBtn_Click(object sender, EventArgs e)
        {
            //CreateKey();
            NewKey();
            MessageBox.Show("New key generated.");
        }
    }
}
