using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Xml;

namespace EncryptionTest
{
    public class RSAEncryption : IDisposable
    {
        // Members:
        // RSA Key components (just the three I'm using, there is more...)
        private BigInteger D = null;
        private BigInteger Exponent = null;
        private BigInteger Modulus = null;
        private BigInteger blind = null;
        private BigInteger enBlind = null;
        private BigInteger invblind = null;

        // .NET RSA class, for loading and creating key pairs
        private RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        // flags, is the keys has been loaded yet?
        private bool isPrivateKeyLoaded = false;
        private bool isPublicKeyLoaded = false;

        // Properties
        public bool IsPrivateKeyLoaded
        { get { return isPrivateKeyLoaded; } }

        public bool IsPublicKeyLoaded
        { get { return isPublicKeyLoaded; } }

        // Methods:
        public void LoadPublicFromXml(string publicPath)
        {
            if (!File.Exists(publicPath))
                throw new FileNotFoundException("File not exists: " + publicPath);
            // Using the .NET RSA class to load a key from an Xml file, and populating the relevant members
            // of my class with it's RSAParameters
            try
            {
                rsa.FromXmlString(File.ReadAllText(publicPath));
                RSAParameters rsaParams = rsa.ExportParameters(false);
                Modulus = new BigInteger(rsaParams.Modulus);
                Exponent = new BigInteger(rsaParams.Exponent);
                isPublicKeyLoaded = true;
                isPrivateKeyLoaded = false;
            }
            // Examle for the proper use of try - catch blocks: Informing the main app where and why the Exception occurred
            catch (CryptographicException ex)  // Not a Key file
            {
                string excReason = "Exception occurred at LoadPublicFromXml(), Selected xml file is not a public key file.";
                System.Diagnostics.Debug.WriteLine(excReason + " Exception Message: " + ex.Message);
                throw new Exception(excReason, ex);
            }
            catch (Exception ex)  // other exception, hope the ex.message will help
            {
                string excReason = "General Exception occurred at LoadPublicFromXml().";
                System.Diagnostics.Debug.WriteLine(excReason + " Exception Message: " + ex.Message);
                throw new Exception(excReason, ex);
            }
            // You might want to replace the Diagnostics.Debug with your Log statement
        }

        // Same as the previous one, but this time loading the private Key
        public void LoadPrivateFromXml(string privatePath)
        {
            if (!File.Exists(privatePath))
                throw new FileNotFoundException("File not exists: " + privatePath);
            try
            {
                rsa.FromXmlString(File.ReadAllText(privatePath));
                RSAParameters rsaParams = rsa.ExportParameters(true);
                D = new BigInteger(rsaParams.D);  // This parameter is only for private key
                Exponent = new BigInteger(rsaParams.Exponent);
                Modulus = new BigInteger(rsaParams.Modulus);
                isPrivateKeyLoaded = true;
                isPublicKeyLoaded = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception occurred at LoadPrivateFromXml()\nMessage: " + ex.Message);
                throw ex;
            }
        }

        // Encrypt data using private key
        public byte[] PrivateEncryption(byte[] data)
        {
            if (!IsPrivateKeyLoaded)  // is the private key has been loaded?
                throw new CryptographicException
                    ("Private Key must be loaded before using the Private Encryption method!");

            // Converting the byte array data into a BigInteger instance
            BigInteger bnData = new BigInteger(data);

            // (bnData ^ D) % Modulus - This Encrypt the data using the private Exponent: D
            BigInteger encData = bnData.modPow(D, Modulus);
            return encData.getBytes();
        }

        // Encrypt data using public key
        public byte[] PublicEncryption(byte[] data)
        {
            if (!IsPublicKeyLoaded)  // is the public key has been loaded?
                throw new CryptographicException
                    ("Public Key must be loaded before using the Public Encryption method!");

            // Converting the byte array data into a BigInteger instance
            BigInteger bnData = new BigInteger(data);

            // (bnData ^ Exponent) % Modulus - This Encrypt the data using the public Exponent
            BigInteger encData = bnData.modPow(Exponent, Modulus);

            return encData.getBytes();

        }


