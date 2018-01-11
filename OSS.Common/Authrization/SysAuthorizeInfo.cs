#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：通用系统授权信息
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System;
using System.Text;
using OSS.Common.Encrypt;
using OSS.Common.Extention;

namespace OSS.Common.Authrization
{
    /// <summary>
    ///  旧应用的授权认证信息
    ///  todelete
    /// </summary>
    [Obsolete]
    public class SysAuthorizeInfo : AppAuthorizeInfo
    {
        #region  字符串处理

        /// <summary>
        ///   从头字符串中初始化签名相关属性信息
        /// </summary>
        /// <param name="signData"></param>
        /// <param name="separator">A=a  B=b 之间分隔符</param>
        public override void FromSignData(string signData, char separator = ';')
        {
            if (string.IsNullOrEmpty(signData)) return;

            var strs = signData.Split(separator);
            foreach (var str in strs)
            {
                if (string.IsNullOrEmpty(str)) continue;

                var keyValue = str.Split(new[] {'='}, 2);
                if (keyValue.Length <= 1) continue;

                var val = keyValue[1].UrlDecode();
                switch (keyValue[0])
                {
                    case "app_version":
                        AppVersion = val;
                        break;
                    case "token":
                        Token = val;
                        break;
                    case "app_source":
                        AppSource = val;
                        break;
                    case "app_client":
                        AppClient = val.ToInt32();
                        break;
                    case "tenant_id":
                        TenantId = val.ToInt64();
                        break;

                    case "sign":
                        Sign = val;
                        break;
                    case "device_id":
                        DeviceId = val;
                        break;
                    case "timespan":
                        TimeSpan = val.ToInt64();
                        break;
                    case "ip_address":
                        IpAddress = val;
                        break;
                    case "web_browser":
                        WebBrowser = val;
                        break;
                    case "pro_code":
                        ProCode = val;
                        break;
                }
            }
        }

        #endregion
        
        /// <summary>
        ///   获取要加密签名的串
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        protected internal override StringBuilder GetSignContent(char separator)
        {
            var strTicketParas = new StringBuilder();
            if (AppClient > 0)
                AddSignDataValue("app_client", AppClient.ToString(), separator, strTicketParas);

            AddSignDataValue("app_source", AppSource, separator, strTicketParas);
            AddSignDataValue("app_version", AppVersion, separator, strTicketParas);
            AddSignDataValue("device_id", DeviceId, separator, strTicketParas);
            AddSignDataValue("ip_address", IpAddress, separator, strTicketParas);

            AddSignDataValue("pro_code", ProCode, separator, strTicketParas);
            if (TenantId>0)
            {
                AddSignDataValue("tenant_id", TenantId.ToString(), separator, strTicketParas);
            }
            AddSignDataValue("timespan", TimeSpan.ToString(), separator, strTicketParas);
            AddSignDataValue("token", Token, separator, strTicketParas);
            AddSignDataValue("web_browser", WebBrowser, separator, strTicketParas);

            return strTicketParas;
        }
    }

    /// <summary>
    ///   应用的授权认证信息
    /// </summary>
    public class AppAuthorizeInfo
    {
        #region  参与签名属性

        /// <summary>
        ///   应用版本
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        ///   应用来源
        /// </summary>
        public string AppSource { get; set; }

        /// <summary>
        /// 应用客户端类型
        ///   用户自定义，也可使用 AppClientType
        /// </summary>
        public int AppClient { get; set; }

        ///// <summary>
        ///// 用户客户端类型
        ///// </summary>
        //public int UserClient { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        ///  Token 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public long TimeSpan { get; set; }

        /// <summary>
        ///  sign标识
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 浏览器类型   可选
        /// </summary>
        public string WebBrowser { get; set; }

        /// <summary>
        /// IP地址 可选 手机App端由接收方赋值
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        ///  租户ID  
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        ///  推广码
        /// </summary>
        public string ProCode { get; set; }
        
        #endregion
        
        /// <summary>
        ///  operate tag 单次操作标识
        /// </summary>
        public string OTag { get; set; }

        #region  字符串处理

        /// <summary>
        ///   从头字符串中初始化签名相关属性信息
        /// </summary>
        /// <param name="signData"></param>
        /// <param name="separator">A=a  B=b 之间分隔符</param>
        public virtual void FromSignData(string signData, char separator = ';')
        {
            if (string.IsNullOrEmpty(signData)) return;

            var strs = signData.Split(separator);
            foreach (var str in strs)
            {
                if (string.IsNullOrEmpty(str)) continue;

                var keyValue = str.Split(new[] {'='}, 2);
                if (keyValue.Length <= 1) continue;

                var val = keyValue[1].UrlDecode();
                switch (keyValue[0])
                {
                    case "av":
                        AppVersion = val;
                        break;
                    case "as":
                        AppSource = val;
                        break;
                    case "ac":
                        AppClient = val.ToInt32();
                        break;
                    case "did":
                        DeviceId = val;
                        break;
                    case "ip":
                        IpAddress = val;
                        break;
                    case "tid":
                        TenantId = val.ToInt64();
                        break;
                    case "pc":
                        ProCode = val;
                        break;
                    case "ts":
                        TimeSpan = val.ToInt64();
                        break;
                    case "tn":
                        Token = val;
                        break;
                    case "sign":
                        Sign = val;
                        break;
                    case "wb":
                        WebBrowser = val;
                        break;
                }
            }
        }

