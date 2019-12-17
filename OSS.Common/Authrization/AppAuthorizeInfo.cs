﻿#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

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
    using OSS.Common.Resp;
    /// <summary>
    ///   应用的授权认证信息
    /// </summary>
    [Obsolete("转移至OSS.Infrastructuer类库下AppIdentity")]
    public class AppAuthorizeInfo
    {
        #region  参与签名属性

        /// <summary>
        ///   【请求方】应用来源
        /// </summary>
        public string AppSource { get; set; }

        /// <summary>
        ///  【请求方】应用版本
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
        public AppSourceType AppType { get; set; } = AppSourceType.SystemManager;

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
                AppClient  = this.AppClient,
                AppSource  = this.AppSource,
                AppVersion = this.AppVersion,
                DeviceId   = this.DeviceId,
                IpAddress  = this.IpAddress,

                Sign     = this.Sign,
                TenantId = this.TenantId,
                TimeSpan = this.TimeSpan,
                Token    = this.Token,
                TraceNum = this.TraceNum,

                AppType = this.AppType
            };


            return newOne;
        }

        #endregion

        #region  签名相关

        private const int defaultExpSecs = 60 * 60 * 24;

        /// <summary>
        ///   检验是否合法
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public bool CheckSign(string secretKey, char separator = ';')
        {
            return CheckSign(secretKey, defaultExpSecs,null, separator).IsSuccess();
        }


        /// <summary>
        ///   检验是否合法
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="signExpiredSeconds"></param>
        /// <param name="extSignData">参与签名的扩展数据（ 原签名数据 + "&amp;" + extSignData ）</param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public Resp CheckSign(string secretKey, int signExpiredSeconds, string extSignData = null, char separator = ';')
        {
            if (TimeSpan + signExpiredSeconds < DateTime.Now.ToUtcSeconds())
                return new Resp(RespTypes.SignExpired, "签名已过期失效！");

            var signContent = GetContent(AppSource, AppVersion, separator, true);
            if (!string.IsNullOrEmpty(extSignData))
                signContent.Append("&").Append(extSignData);

            var signData = HMACSHA.EncryptBase64(signContent.ToString(), secretKey);

            return Sign == signData ? new Resp() : new Resp(RespTypes.SignError, "签名错误！");
        }

        /// <summary>
        /// 生成签名后的字符串
        /// </summary>
        /// <param name="appSource">当前应用来源</param>
        /// <param name="appVersion"></param>
        /// <param name="secretKey"></param>
        /// <param name="extSignData">参与签名的扩展数据（ 原签名数据 + "&amp;" + extSignData ）</param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string ToTicket(string appSource, string appVersion, string secretKey, string extSignData = null, char separator = ';')
        {
            TimeSpan = DateTime.Now.ToUtcSeconds();
            
            var signContent = GetContent(appSource, appVersion, separator,true);
            if (!string.IsNullOrEmpty(extSignData))
                signContent.Append("&").Append(extSignData);

            Sign = HMACSHA.EncryptBase64(signContent.ToString(), secretKey);

            var ticket = GetContent(appSource, appVersion, separator,false);
            AddTicketProperty("sign", Sign, separator, ticket, false);

            return ticket.ToString();
        }



        /// <summary>
        ///   获取要加密签名的串
        /// </summary>
        /// <param name="appSource"></param>
        /// <param name="appVersion"></param>
        /// <param name="separator"></param>
        /// <param name="isForSign">是否签名校验时调用，签名校验时不进行url转义，否则需要转义</param>
        /// <returns></returns>
        private StringBuilder GetContent(string appSource, string appVersion, char separator, bool isForSign)
        {
            var strTicketParas = new StringBuilder();

            AddTicketProperty("as", appSource, separator, strTicketParas, isForSign);
            AddTicketProperty("av", appVersion, separator, strTicketParas, isForSign);
            AddTicketProperty("did", DeviceId, separator, strTicketParas, isForSign);
            AddTicketProperty("ip", IpAddress, separator, strTicketParas, isForSign);
            AddTicketProperty("tid", TenantId, separator, strTicketParas, isForSign);
            
            AddTicketProperty("tn", Token, separator, strTicketParas, isForSign);
            AddTicketProperty("tnum", TraceNum, separator, strTicketParas, isForSign);
            AddTicketProperty("ts", TimeSpan.ToString(), separator, strTicketParas, isForSign);

            return strTicketParas;
        }

     

        /// <summary>
        ///   追加要加密的串
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <param name="strTicketParas"></param>
        /// <param name="isForSign">是否参与加密字符串</param>
        private static void AddTicketProperty(string name, string value, char separator, StringBuilder strTicketParas,
            bool isForSign)
        {
            if (string.IsNullOrEmpty(value)) return;

            if (strTicketParas.Length > 0)
                strTicketParas.Append(separator);

            strTicketParas.Append(name).Append("=").Append(isForSign ? value: value.UrlEncode());
        }

        #endregion
    }

    /// <summary>
    /// 应用客户端类型
    /// </summary>
    [Obsolete("转移至OSS.Infrastructuer类库下")]
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
    [Obsolete("转移至OSS.Infrastructuer类库下")]
    public enum AppSourceType
    {
        /// <summary>
        ///  平台应用（对内全租户管理）
        /// </summary>
        SystemManager = 1,

        /// <summary>
        ///  平台应用（对内多租户）
        /// </summary>
        System = 30,
        
        /// <summary>
        ///  平台应用 （对外多租户）
        /// </summary>
        Proxy = 60,

        /// <summary>
        ///  内部应用 （对内单一租户）
        /// </summary>
        Inner = 90,

        /// <summary>
        ///   外部应用（对外单一租户）
        /// </summary>
        Outer = 120
    }
}