        public void createBlindFactor()
        {
            Boolean done = false;  //finding a blind factor
            Random rnd = new Random();//finding a blind factor
            blind = rnd.Next(1000000, 2100000000);//finding a blind factor
            while (done != true)//finding a blind factor
            {
                if (Modulus.gcd(blind) == 1) done = true;//finding a blind factor
                else blind = rnd.Next(1000000, 2100000000);//finding a blind factor

            }

            return;
        }


        public string retrieveBlindFactor()
        {
            return blind.ToString();

        }

        public void setBlindFactor(BigInteger tempBlind)
        {
            // Converting the byte array data into a BigInteger instance
            //BigInteger bnBlind = new BigInteger(tempBlind);
            //blind = bnBlind;
            blind = tempBlind;

        }

        public string PublicBlindEncryption(byte[] data)
        {

            // Converting the byte array data into a BigInteger instance
            BigInteger bnData = new BigInteger(data);


            enBlind = blind.modPow(Exponent, Modulus); //encrypting the blind factor
            BigInteger encBlindData = enBlind * bnData; //blinding the message with encrypted blindfactor

            //return encBlindData.getBytes();
            return encBlindData.ToString();

        }

        public string Sign(BigInteger data)
        {
            //BigInteger bnData = new BigInteger(data);   // Converting the byte array data into a BigInteger instance
            BigInteger bnData = data;
            BigInteger signeddata;
            signeddata = bnData.modPow(D, Modulus);    // Signning the blinded message
            //return signeddata.getBytes();
            return signeddata.ToString();
        }




        // Decrypt data using private key (for data encrypted with public key)
        public byte[] PrivateDecryption(byte[] encryptedData)
        {
            if (!IsPrivateKeyLoaded)  // is the private key has been loaded?
                throw new CryptographicException
                    ("Private Key must be loaded before using the Private Decryption method!");

            // Converting the encrypted data byte array data into a BigInteger instance
            BigInteger encData = new BigInteger(encryptedData);

            // (encData ^ D) % Modulus - This Decrypt the data using the private Exponent: D
            BigInteger bnData = encData.modPow(D, Modulus);
            BigInteger inverseb = blind.modInverse(Modulus);
            return bnData.getBytes();
        }


        public string unblind(BigInteger data)
        //public byte[] unblind(byte[] data)
        {

            // Converting the encrypted data byte array data into a BigInteger instance
            //BigInteger bnData = new BigInteger(data);
            BigInteger bnData = data;
            BigInteger unBlindedData;
            invblind = blind.modInverse(Modulus);   // Finding the invese of blind factor
            unBlindedData = invblind * bnData;      // Unblinding the signed message
            //return unBlindedData.getBytes();
            return unBlindedData.ToString();

        }

        public byte[] reveal(BigInteger data)
        {
            //BigInteger bnData = new BigInteger(data);
            BigInteger bnData = data;
            BigInteger revealedData;
            revealedData = bnData.modPow(Exponent, Modulus);   // Decrypting the singed message
            return revealedData.getBytes();

        }










        // Decrypt data using public key (for data encrypted with private key)
        public byte[] PublicDecryption(byte[] encryptedData)
        {
            if (!IsPublicKeyLoaded)  // is the public key has been loaded?
                throw new CryptographicException
                    ("Public Key must be loaded before using the Public Deccryption method!");

            // Converting the encrypted data byte array data into a BigInteger instance
            BigInteger encData = new BigInteger(encryptedData);

            // (encData ^ Exponent) % Modulus - This Decrypt the data using the public Exponent
            BigInteger bnData = encData.modPow(Exponent, Modulus);
            return bnData.getBytes();
        }

        // Implementation of IDisposable interface,
        // allow you to use this class as: using(RSAEncryption rsa = new RSAEncryption()) { ... }
        public void Dispose()
        {
            rsa.Clear();
        }
    }
}