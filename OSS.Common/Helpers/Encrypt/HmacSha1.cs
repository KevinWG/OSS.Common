#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：加盐sha1的加解密实现
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion
using System.Security.Cryptography;
using System.Text;

namespace OSS.Common.Encrypt;

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
    /// <param name="encryptType">HMAC加密类型：SHA1，SHA256，SHA384，SHA512，MD5</param>
    /// <returns> 解密后的字节流通过Base64转化 </returns>
    public static string EncryptBase64(string data, string key, Encoding? encoding = null, string encryptType = "SHA1")
    {
        encoding ??= Encoding.UTF8;

        var bytes    = encoding.GetBytes(data);
        var keyBytes = encoding.GetBytes(key);

        var resultBytes = Encrypt(keyBytes, bytes, encryptType);

        return Convert.ToBase64String(resultBytes);
    }

    /// <summary>
    /// 返回加密后的
    /// </summary>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <param name="encoding">如果为空，则默认Utf-8</param>
    /// <param name="encryptType">HMAC加密类型：SHA1，SHA256，SHA384，SHA512，MD5</param>
    /// <returns></returns>
    public static string Encrypt(string data, string key, Encoding? encoding = null, string encryptType = "SHA1")
    {
        encoding ??= Encoding.UTF8;

        var bytes    = encoding.GetBytes(data);
        var keyBytes = encoding.GetBytes(key);

        var resultBytes = Encrypt(keyBytes, bytes, encryptType);

        return encoding.GetString(resultBytes);
    }


    /// <summary>
    /// HMAC加密
    /// </summary>
    /// <param name="key"></param>
    /// <param name="bytes"></param>
    /// <param name="encryptType">HMAC加密类型：SHA1，SHA256，SHA384，SHA512，MD5</param>
    /// <returns></returns>
    public static byte[] Encrypt(byte[] key, byte[] bytes, string encryptType = "SHA1")
    {
        byte[] resultBytes;
        using (var hmac = GetCryptAlgorithm(key, encryptType))
        {
            resultBytes = hmac.ComputeHash(bytes);
        }

        return resultBytes;
    }

    private static HMAC GetCryptAlgorithm(byte[] key, string encryptType)
    {
        switch (encryptType)
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