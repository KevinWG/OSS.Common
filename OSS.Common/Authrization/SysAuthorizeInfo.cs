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
        /// <summary>
        ///   从头字符串中初始化签名相关属性信息
        /// </summary>
        /// <param name="signData"></param>
        /// <param name="separator">A=a  B=b 之间分隔符</param>
        public void FromSignData(string signData, char separator = ';')
        {
            if (string.IsNullOrEmpty(signData)) return;

            var strs = signData.Split(separator);
            foreach (var str in strs)
            {
                if (string.IsNullOrEmpty(str)) continue;

                var keyValue = str.Split(new[] {'='}, 2);
                if (keyValue.Length <= 1) continue;

                var val = keyValue[1].UrlDecode();
                FormatProperty(keyValue[0], val);
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
            AddTicketProperty("sign", Sign, separator, encrpStr);
            return encrpStr.ToString();
        }


        #region  字符串处理

        /// <summary>
        ///   从头字符串中初始化签名相关属性信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        protected override void FormatProperty(string key, string val)
        {
            switch (key)
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
                    AppClient = (AppClientType)val.ToInt32();
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

        #endregion

        /// <summary>
        ///   获取要加密签名的串
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        protected override StringBuilder GetSignContent(char separator)
        {
            var strTicketParas = new StringBuilder();
            if (AppClient > 0)
                AddTicketProperty("app_client", AppClient.ToString(), separator, strTicketParas);

            AddTicketProperty("app_source", AppSource, separator, strTicketParas);
            AddTicketProperty("app_version", AppVersion, separator, strTicketParas);
            AddTicketProperty("device_id", DeviceId, separator, strTicketParas);
            AddTicketProperty("ip_address", IpAddress, separator, strTicketParas);

            AddTicketProperty("pro_code", ProCode, separator, strTicketParas);
            if (TenantId > 0)
            {
                AddTicketProperty("tenant_id", TenantId.ToString(), separator, strTicketParas);
            }
            AddTicketProperty("timespan", TimeSpan.ToString(), separator, strTicketParas);
            AddTicketProperty("token", Token, separator, strTicketParas);
            AddTicketProperty("web_browser", WebBrowser, separator, strTicketParas);

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
        /// </summary>
        public AppClientType AppClient { get; set; }

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
        /// IP地址 可选 手机App为空
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        ///  租户ID  可选 
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        ///  推广码  可选 
        /// </summary>
        public string ProCode { get; set; }

        /// <summary>
        ///  operate tag  可选 
        /// </summary>
        public string OTag { get; set; }

        #endregion

        #region  字符串处理

        /// <summary>
        ///   从头字符串中初始化签名相关属性信息
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="separator">A=a  B=b 之间分隔符</param>
        public void FromTicket(string ticket, char separator = ';')
        {
            if (string.IsNullOrEmpty(ticket)) return;

            var strs = ticket.Split(separator);
            foreach (var str in strs)
            {
                if (string.IsNullOrEmpty(str)) continue;

                var keyValue = str.Split(new[] {'='}, 2);
                if (keyValue.Length <= 1) continue;

                var val = keyValue[1].UrlDecode();
                FormatProperty(keyValue[0], val);
            }
        }

        /// <summary>
        ///   格式化属性值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        protected virtual void FormatProperty(string key, string val)
        {
            switch (key)
            {
                case "av":
                    AppVersion = val;
                    break;
                case "as":
                    AppSource = val;
                    break;
                case "ac":
                    AppClient = (AppClientType)val.ToInt32();
                    break;
                case "did":
                    DeviceId = val;
                    break;
                case "ip":
                    IpAddress = val;
                    break;
                case "ot":
                    OTag = val;
                    break;
                case "pc":
                    ProCode = val;
                    break;

                case "tid":
                    TenantId = val.ToInt64();
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

                OTag = this.OTag,
                ProCode = this.ProCode,
                Sign = this.Sign,
                TenantId = this.TenantId,
                TimeSpan = this.TimeSpan,

                Token = this.Token,
                WebBrowser = this.WebBrowser
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
            var signData = HMACSHA.EncryptBase64(strTicketParas.ToString(), secretKey);

            return Sign == signData;
        }


        /// <summary>
        /// 生成签名后的字符串
        /// </summary>
        /// <returns></returns>
        public string ToTicket(string secretKey, char separator = ';')
        {
            TimeSpan = DateTime.Now.ToUtcSeconds();
            var encrpStr = GetSignContent(separator);

            Sign = HMACSHA.EncryptBase64(encrpStr.ToString(), secretKey);
            AddTicketProperty("sign", Sign, separator, encrpStr);

            ExtendTicket(encrpStr, separator);
            return encrpStr.ToString();
        }

        /// <summary>
        ///  扩展ticket内容，此内容不参与签名
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="separator"></param>
        protected virtual void ExtendTicket(StringBuilder ticket, char separator)
        {

        }

        /// <summary>
        ///   获取要加密签名的串
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        protected virtual StringBuilder GetSignContent(char separator)
        {
            var strTicketParas = new StringBuilder();

            AddTicketProperty("ac", AppClient.ToString(), separator, strTicketParas);
            AddTicketProperty("as", AppSource, separator, strTicketParas);
            AddTicketProperty("av", AppVersion, separator, strTicketParas);
            AddTicketProperty("did", DeviceId, separator, strTicketParas);
            AddTicketProperty("ip", IpAddress, separator, strTicketParas);

            AddTicketProperty("ot", OTag, separator, strTicketParas);
            AddTicketProperty("pc", ProCode, separator, strTicketParas);

            if (TenantId > 0)
                AddTicketProperty("tid", TenantId.ToString(), separator, strTicketParas);
            
            AddTicketProperty("tn", Token, separator, strTicketParas);
            AddTicketProperty("ts", TimeSpan.ToString(), separator, strTicketParas);
            AddTicketProperty("wb", WebBrowser, separator, strTicketParas);

            return strTicketParas;
        }

        /// <summary>
        ///   追加要加密的串
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <param name="strTicketParas"></param>
        protected static void AddTicketProperty(string name, string value, char separator, StringBuilder strTicketParas)
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
        Window = 300,

        /// <summary>
        /// wap网页
        /// </summary>
        Wap = 500,

        /// <summary>
        /// web网页
        /// </summary>
        Web = 600,
    }
}