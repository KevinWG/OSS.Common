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
    ///   应用的授权认证信息
    /// </summary>
    public class AppAuthorizeInfo
    {
        #region  参与签名属性

        /// <summary>
        ///   应用来源
        /// </summary>
        public string AppSource { get; set; }

        /// <summary>
        ///   应用版本
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// IP地址 可选 手机App为空
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        ///  用户Token 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///  租户ID
        /// </summary>
        public string TenantId { get; set; }


        /// <summary>
        ///  请求跟踪编号
        /// </summary>
        public string TraceNum { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long TimeSpan { get; set; }

        ///// <summary>
        /////   租户Token[仅对内部应用有效]
        ///// </summary>
        //public string TenantToken { get; set; }

        /// <summary>
        /// 浏览器类型   可选
        /// </summary>
        public string WebBrowser { get; set; }

     
        /// <summary>
        ///  sign标识
        /// </summary>
        public string Sign { get; set; }
        

        /// <summary>
        /// 应用客户端类型[非外部传值，不参与签名]
        /// </summary>
        public AppClientType AppClient { get; set; }

        /// <summary>
        ///   应用类型 [非外部传值，不参与签名]
        /// </summary>
        public AppSourceType AppType { get; set; }



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
                case "as":
                    AppSource = val;
                    break;
                case "av":
                    AppVersion = val;
                    break;
                case "did":
                    DeviceId = val;
                    break;
                case "ip":
                    IpAddress = val;
                    break;

                case "tid":
                    TenantId = val;
                    break;
                case "tn":
                    Token = val;
                    break;
                case "tnum":
                    TraceNum = val;
                    break;
                case "ts":
                    TimeSpan = val.ToInt64();
                    break;
                //case "tt":
                //    TenantToken = val;
                //    break;

                case "wb":
                    WebBrowser = val;
                    break;
                    

                case "sign":
                    Sign = val;
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

                Sign = this.Sign,
                TenantId = this.TenantId,
                TimeSpan = this.TimeSpan,
                Token = this.Token,
                TraceNum = this.TraceNum,

                WebBrowser = this.WebBrowser,
                //TenantToken = this.TenantToken,
                AppType = this.AppType
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
            var strTicketParas = GetSignContent(AppSource, AppVersion, separator, false);

            var signData = HMACSHA.EncryptBase64(strTicketParas.ToString(), secretKey);

            return Sign == signData;
        }


        /// <summary>
        /// 生成签名后的字符串
        /// </summary>
        /// <returns></returns>
        public string ToTicket(string appSource, string appVersion, string secretKey, char separator = ';')
        {
            TimeSpan = DateTime.Now.ToUtcSeconds();
            var encrpStr = GetSignContent(appSource, appVersion, separator, false);

            Sign = HMACSHA.EncryptBase64(encrpStr.ToString(), secretKey);

            var content = GetContent(appSource, appVersion, separator);
            AddTicketProperty("sign", Sign, separator, content, true);

            return content.ToString();
        }



        /// <summary>
        ///   获取要加密签名的串
        /// </summary>
        /// <param name="appSource"></param>
        /// <param name="appVersion"></param>
        /// <param name="separator"></param>
        /// <param name="isUrlEncode">是否url转义，传递的值需要转义，签名时不需要</param>
        /// <returns></returns>
        private StringBuilder GetSignContent(string appSource, string appVersion, char separator, bool isUrlEncode)
        {
            var strTicketParas = new StringBuilder();

            AddTicketProperty("as", appSource, separator, strTicketParas, isUrlEncode);
            AddTicketProperty("av", appVersion, separator, strTicketParas, isUrlEncode);
            AddTicketProperty("did", DeviceId, separator, strTicketParas, isUrlEncode);
            AddTicketProperty("ip", IpAddress, separator, strTicketParas, isUrlEncode);
            AddTicketProperty("tid", TenantId, separator, strTicketParas, isUrlEncode);


            AddTicketProperty("tn", Token, separator, strTicketParas, isUrlEncode);
            AddTicketProperty("tnum", TraceNum, separator, strTicketParas, isUrlEncode);
            AddTicketProperty("ts", TimeSpan.ToString(), separator, strTicketParas, isUrlEncode);
            //AddTicketProperty("tt", TenantToken, separator, strTicketParas, isUrlEncode);
            AddTicketProperty("wb", WebBrowser, separator, strTicketParas, isUrlEncode);

            return strTicketParas;
        }

        private StringBuilder GetContent(string appSource, string appVersion, char separator)
        {
            return GetSignContent(appSource, appVersion, separator, true);
        }

        /// <summary>
        ///   追加要加密的串
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <param name="strTicketParas"></param>
        /// <param name="isUrlEncode">是否参与加密字符串</param>
        private static void AddTicketProperty(string name, string value, char separator, StringBuilder strTicketParas,
            bool isUrlEncode)
        {
            if (string.IsNullOrEmpty(value)) return;

            if (strTicketParas.Length > 0)
            {
                strTicketParas.Append(separator);
            }
            strTicketParas.Append(name).Append("=").Append(isUrlEncode ? value.UrlEncode() : value);
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
        PC = 10,

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


    /// <summary>
    ///  应用来源类型
    /// </summary>
    public enum AppSourceType
    {
        /// <summary>
        ///  系统内部管理应用
        /// </summary>
        SystemManager = 1,

        /// <summary>
        ///系统应用
        /// </summary>
        System = 30,


        /// <summary>
        ///  代理应用
        /// </summary>
        Proxy = 60,

        /// <summary>
        ///  内部应用 
        /// </summary>
        Inner = 90,

        /// <summary>
        ///   外部应用
        /// </summary>
        Outer = 120
    }
}