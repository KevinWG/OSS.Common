using System;
using System.Security.Cryptography;
using System.Text;

namespace OSS.Common.Encrypt
{
    /// <summary>
    /// 
    /// </summary>
    public static class HmacSha1
    {
        /// <summary>
        /// 返回加密后的
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="encoding">如果为空，则默认Utf-8</param>
        /// <returns> 解密后的字节流通过Base64转化 </returns>
        public static string EncryptBase64(string data, string key, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var bytes = encoding.GetBytes(data);
            var keyBytes = encoding.GetBytes(key);

            byte[] resultbytes = Encrypt(keyBytes, bytes);

            return Convert.ToBase64String(resultbytes);
        }

        /// <summary>
        /// 返回加密后的
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="encoding">如果为空，则默认Utf-8</param>
        /// <returns></returns>
        public static string EncryptUtf8(string data, string key, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var bytes = encoding.GetBytes(data);
            var keyBytes = encoding.GetBytes(key);

            byte[] resultbytes = Encrypt(keyBytes,  bytes);

            return Encoding.UTF8.GetString(resultbytes);
        }


        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static byte[] Encrypt(byte[] key, byte[] bytes)
        {
            byte[] resultbytes;
            using (var hmac = new HMACSHA1(key))
            {
                resultbytes = hmac.ComputeHash(bytes);
            }
            return resultbytes;
        }
    }
}
