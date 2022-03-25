#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用返回响应实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using OSS.Common.Extension;

namespace OSS.Common.Resp
{
    /// <summary>
    /// 响应实体
    /// </summary>
    public class Resp : IResp
    {
        /// <summary>
        ///  默认成功结果
        /// </summary>
        public static readonly IResp default_success = new Resp();

        /// <summary>
        /// 构造响应类
        /// </summary>
        public Resp()
        {
            sys_ret = 0;
            _ret    = 0; // 初始化为正常
        }

        /// <summary>
        ///  构造响应类
        /// </summary>
        /// <param name="ret">【业务】响应标识</param>
        /// <param name="message">响应信息描述</param>
        public Resp(int ret, string message)
        {
            this.ret = ret;
            this.msg = message;
        }


        /// <summary>
        ///  构造响应类
        /// </summary>
        /// <param name="ret">【业务】响应标识</param>
        /// <param name="message">响应信息描述</param>
        public Resp(RespTypes ret, string message = null)
            : this((int) ret, ret != RespTypes.Success && string.IsNullOrEmpty(message) ? ret.GetDesp() : message)
        {
        }


        /// <summary>
        ///  构造响应类
        /// </summary>
        /// <param name="sysRet">【系统/框架】 响应标识</param>
        /// <param name="message">响应信息描述</param>
        public Resp(SysRespTypes sysRet, string message = null)
        {
            this.sys_ret = (int) sysRet;
            this.msg     = sysRet != SysRespTypes.Ok && string.IsNullOrEmpty(message) ? sysRet.GetDesp() : message;
        }

        /// <summary>
        ///  构造响应类
        /// </summary>
        /// <param name="sysRet">【系统/框架】 响应标识</param>
        /// <param name="message">响应信息描述</param>
        /// <param name="ret">【业务】响应标识</param>
        public Resp(SysRespTypes sysRet, string message = null, int ret = 0) : this(sysRet, message)
        {
            this.ret = ret;
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
        public int ret
        {
            get => (sys_ret != 0 && _ret == 0) ? (int)RespTypes.OperateFailed : _ret;
            set => _ret = value;
        }
        private int _ret;


        /// <summary>
        ///  系统响应
        /// </summary>
        public int sys_ret { get; set; }

        /// <summary>
        /// 状态信息(错误描述等)
        /// </summary>
        public string msg { get; set; }

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
