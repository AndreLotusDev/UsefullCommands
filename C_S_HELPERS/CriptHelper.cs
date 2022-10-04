using System;
using System.Security.Cryptography;
using System.Text;

namespace Helpers
{
    public class CriptHelper
    {
        public static string KEY = "zL^UGk6t^*PD$7*Q";
        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();

            string stringFormatted = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            stringFormatted = stringFormatted.Replace('+', '-');
            stringFormatted = stringFormatted.Replace('/', '_');

            return stringFormatted;
        }
        public static string Decrypt(string input, string key)
        {

            input = input.Replace('-', '+');
            input = input.Replace('_', '/');

            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();

            var stringFormatted = UTF8Encoding.UTF8.GetString(resultArray);
            return stringFormatted;
        }

        public static string ToSha256(string text)
        {
            using (var sha256 = new SHA256Managed())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", "");
            }
        }
    }
}
