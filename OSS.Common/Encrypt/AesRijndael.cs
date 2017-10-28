#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：aes加密实现
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
    /// Aes加密
    /// </summary>
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
            var result = string.Empty;

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key值不能为空");

            if (string.IsNullOrEmpty(toEncrypt))
                return result;

            if (encoding == null)
                encoding = Encoding.UTF8;
            
            var keyArray = encoding.GetBytes(key); // ToByte(key);
            var toEncryptArray = encoding.GetBytes(toEncrypt);
            var resultArray = Encrypt(keyArray, toEncryptArray);
            result = Convert.ToBase64String(resultArray);

            return result;
        }


        /// <summary>
        ///   加密
        /// </summary>
        /// <param name="keyArray"></param>
        /// <param name="toEncryptArray"></param>
        /// <param name="iv">偏移量</param>
        /// <param name="keySize">key大小</param>
        /// <param name="blockSize">块大小</param>
        /// <param name="cipherMode"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] keyArray, byte[] toEncryptArray, byte[] iv = null, int keySize = 256, int blockSize = 128, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            using (var rDel = Aes.Create())
            {
                rDel.KeySize = keySize;
                rDel.BlockSize = blockSize;
                
                rDel.Key = keyArray;
                if (iv != null)
                {
                    rDel.IV = iv;
                }

                rDel.Mode = cipherMode;
                rDel.Padding = paddingMode;

                var cTransform = rDel.CreateEncryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return resultArray;
            }
        }


        /// <summary>
        ///    解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <param name="encoding"> 编码方式 不传值默认为  utf-8 </param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key, Encoding encoding = null)
        {
            var result = string.Empty;

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key值不能为空");

            if (string.IsNullOrEmpty(toDecrypt))
                return result;

            if (encoding == null)
                encoding = Encoding.UTF8;

            var keyArray = encoding.GetBytes(key); // ToByte(key);
            var toEncryptArray = Convert.FromBase64String(toDecrypt); // ToByte(toDecrypt);
            var resultArray = Decrypt(keyArray, toEncryptArray);
            result = encoding.GetString(resultArray);

            return result;
        }


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="keyArray">key的字节流</param>
        /// <param name="toEncryptArray">加密串的字节流</param>
        /// <param name="iv">偏移量</param>
        /// <param name="keySize">key大小</param>
        /// <param name="blockSize">块大小</param>
        /// <param name="cipherMode"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] keyArray, byte[] toEncryptArray,byte[] iv=null,int keySize=256,int blockSize=128, CipherMode cipherMode= CipherMode.ECB, PaddingMode paddingMode= PaddingMode.PKCS7)
        {
            using (var rDel =Aes.Create())
            {
                rDel.KeySize = keySize;
                rDel.BlockSize = blockSize;

                rDel.Mode = cipherMode;
                rDel.Padding = paddingMode;

                rDel.Key = keyArray;
                if (iv!=null)
                {
                    rDel.IV = iv;
                }

                var cTransform = rDel.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return resultArray;
            }
        }
    }
}
