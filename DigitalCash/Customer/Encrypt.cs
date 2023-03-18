using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.Reflection;
using System.Xml.Serialization;

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
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);

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

        private void ManualKey()
        {
            // Set p, q, and e equal to 17, 23, and 5 respectively
            byte[] p = new byte[] { 0x11 };
            byte[] q = new byte[] { 0x17 };
            byte[] e = new byte[] { 0x05 };

            // Convert the byte arrays to BigInteger objects
            BigInteger P = new BigInteger(p);
            BigInteger Q = new BigInteger(q);
            BigInteger E = new BigInteger(e);

            // Calculate n and d
            BigInteger N = P * Q;
            BigInteger phi = (P - 1) * (Q - 1);
            BigInteger D = (2*(phi)+1)/E;

            // Create a new RSA instance with custom parameters
            var rsa = new RSACryptoServiceProvider();
            var rsaParams = new RSAParameters
            {
                P = p,
                Q = q,
                Exponent = e,
                D = Encoding.UTF8.GetBytes(D.ToString()),
                Modulus = Encoding.UTF8.GetBytes(N.ToString())
            };
            rsa.ImportParameters(rsaParams);

            // Export public key
            RSAParameters publicKeyParams = rsa.ExportParameters(false);
            XmlSerializer serializer = new XmlSerializer(typeof(RSAParameters));
            using (TextWriter writer = new StreamWriter("publickey.xml"))
            {
                serializer.Serialize(writer, publicKeyParams);
            }

            // Export private key
            RSAParameters privateKeyParams = rsa.ExportParameters(true);
            using (TextWriter writer = new StreamWriter("privatekey.xml"))
            {
                serializer.Serialize(writer, privateKeyParams);
            }
        }



        private void EncryptBtn_Click(object sender, EventArgs e)
        {
            RSAEncryption rsa = new();

            string amount = "100";

            // declare the path to the private key and load it in
            string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\publickey.xml";
            rsa.LoadPublicFromXml(publicPath);

            // Create the blind factor
            rsa.createBlindFactor();
            // Encrypt the blind factor with the public key and multiply it by the data
            string blindEncAmount = rsa.PublicBlindEncryption(Encoding.UTF8.GetBytes(amount));

            string blind = rsa.retrieveBlindFactor();

            MessageBox.Show(blindEncAmount, "Blinded Amount");

            string privatePath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\privatekey.xml";
            rsa.LoadPrivateFromXml(privatePath);

            string signedData = rsa.Sign(new BigInteger(Encoding.UTF8.GetBytes(blindEncAmount)));

            MessageBox.Show(signedData, "Signed");

            string unblindedSigned = rsa.Unblind(new BigInteger(Encoding.UTF8.GetBytes(signedData)));
            MessageBox.Show(unblindedSigned, "Signed-Unblinded");

            byte[] revealedUnblinded = rsa.reveal(new BigInteger(Encoding.UTF8.GetBytes(unblindedSigned)));
            BigInteger revealed = new BigInteger(revealedUnblinded);
            MessageBox.Show(revealed.ToString(), "Revealed");

            byte[] temp = rsa.PrivateDecryption(revealedUnblinded);
            BigInteger tempFinal = new BigInteger(temp);
            MessageBox.Show(tempFinal.ToString(), "End");

            BigInteger blindNum = BigInteger.Parse(blind);
            rsa.setBlindFactor(blindNum);

            BigInteger blindAmount = BigInteger.Parse(blindEncAmount);
            string unblindAmount = rsa.Unblind(blindAmount);


            byte[] decryptedAmount = rsa.PrivateDecryption(Encoding.UTF8.GetBytes(blindEncAmount));
            BigInteger decrypted = new BigInteger(decryptedAmount);
            MessageBox.Show(unblindAmount, "Blind-Unblind");
            MessageBox.Show(decrypted.ToString(), "Blind-Decrypt");
        }

        private void DecryptBtn_Click(object sender, EventArgs e)
        {
            RSAEncryption rsa = new();

            string amount = "Hello";

            // declare the path to the private key and load it in
            string privatePath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\privatekey.xml";
            rsa.LoadPrivateFromXml(privatePath);

            byte[] encryptedAmount = rsa.PrivateEncryption(Encoding.UTF8.GetBytes(amount));
            BigInteger encrypt = new BigInteger();
            MessageBox.Show(encrypt.ToString(), "Encrypted");

            string publicPath = @"C:\Users\bentu\OneDrive\Documents\GitHub\DigitalCash\DigitalCash\Customer\bin\Debug\net7.0-windows\publickey.xml";
            rsa.LoadPublicFromXml(publicPath);

            byte[] decryptedAmount = rsa.PublicDecryption(encryptedAmount);
            BigInteger decrypt = new BigInteger();
            MessageBox.Show(decrypt.ToString(), "Decrypted");
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
            BigInteger finalText = new BigInteger(decryptedText);
            MessageBox.Show(finalText.ToString());
        }


        private void ClassBtn_Click(object sender, EventArgs e)
        {
            // Create an instance of the RSACryptoServiceProvider class with a key size of 2048 bits
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            // Get the public and private keys in XML format
            string publicKeyXml = rsa.ToXmlString(false);
            string privateKeyXml = rsa.ToXmlString(true);

            // Convert a plaintext message to a byte array
            string plaintext = "100";
            byte[] plaintextBytes = System.Text.Encoding.UTF8.GetBytes(plaintext);

            // Encrypt the plaintext using the public key
            byte[] ciphertextBytes = rsa.Encrypt(plaintextBytes, false);
            string cipherText = System.Text.Encoding.UTF8.GetString(ciphertextBytes);
            MessageBox.Show(cipherText);

            // Decrypt the ciphertext using the private key
            byte[] decryptedBytes = rsa.Decrypt(ciphertextBytes, false);

            // Convert the decrypted bytes back to a string
            string decryptedText = System.Text.Encoding.UTF8.GetString(decryptedBytes);
            MessageBox.Show(decryptedText.ToString());
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

        private void GenKeysBtn_Click(object sender, EventArgs e)
        {
            CreateKey();
            MessageBox.Show("New key generated.");
        }









        //// Archived code
        //// Encrypt button
        //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        //string publicKey = File.ReadAllText("publickey");
        //rsa.FromXmlString(publicKey);
        //    byte[] cipherText = rsa.Encrypt(Encoding.UTF8.GetBytes(inputTxtBx.Text), false);
        //encryptLbl.Text = Encoding.UTF8.GetString(cipherText);

        //// Decrypt Button
        //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        //string publicKey = File.ReadAllText("publickey");
        //rsa.FromXmlString(publicKey);
        //    byte[] cipherText = rsa.Encrypt(Encoding.UTF8.GetBytes(inputTxtBx.Text), false);

        //string privateKey = File.ReadAllText("privatekey");
        //rsa.FromXmlString(privateKey);
        //    byte[] decryptedText = rsa.Decrypt(cipherText, false);
        //decryptLbl.Text = Encoding.UTF8.GetString(decryptedText);
    }
}
