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
                throw new ArgumentNullException("input", "Sha1加密的字符串不能为空！");

            if (encoding == null)
                encoding = Encoding.UTF8;

            var data = encoding.GetBytes(input);
            var encryData = Encrypt(data);

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < encryData.Length; i++)
            {
                sBuilder.Append(encryData[i].ToString("x2"));
            }

            // Return the hexadecimal string.
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
                throw new ArgumentNullException("bytes","Sha1加密的字节不能为空！");
            
            using (SHA1 sha1Hash = SHA1.Create())
            {
                return sha1Hash.ComputeHash(bytes);
            }
        }




    }
}