        /// <summary>
        /// 生成签名后的字符串
        /// </summary>
        /// <returns></returns>
        public string ToSignData(string secretKey, char separator = ';')
        {
            TimeSpan = DateTime.Now.ToUtcSeconds();

            var encrpStr = GetSignContent(separator);
            Sign = HMACSHA.EncryptBase64(encrpStr.ToString(), secretKey);

            AddSignDataValue("sign", Sign, separator, encrpStr);

            return encrpStr.ToString();
        }


        /// <summary>
        /// 复制新的授权信息实体
        /// </summary>
        /// <returns></returns>
        public AppAuthorizeInfo Copy()
        {
            var newOne = new AppAuthorizeInfo
            {
                AppClient = this.AppClient,
                AppSource = this.AppSource,
                AppVersion = this.AppVersion,
                DeviceId = this.DeviceId,
                IpAddress = this.IpAddress,

                TenantId = this.TenantId,
                Sign = this.Sign,
                TimeSpan = this.TimeSpan,
                Token = this.Token,
                WebBrowser = this.WebBrowser,

                ProCode = this.ProCode
            };


            return newOne;
        }

        #endregion

        #region  签名相关

        /// <summary>
        ///   检验是否合法
        /// </summary>
        /// <returns></returns>
        public bool CheckSign(string secretKey, char separator = ';')
        {
            var strTicketParas = GetSignContent(separator);

            var signData = HmacSha1.EncryptBase64(strTicketParas.ToString(), secretKey);

            return Sign == signData;
        }

        /// <summary>
        ///   获取要加密签名的串
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        protected internal virtual StringBuilder GetSignContent(char separator)
        {
            var strTicketParas = new StringBuilder();
            if (AppClient>0)
                AddSignDataValue("ac", AppClient.ToString(), separator, strTicketParas);
           
            AddSignDataValue("as", AppSource, separator, strTicketParas);
            AddSignDataValue("av", AppVersion, separator, strTicketParas);
            AddSignDataValue("did", DeviceId, separator, strTicketParas);
            AddSignDataValue("ip", IpAddress, separator, strTicketParas);

            AddSignDataValue("pc", ProCode, separator, strTicketParas);
            if (TenantId>0)
            {
                AddSignDataValue("tid", TenantId.ToString(), separator, strTicketParas);
            }
            AddSignDataValue("ts", TimeSpan.ToString(), separator, strTicketParas);
            AddSignDataValue("tn", Token, separator, strTicketParas);
            AddSignDataValue("wb", WebBrowser, separator, strTicketParas);

            return strTicketParas;
        }

        /// <summary>
        ///   追加要加密的串
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <param name="strTicketParas"></param>
        protected internal static void AddSignDataValue(string name, string value, char separator, StringBuilder strTicketParas)
        {
            if (string.IsNullOrEmpty(value)) return;

            if (strTicketParas.Length > 0)
            {
                strTicketParas.Append(separator);
            }
            strTicketParas.Append(name).Append("=").Append(value.UrlEncode());
        }

        #endregion
    } 
    /// <summary>
    /// 应用客户端类型
    /// 1 - 3000  PC
    /// 3001-6000  Pad  
    /// 6001- 9000  Mobile
    /// </summary>
    public enum AppClientType
    {
        /// <summary>
        ///  未知
        /// </summary>
        Unkonw = 0,
        
        /// <summary>
        /// 苹果应用
        /// </summary>
        iOS = 100,

        /// <summary>
        /// 安卓应用
        /// </summary>
        Android = 200,

        /// <summary>
        ///  window应用
        /// </summary>
        Window=300,

        /// <summary>
        /// 网页应用
        /// </summary>
        Web=500,
    }

    ///// <summary>
    ///// 应用客户端类型
    ///// 1 - 3000  PC
    ///// 3001-6000  Pad  
    ///// 6001- 9000  Mobile
    ///// </summary>
    //public enum UserClientType
    //{
    //    /// <summary>
    //    ///  未知
    //    /// </summary>
    //    Unkonw=0,

    //    /// <summary>
    //    /// PC版windows系统
    //    /// </summary>
    //    Windows = 1,

    //    /// <summary>
    //    ///  PC版苹果系统
    //    /// </summary>
    //    Macintosh = 30,

    //    /// <summary>
    //    /// Linux 系统
    //    /// </summary>
    //    Linux = 60,

    //    //============================= PC 分界线

    //    /// <summary>
    //    ///  Pad泛类
    //    /// </summary>
    //    Pad=3001,

    //    /// <summary>
    //    ///  苹果pad操作系统
    //    /// </summary>
    //    iOS_Pad = 3030,

    //    /// <summary>
    //    /// 安卓pad端
    //    /// </summary>
    //    Android_Pad =3060,

    //    //============================= Pad 分界线

    //    /// <summary>
    //    ///  手机泛类
    //    /// </summary>
    //    Mobile= 6001,

    //    /// <summary>
    //    /// 苹果手机系统
    //    /// </summary>
    //    iOS = 6030,

    //    /// <summary>
    //    /// 安卓手机系统
    //    /// </summary>
    //    Android=6030
    //}
}