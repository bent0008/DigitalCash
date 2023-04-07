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
    }
}
