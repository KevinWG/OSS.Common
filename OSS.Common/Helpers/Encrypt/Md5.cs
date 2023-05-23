#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：md5加密实现
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion
using System.Security.Cryptography;
using System.Text;

namespace OSS.Common.Encrypt
{
    /// <summary>
    /// MD5加密类
    /// </summary>
    public static class Md5
    {
        /// <summary>
        /// 获取16位MD5值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HalfEncryptHexString(string input)
        {
            var result = EncryptHexString(input);

            return !string.IsNullOrEmpty(result) ? result[..16] : result;
        }


        /// <summary>
        /// 获取MD5加密值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string EncryptHexString(string input, Encoding? encoding = null)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input), "MD5加密的字符串不能为空！");

            encoding ??= Encoding.UTF8;

            var data = encoding.GetBytes(input);
            var encryptData = Encrypt(data);

            var sBuilder = new StringBuilder(encryptData.Length*2);
            foreach (var t in encryptData)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            
            return sBuilder.ToString();

        }


        /// <summary>
        /// 获取MD5加密值
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                throw new ArgumentNullException(nameof(bytes),"MD5加密的字节不能为空！");
            
            using (var md5Hash = MD5.Create())
            {
                return md5Hash.ComputeHash(bytes);
            }
        }
    }
}
