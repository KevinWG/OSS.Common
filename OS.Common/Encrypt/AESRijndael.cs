using System;
using System.Security.Cryptography;
using System.Text;

namespace OS.Common.Encrypt
{
    public class AesRijndael
    {
        /// <summary>
        ///    aes 加密
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <param name="encoding">加密编码方式    默认为   utf-8  </param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key, Encoding encoding = null)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key值不能为空");

            if (string.IsNullOrEmpty(toEncrypt))
                return result;

            if (encoding==null)
                encoding=Encoding.UTF8;

            try
            {
                byte[] keyArray = encoding.GetBytes(key);// ToByte(key);
                byte[] toEncryptArray = encoding.GetBytes(toEncrypt);
                var resultArray = Encrypt(keyArray, toEncryptArray);
                result = Convert.ToBase64String(resultArray);
            }
            catch
            {

            }
            return result;
        }


        /// <summary>
        ///   加密
        /// </summary>
        /// <param name="keyArray"></param>
        /// <param name="toEncryptArray"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] keyArray, byte[] toEncryptArray)
        {
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            rDel.Clear();
            return resultArray;
        }


        /// <summary>
        ///    解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <param name="encoding"> 编码方式 不传值默认为  utf-8 </param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key,Encoding encoding=null)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key","key值不能为空");
            
            if (string.IsNullOrEmpty(toDecrypt))
                return result;
            
            if(encoding==null)
                encoding=Encoding.UTF8;

            try
            {
                byte[] keyArray = encoding.GetBytes(key);// ToByte(key);
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);// ToByte(toDecrypt);
                var resultArray = Decrypt(keyArray, toEncryptArray);
                result = encoding.GetString(resultArray);
            }
            catch { }
            return result;
        }


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="keyArray">key的字节流</param>
        /// <param name="toEncryptArray">加密串的字节流</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] keyArray, byte[] toEncryptArray)
        {
            using (RijndaelManaged rDel = new RijndaelManaged())
            {
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return resultArray;
            }
        }


        //private static string HEX = "0123456789abcdef";
        //private static string ToText(byte[] buf)
        //{
        //    if (buf == null)
        //        return string.Empty;
        //    StringBuilder builder = new StringBuilder();
        //    for (int i = 0; i < buf.Length; i++)
        //    {
        //        builder.Append(HEX[(buf[i] >> 4) & 0x0f]).Append(HEX[buf[i] & 0x0f]);
        //    }
        //    return builder.ToString();
        //}
        //private static byte[] ToByte(String hexString)
        //{
        //    int len = hexString.Length / 2;
        //    byte[] result = new byte[len];
        //    for (int i = 0; i < len; i++)
        //    {
        //        result[i] = Convert.ToByte(Convert.ToInt32(hexString.Substring(2 * i, 2), 16));
        //    }
        //    return result;
        //}
    }
}
