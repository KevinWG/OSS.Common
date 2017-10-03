#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：用户基础信息
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System;
using System.Threading;
using OSS.Common.Encrypt;
using OSS.Common.Extention;

namespace OSS.Common.Authrization
{
    /// <summary>
    /// 当前系统访问上下文信息
    /// </summary>
    public static class MemberShiper
    {
        #region  当前应用授权信息

        private static readonly AsyncLocal<AppAuthorizeInfo> _appAuthorize = new AsyncLocal<AppAuthorizeInfo>();

        /// <summary>
        ///   应用授权信息
        /// </summary>
        public static AppAuthorizeInfo AppAuthorize => _appAuthorize.Value;

        #endregion

        #region  成员信息

        private static readonly AsyncLocal<MemberIdentity> _identity = new AsyncLocal<MemberIdentity>();

        /// <summary>
        ///   成员身份信息
        /// </summary>
        public static MemberIdentity Identity => _identity.Value;

        #endregion

        /// <summary>
        /// 是否已经验证
        /// </summary>
        public static bool IsAuthenticated => _identity.Value != null;


        #region   设置相关信息

        /// <summary>
        ///   设置用户信息
        /// </summary>
        /// <param name="info"></param>
        public static void SetIdentity(MemberIdentity info)
        {
            _identity.Value = info;
        }

        /// <summary>
        ///   设置应用授权信息
        /// </summary>
        /// <param name="info"></param>
        public static void SetAppAuthrizeInfo(AppAuthorizeInfo info)
        {
            _appAuthorize.Value = info;
        }

        #endregion

        #region    token  处理

        /// <summary>
        /// 获取成员扩展详情
        /// </summary>
        /// <typeparam name="TMInfo"></typeparam>
        /// <returns></returns>
        public static TMInfo GetMemberInfo<TMInfo>()
            where TMInfo : class => _identity.Value?.MemberInfo as TMInfo;

        /// <summary>
        /// 通过 ID 生成对应的Token
        /// </summary>
        /// <param name="encryptKey"></param>
        /// <param name="tokenDetail"></param>
        /// <returns></returns>
        public static string GetToken(string encryptKey, string tokenDetail)
        {
            return AesRijndael.Encrypt(tokenDetail, encryptKey).Base64UrlEncode();
        }

        /// <summary>
        ///  通过token解析出对应的id和key
        /// </summary>
        /// <param name="encryptKey"></param>
        /// <param name="token"></param>
        /// <returns>返回解析信息，Item1为id，Item2为key</returns>
        public static string GetTokenDetail(string encryptKey, string token)
        {
            var tokenDetail = AesRijndael.Decrypt(token.Base64UrlDecode(), encryptKey);

            if (string.IsNullOrEmpty(tokenDetail))
                throw new ArgumentNullException(nameof(token), "不合法的用户Token");

            return tokenDetail;
        }

        #endregion
    }

    /// <summary>
    /// 成员通行证信息
    /// </summary>
    public class MemberIdentity
    {
        /// <summary>
        /// 授权类型
        /// </summary>
        public int AuthenticationType { get; set; }

        /// <summary>
        ///   成员Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 成员扩展信息
        /// </summary>
        public object MemberInfo { get; set; }
    }
}