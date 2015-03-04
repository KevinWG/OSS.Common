using System;
using System.Security.Cryptography;
using System.Text;

namespace OS.Common.Encrypt
{
    public class AESRijndael
    {
        /// <summary>
        ///    aes 加密
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(toEncrypt))
            {
                return result;
            }
            try
            {
                byte[] keyArray = ToByte(key);
                byte[] toEncryptArray = Encoding.Default.GetBytes(toEncrypt);
                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                rDel.Clear();
                result = ToText(resultArray);
            }
            catch
            {

            }
            return result;
        }

        public static string Decrypt(string toDecrypt, string key)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(toDecrypt))
            {
                return result;
            }
            try
            {
                byte[] keyArray = ToByte(key);
                byte[] toEncryptArray = ToByte(toDecrypt);
                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                rDel.Clear();
                result= UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch { }
            return result;
        }
        private static string HEX = "0123456789ABCDEF";
        private static string ToText(byte[] buf)
        {
            if (buf == null)
                return string.Empty;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buf.Length; i++)
            {
                builder.Append(HEX[(buf[i] >> 4) & 0x0f]).Append(HEX[buf[i] & 0x0f]);
            }
            return builder.ToString();
        }
        private static byte[] ToByte(String hexString)
        {
            int len = hexString.Length / 2;
            byte[] result = new byte[len];
            for (int i = 0; i < len; i++)
            {
                result[i] = Convert.ToByte(Convert.ToInt32(hexString.Substring(2 * i, 2), 16));
            }
            return result;
        }
    }
}
