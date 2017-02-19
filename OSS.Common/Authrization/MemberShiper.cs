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
using OSS.Common.Encrypt;
using OSS.Common.Extention;

namespace OSS.Common.Authrization
{
    /// <summary>
    /// 当前系统访问上下文信息
    /// </summary>
    public class MemberShiper
    {
        #region  当前应用授权信息

        [ThreadStatic] private static SysAuthorizeInfo _appAuthorize;

        /// <summary>
        ///   应用授权信息
        /// </summary>
        public static SysAuthorizeInfo AppAuthorize => _appAuthorize;

        #endregion

        #region   用户信息


        [ThreadStatic] private static MemberInfo _memberInfo;

        /// <summary>
        ///   登陆用户信息
        /// </summary>
        public static MemberInfo MemberInfo => _memberInfo;

        #endregion

        /// <summary>
        /// 是否已经验证
        /// </summary>
        public static bool IsMemberAuthorized => MemberInfo != null && (MemberInfo.Id > 0 || !string.IsNullOrEmpty(MemberInfo.Key));
    
        
        #region   设置相关信息

        /// <summary>
        ///   设置用户信息
        /// </summary>
        /// <param name="info"></param>
        public static void SetMemberInfo(MemberInfo info)
        {
            _memberInfo = info;
        }



        /// <summary>
        ///   设置应用授权信息
        /// </summary>
        /// <param name="info"></param>
        public static void SetAppAuthrizeInfo(SysAuthorizeInfo info)
        {
            _appAuthorize = info;
        }

        #endregion

        #region    token  处理

        /// <summary>
        /// 通过 ID 生成对应的Token
        /// </summary>
        /// <param name="encryptKey"></param>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetToken(string encryptKey, long id,string key=null)
        {
            string sourceData = string.Concat(id, "|", key,"|" ,DateTime.Now.ToUtcSeconds());
            return AesRijndael.Encrypt(sourceData, encryptKey).Base64UrlEncode();
        }

        /// <summary>
        ///  通过token解析出对应的id和key
        /// </summary>
        /// <param name="encryptKey"></param>
        /// <param name="token"></param>
        /// <returns>返回解析信息，Item1为id，Item2为key</returns>
        public static Tuple<long, string> GetTokenDetail(string encryptKey, string token)
        {
            string tokenDetail = AesRijndael.Decrypt(token.Base64UrlDecode(), encryptKey);

            if (string.IsNullOrEmpty(tokenDetail))
                throw new ArgumentNullException("token", "不合法的用户Token");

            string[] memberIdSplit = tokenDetail.Split('|');

            return Tuple.Create(memberIdSplit[0].ToInt64(), memberIdSplit[1]);
        }

        #endregion


    }



    /// <summary>
    /// 授权用户信息
    /// </summary>
    public class MemberInfo
    {
        /// <summary>
        ///   其他key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///   会员ID 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///   用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 用户数据
        /// </summary>
        public object MemberData { get; set; }
    }
}