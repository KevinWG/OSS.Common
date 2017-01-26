using System;
using OSS.Common.ComUtils;
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
        public static SysAuthorizeInfo AppAuthorize
        {
            get { return _appAuthorize; }
        }

        #endregion

        #region   用户信息


        [ThreadStatic] private static MemberInfo _memberInfo;

        /// <summary>
        ///   登陆用户信息
        /// </summary>
        public static MemberInfo MemberInfo
        {
            get { return _memberInfo; }
        }

        #endregion

        /// <summary>
        /// 是否已经验证
        /// </summary>
        public static bool IsMemberAuthorized
        {
            get { return MemberInfo != null && MemberInfo.UserId > 0; }
        }


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

        //private static readonly string _encryptKey = ConfigUtil.GetAppSetting("TokenSecretKey");

        /// <summary>
        /// 通过 ID 生成对应的Token
        /// </summary>
        /// <param name="encryptKey"></param>
        /// <param name="userId"></param>
        /// <param name="employeeId"></param>
        /// <param name="isEmployee"></param>
        /// <returns></returns>
        public static string GetToken(string encryptKey, long userId, long employeeId = 0, bool isEmployee = false)
        {
            string sourceData = string.Concat(userId, "|", employeeId, "|", isEmployee, "|", DateTime.Now.ToUtcSeconds());
            return AesRijndael.Encrypt(sourceData, encryptKey).Base64UrlEncode();
        }

        /// <summary>
        /// 通过 ID 生成对应的Token
        /// </summary>
        /// <param name="encryptKey"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static MemberInfo GetTokenDetail(string encryptKey, string token)
        {
            string tokenDetail = AesRijndael.Decrypt(token.Base64UrlDecode(), encryptKey);

            if (string.IsNullOrEmpty(tokenDetail))
                throw new ArgumentNullException("token", "不合法的用户Token");

            string[] memberIdSplit = tokenDetail.Split('|');

            var memberInfo = new MemberInfo();

            memberInfo.UserId = memberIdSplit[0].ToInt64();
            memberInfo.EmployeeId = memberIdSplit[1].ToInt64();
            memberInfo.IsEmployee = memberIdSplit[2].ToBoolean();

            return memberInfo;
        }

        #endregion
    }



    /// <summary>
    /// 授权用户信息
    /// </summary>
    public class MemberInfo
    {
        /// <summary>
        ///   会员ID 
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///   员工Id
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        ///   用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        ///   是否是后台员工
        ///    如果员工以会员身份登录，则UserId  和  EmployeeId  都有值
        /// </summary>
        public bool IsEmployee { get; set; }

        /// <summary>
        /// 用户数据
        /// </summary>
        public object MemberData { get; set; }
    }
}