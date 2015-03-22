using System;
using System.Security.Cryptography;
using System.Text;

namespace OS.Common.Encrypt
{
    public static class HmacSha1
    {
        /// <summary>
        /// 返回加密后的
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="encoding">如果为空，则默认Utf-8</param>
        /// <returns></returns>
        public static string EncryptBase64(string data,string key,Encoding encoding=null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var byties = encoding.GetBytes(data);
                using (HMACSHA1 hmac = new HMACSHA1(encoding.GetBytes(key)))
                {
                    byte[] digest = hmac.ComputeHash(byties);
                    return Convert.ToBase64String(digest);
                }
            
        }

    }
}
