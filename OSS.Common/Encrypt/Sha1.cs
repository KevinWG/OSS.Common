#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：sha1的加密实现
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
    /// Sha1加密类
    /// </summary>
    public static class Sha1
    {
        /// <summary>
        /// 获取Sha1加密值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Encrypt(string input, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input), "Sha1加密的字符串不能为空！");

            if (encoding == null)
                encoding = Encoding.UTF8;

            var data = encoding.GetBytes(input);
            var encryData = Encrypt(data);

            var sBuilder = new StringBuilder(encryData.Length*2);
            foreach (var t in encryData)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            
            return sBuilder.ToString();

        }


        /// <summary>
        /// 获取Sha1加密值
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                throw new ArgumentNullException(nameof(bytes),"Sha1加密的字节不能为空！");
            
            using (var sha1Hash = SHA1.Create())
            {
                return sha1Hash.ComputeHash(bytes);
            }
        }
        
    }
}
