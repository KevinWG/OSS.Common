using System;
using System.Security.Cryptography;
using System.Text;

namespace OS.Common.Encrypt
{
    public static class HMACSHA1Encrypt
    {
        /// <summary>
        /// 返回加密后的
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string EncryptBase64(string data,string key,Encoding encoding)
        {
            var byties = encoding.GetBytes(data);
            using (HMACSHA1 hmac = new HMACSHA1(encoding.GetBytes(key)))
            {
                byte[] digest = hmac.ComputeHash(byties);
                return Convert.ToBase64String(digest);
            }
        }

    }
}
