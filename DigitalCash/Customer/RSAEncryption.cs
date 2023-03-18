using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Xml;
using System.Numerics;

namespace Customer
{
    public class RSAEncryption : IDisposable
    {
        // Members:
        // RSA Key components (just the three I'm using, there is more...)
        private BigInteger D = new BigInteger();
        private BigInteger Exponent = new BigInteger();
        private BigInteger Modulus = new BigInteger();
        private BigInteger blind = new BigInteger();
        private BigInteger enBlind = new BigInteger();
        private BigInteger invblind = new BigInteger();

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
                Exponent = new BigInteger(rsaParams.Exponent);
                Modulus = new BigInteger(rsaParams.Modulus);
                isPublicKeyLoaded = true;
                isPrivateKeyLoaded = false;
            }
            // Examle for the proper use of try - catch blocks: Informing the main app where and why the Exception occurred
            catch (XmlSyntaxException ex)  // Not an xml file
            {
                string excReason = "Exception occurred at LoadPublicFromXml(), Selected file is not a valid xml file.";
                System.Diagnostics.Debug.WriteLine(excReason + " Exception Message: " + ex.Message);
                throw new Exception(excReason, ex);
            }
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
                MessageBox.Show(D.ToString(), "D");
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
            BigInteger encData = BigInteger.ModPow(bnData, D, Modulus);
            return encData.ToByteArray();
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
            BigInteger encData = BigInteger.ModPow(bnData, Exponent, Modulus);

            return encData.ToByteArray();

        }


        public void createBlindFactor()
        {
            Boolean done = false;  //finding a blind factor
            Random rnd = new Random();//finding a blind factor
            blind = rnd.Next(1000000, 2100000000);//finding a blind factor
            while (done != true)//finding a blind factor
            {
                if (BigInteger.GreatestCommonDivisor(Modulus, blind) == 1) done = true;//finding a blind factor
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


            enBlind = BigInteger.ModPow(blind, Exponent, Modulus); //encrypting the blind factor
            BigInteger encBlindData = enBlind * bnData; //blinding the message with encrypted blindfactor

            //return encBlindData.getBytes();
            return encBlindData.ToString();

        }

        public string Sign(BigInteger data)
        {
            //BigInteger bnData = new BigInteger(data);   // Converting the byte array data into a BigInteger instance
            BigInteger bnData = data;
            BigInteger signeddata;
            signeddata = BigInteger.ModPow(bnData, D, Modulus);    // Signning the blinded message
            //return signeddata.getBytes();
            return signeddata.ToString();
        }

        public static BigInteger Egcd(BigInteger left,
                              BigInteger right,
                          out BigInteger leftFactor,
                          out BigInteger rightFactor)
        {
            leftFactor = 0;
            rightFactor = 1;
            BigInteger u = 1;
            BigInteger v = 0;
            BigInteger gcd = 0;

            while (left != 0)
            {
                BigInteger q = right / left;
                BigInteger r = right % left;

                BigInteger m = leftFactor - u * q;
                BigInteger n = rightFactor - v * q;

                right = left;
                left = r;
                leftFactor = u;
                rightFactor = v;
                u = m;
                v = n;

                gcd = right;
            }

            return gcd;
        }

        public static BigInteger ModInverse(BigInteger value, BigInteger modulo)
        {
            BigInteger x, y;

            if (1 != Egcd(value, modulo, out x, out y))
                throw new ArgumentException("Invalid modulo", nameof(modulo));

            if (x < 0)
                x += modulo;

            return x % modulo;
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
            BigInteger bnData = BigInteger.ModPow(encData, D, Modulus);
            //BigInteger inverseb = ModInverse(blind, Modulus);
            return bnData.ToByteArray();
        }


        public string Unblind(BigInteger data)
        //public byte[] unblind(byte[] data)
        {

            // Converting the encrypted data byte array data into a BigInteger instance
            //BigInteger bnData = new BigInteger(data);
            BigInteger bnData = data;
            BigInteger unBlindedData;
            BigInteger invblind = ModInverse(blind, Modulus);   // Finding the inverse of blind factor
            //BigInteger invBlind = BigInteger.ModPow(blind, Modulus - 2, Modulus);
            unBlindedData = invblind * bnData;      // Unblinding the signed message
            //return unBlindedData.getBytes();
            return unBlindedData.ToString();

        }

        public byte[] reveal(BigInteger data)
        {
            //BigInteger bnData = new BigInteger(data);
            BigInteger bnData = data;
            BigInteger revealedData;
            revealedData = BigInteger.ModPow(bnData, Exponent, Modulus);   // Decrypting the singed message
            return revealedData.ToByteArray();

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
            BigInteger bnData = BigInteger.ModPow(encData, Exponent, Modulus);
            return bnData.ToByteArray();
        }

        // Implementation of IDisposable interface,
        // allow you to use this class as: using(RSAEncryption rsa = new RSAEncryption()) { ... }
        public void Dispose()
        {
            rsa.Clear();
        }

        public string PrintExponent()
        {
            return Exponent.ToString();
        }

        public string PrintModulus()
        {
            return Modulus.ToString();
        }
    }
}
