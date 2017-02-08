#region Copyright (C) 2016 Kevin (OSS开源作坊) 公众号：osscoder

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
    ///   授权认证信息
    /// </summary>
    public class SysAuthorizeInfo
    {
        #region  参与签名属性

        /// <summary>
        ///   应用版本
        ///    不得包含  & 等特殊符号
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        ///   应用来源
        /// </summary>
        public string AppSource { get; set; }

        /// <summary>
        /// 应用客户端类型
        /// iOS, Android,PC等用户自定义
        /// </summary>
        public string AppClient { get; set; }

        /// <summary>
        /// 设备ID
        ///  不得包含  & 等特殊符号
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
        /// IP地址   可选
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 浏览器类型   可选
        /// </summary>
        public string WebBrowser { get; set; }


        /// <summary>
        /// 原始appsource   可选
        /// 主要是当应用层向基础层传递时使用
        ///  如支付系统等，api层面对多个应用，每个应用对应不同支付key，调用支付接口时必传
        /// </summary>
        public string OriginAppSource { get; set; }

        #endregion

     
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysAuthorizeInfo()
        {
            
        }



        #region  字符串处理

        /// <summary>
        ///   从头字符串中初始化签名相关属性信息
        /// </summary>
        /// <param name="signData"></param>
        /// <param name="separator">A=a  B=b 之间分隔符</param>
        public void FromSignData(string signData, char separator=';')
        {
            if (!string.IsNullOrEmpty(signData))
            {
                string[] strs = signData.Split(separator);
                foreach (var str in strs)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        string[] keyValue = str.Split(new[] { '=' }, 2);
                        if (keyValue.Length > 1)
                        {
                            string val = keyValue[1].UrlDecode();
                            switch (keyValue[0].ToLower())
                            {
                                case "appversion":
                                    AppVersion = val;
                                    break;
                                case "token":
                                    Token = val;
                                    break;
                                case "appsource":
                                    AppSource = val;
                                    break;
                                case "appclient":
                                    AppClient = val;
                                    break;
                                case "sign":
                                    Sign = val;
                                    break;
                                case "deviceid":
                                    DeviceId = val;
                                    break;
                                case "timespan":
                                    TimeSpan = val.ToInt64();
                                    break;
                                case "ipaddress":
                                    IpAddress = val;
                                    break;
                                case "webbrowser":
                                    WebBrowser = val;
                                    break;
                                case "originappsource":
                                    OriginAppSource = val;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 生成签名后的字符串
        /// </summary>
        /// <returns></returns>
        public string ToSignData(string secretKey, char separator=';')
        {
            TimeSpan = DateTime.Now.ToUtcSeconds();

            var encrpStr = GetSignContent(separator);
            Sign = HmacSha1.EncryptBase64(encrpStr.ToString(), secretKey);

            AddSignDataValue("sign", Sign, separator, encrpStr);

            return encrpStr.ToString();
        }

        /// <summary>
        /// 复制新的授权信息实体
        /// </summary>
        /// <returns></returns>
        public SysAuthorizeInfo Copy()
        {
            SysAuthorizeInfo newOne = new SysAuthorizeInfo();

            newOne.AppClient = this.AppClient;
            newOne.AppSource = this.AppSource;
            newOne.AppVersion = this.AppVersion;
            newOne.DeviceId = this.DeviceId;
            newOne.IpAddress = this.IpAddress;

            newOne.OriginAppSource = this.OriginAppSource;
            newOne.Sign = this.Sign;
            newOne.TimeSpan = this.TimeSpan;
            newOne.Token = this.Token;
            newOne.WebBrowser = this.WebBrowser;
            return newOne;
        }

        #endregion

        #region  签名相关

        /// <summary>
        ///   检验是否合法
        /// </summary>
        /// <returns></returns>
        public bool CheckSign(string secretKey, char separator=';')
        {
            var strTicketParas = GetSignContent(separator);
            
            string signData = HmacSha1.EncryptBase64(strTicketParas.ToString(), secretKey);
                
            return Sign == signData;
        }

        /// <summary>
        ///   获取要加密签名的串
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        private StringBuilder GetSignContent(char separator)
        {
            StringBuilder strTicketParas = new StringBuilder();

            AddSignDataValue("appclient",  AppClient, separator, strTicketParas);
            AddSignDataValue("appsource", AppSource, separator, strTicketParas);
            AddSignDataValue("appversion", AppVersion, separator, strTicketParas);
            AddSignDataValue("deviceid", DeviceId, separator, strTicketParas);
            
            AddSignDataValue("timespan", TimeSpan.ToString(), separator, strTicketParas);
            AddSignDataValue("token", Token, separator, strTicketParas);       
            AddSignDataValue("originappsource", OriginAppSource, separator, strTicketParas);

            return strTicketParas;
        }

        /// <summary>
        ///   追加要加密的串
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <param name="strTicketParas"></param>
        private void AddSignDataValue(string name, string value, char separator, StringBuilder strTicketParas)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (strTicketParas.Length > 0)
                {
                    strTicketParas.Append(separator);
                }
                strTicketParas.Append(name).Append("=").Append(value.UrlEncode());
            }
        }

        #endregion

    }

}