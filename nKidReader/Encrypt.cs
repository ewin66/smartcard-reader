using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace nKidReader
{
    static class EncryptString
    {
        private static readonly string encryptionKey = "YOURSECRETKEY";
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            // If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(encryptionKey));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(encryptionKey);

            // Set the secret key for the tripleDES algorithm
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            // Transform the specified region of bytes array to resultArray
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            // Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString.Replace(' ', '+'));

            if (useHashing)
            {
                // If hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(encryptionKey));
                hashmd5.Clear();
            }
            else
            {
                // If hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(encryptionKey);
            }

            // Set the secret key for the tripleDES algorithm
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            // Return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        [STAThread]
        static void Main() { 
            Console.WriteLine(Encrypt("7cbe80709c9eab855e14ac10adc250ea", true));
            Console.WriteLine(Decrypt(Encrypt("7cbe80709c9eab855e14ac10adc250ea", true), true));
        }
    }
}
