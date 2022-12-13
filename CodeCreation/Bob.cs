using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeCreation
{
    internal class Bob
    {
        static void RSAEncryption(string message)
        {
            RSA rsa = RSA.Create();
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
    }
}
