#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用返回响应实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System;
using OSS.Common.Extension;

namespace OSS.Common.Resp
{
    /// <summary>
    /// 响应实体
    /// </summary>
    public class Resp : IResp
    {
        /// <summary>
        ///  默认成功实体
        /// </summary>
        public static readonly  IResp DefaultSuccess = new Resp();
        
        /// <summary>
        /// 响应实体
        /// </summary>
        public Resp()
        {
        }

        /// <summary>
        ///  响应实体
        /// </summary>
        /// <param name="code">【业务】响应标识</param>
        /// <param name="message">响应信息描述</param>
        public Resp(int code, string message)
        {
            this.code = code;
            this.msg = message;
        }


        /// <summary>
        ///  响应实体
        /// </summary>
        /// <param name="code">【业务】响应标识</param>
        /// <param name="message">响应信息描述</param>
        public Resp(RespTypes code, string message = null)
        {
            this.code = (int) code;
            this.msg = code != RespTypes.Success && string.IsNullOrEmpty(message) ? code.GetDesp() : message;
        }


        /// <summary>
        ///  响应实体
        /// </summary>
        /// <param name="sysCode">【系统/框架】 响应标识</param>
        /// <param name="message">响应信息描述</param>
        public Resp(SysRespTypes sysCode, string message = null)
        {
            this.sys_code = (int) sysCode;
            this.msg     = sysCode != SysRespTypes.Ok && string.IsNullOrEmpty(message) ? sysCode.GetDesp() : message;
        }


        
        /// <summary>
        ///  响应实体
        /// </summary>
        /// <param name="sysCode">【系统/框架】 响应标识</param>
        /// <param name="code">【业务】响应标识</param>
        /// <param name="message">响应信息描述</param>
        public Resp(int sysCode, int code, string message)
        {
            this.sys_code = sysCode;
            this.code    = code;
            this.msg =  message;
        }


        /// <summary>
        /// 【业务响应】
        /// 一般情况下：
        ///  0  成功
        ///  13xxx   参数相关错误 
        ///  14xxx   用户授权相关错误
        ///  15xxx   应用处理错误
        /// 也可依据第三方自行定义数值
        /// </summary>
        public int code
        {
            get => (sys_code != 0 && _code == 0) ? (int)RespTypes.OperateFailed : _code;
            set => _code = value;
        }
        private int _code;

        /// <summary>
        ///  系统响应
        /// </summary>
        public int sys_code { get; set; }

        /// <summary>
        /// 状态信息(错误描述等)
        /// </summary>
        public string msg { get; set; }



        /// <summary>
        /// 结果标识，请使用code代替
        /// </summary>
        [Obsolete]
        public int ret
        {
            get => code;
            set => code = value;
        }
    }

    /// <summary>
    /// 响应实体泛型
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class Resp<TType> : Resp, IResp<TType>
    {
        /// <inheritdoc />
        public Resp()
        {
        }

        /// <inheritdoc />
        public Resp(TType data)
        {
            this.data = data;
        }
        
        /// <summary>
        ///  响应类型数据
        /// </summary>
        public TType data { get; set; }
    }

    /// <summary>
    ///  长整形的结果实例
    /// </summary>
    public class LongResp : Resp<long>
    {
        /// <inheritdoc />
        public LongResp()
        {
        }
        /// <inheritdoc />
        public LongResp(long data)
        {
            this.data = data;
        }
    }
    /// <summary>
    ///  整形的结果实例
    /// </summary>
    public class IntResp : Resp<int>
    {
        /// <inheritdoc />
        public IntResp()
        {
        }
        /// <inheritdoc />
        public IntResp(int data)
        {
            this.data = data;
        }
    }
    /// <summary>
    ///  字符串的结果实例
    /// </summary>
    public class StrResp : Resp<string>
    {
        /// <inheritdoc />
        public StrResp()
        {
        }

        /// <inheritdoc />
        public StrResp(string data)
        {
            this.data = data;
        }
    }



}
