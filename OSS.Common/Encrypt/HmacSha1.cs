#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：加盐sha1的加解密实现
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion
using System;
using System.Security.Cryptography;
using System.Text;

namespace OSS.Common.Encrypt
{
    /// <summary>
    /// HMACSHA1 加密类
    /// </summary>
    [Obsolete]
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

            var resultbytes = Encrypt(keyBytes, bytes);

            return Convert.ToBase64String(resultbytes);
        }

        /// <summary>
        /// 返回加密后的
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="encoding">如果为空，则默认Utf-8</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var bytes = encoding.GetBytes(data);
            var keyBytes = encoding.GetBytes(key);

            var resultbytes = Encrypt(keyBytes, bytes);

            return encoding.GetString(resultbytes);
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

    /// <summary>
    /// HMAC哈希加密类
    /// </summary>
    public static class HMACSHA
    {
        /// <summary>
        /// 返回加密后的
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="encoding">如果为空，则默认Utf-8</param>
        /// <param name="encryType">HMAC加密类型：SHA1，SHA256，SHA384，SHA512，MD5</param>
        /// <returns> 解密后的字节流通过Base64转化 </returns>
        public static string EncryptBase64(string data, string key, Encoding encoding = null, string encryType = "SHA1")
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var bytes = encoding.GetBytes(data);
            var keyBytes = encoding.GetBytes(key);

            var resultbytes = Encrypt(keyBytes, bytes, encryType);

            return Convert.ToBase64String(resultbytes);
        }

        /// <summary>
        /// 返回加密后的
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="encoding">如果为空，则默认Utf-8</param>
        /// <param name="encryType">HMAC加密类型：SHA1，SHA256，SHA384，SHA512，MD5</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key, Encoding encoding = null, string encryType = "SHA1")
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            var bytes = encoding.GetBytes(data);
            var keyBytes = encoding.GetBytes(key);

            var resultbytes = Encrypt(keyBytes, bytes, encryType);

            return encoding.GetString(resultbytes);
        }


        /// <summary>
        /// HMAC加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bytes"></param>
        /// <param name="encryType">HMAC加密类型：SHA1，SHA256，SHA384，SHA512，MD5</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] key, byte[] bytes, string encryType = "SHA1")
        {
            byte[] resultbytes;
            using (var hmac = GetCryptAlgorithm(key, encryType))
            {
                resultbytes = hmac.ComputeHash(bytes);
            }
            return resultbytes;
        }

        private static HMAC GetCryptAlgorithm(byte[] key, string encryType )
        {
            switch (encryType)
            {
                case "SHA256":
                    return new HMACSHA256(key);
                case "SHA384":
                    return new HMACSHA384(key);
                case "SHA512":
                    return new HMACSHA512(key);
                case "MD5":
                    return new HMACMD5(key);
            }
            return new HMACSHA1(key);
        }
    }
}
