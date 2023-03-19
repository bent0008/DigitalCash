using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace Customer
{
    public class RSACryptography
    {
        private RSACryptoServiceProvider _rsa;

        public RSACryptography()
        {
            _rsa = new RSACryptoServiceProvider();
        }

        public string GetPublicKey()
        {
            return _rsa.ToXmlString(false);
        }

        public string GetPrivateKey()
        {
            return _rsa.ToXmlString(true);
        }

        public string Encrypt(string data, string publicKey)
        {
            _rsa.FromXmlString(publicKey);

            byte[] plainBytes = Encoding.UTF8.GetBytes(data);
            byte[] encryptedBytes = _rsa.Encrypt(plainBytes, false);

            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string encryptedData, string privateKey)
        {
            _rsa.FromXmlString(privateKey);

            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
            byte[] decryptedBytes = _rsa.Decrypt(encryptedBytes, false);

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
