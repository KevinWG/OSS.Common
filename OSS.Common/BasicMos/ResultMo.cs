#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用返回结果实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Generic;

namespace OSS.Common.ComModels
{
    /// <summary>
    /// 结果实体
    /// </summary>
    [Obsolete("建议使用 Resp 响应实体")]
    public class ResultMo
    {
        /// <summary>
        /// 构造结果类
        /// </summary>
        public ResultMo()
        {
        }

        /// <summary>
        ///  构造结果类
        /// </summary>
        /// <param name="ret">【业务】结果标识</param>
        /// <param name="message">结果信息描述</param>
        [Obsolete("使用 WithResult 方法")]
        public ResultMo(int ret, string message)
        {
            this.ret = ret;
            this.msg = message;
        }


        /// <summary>
        ///  构造结果类
        /// </summary>
        /// <param name="ret">【业务】结果标识</param>
        /// <param name="message">结果信息描述</param>
        [Obsolete("使用 WithResult 方法")]
        public ResultMo(ResultTypes ret, string message)
            : this((int) ret, message)
        {
        }


        /// <summary>
        ///  构造结果类
        /// </summary>
        /// <param name="sysRet">【系统/框架】 结果标识</param>
        /// <param name="message">结果信息描述</param>
        [Obsolete("使用 WithResult 方法")]
        public ResultMo(SysResultTypes sysRet, string message = null)
            : this((int) sysRet, 0, message)
        {
        }

        /// <summary>
        ///  构造结果类
        /// </summary>
        /// <param name="sysRet">【系统/框架】 结果标识</param>
        /// <param name="ret">【业务】结果标识</param>
        /// <param name="message">结果信息描述</param>
        [Obsolete("使用 WithResult 方法")]
        public ResultMo(int sysRet, int ret, string message)
        {
            this.sys_ret = sysRet;
            this.ret = ret;
            this.msg = message;
        }

        /// <summary>
        ///  构造结果类
        /// </summary>
        /// <param name="sysRet">【系统/框架】 结果标识</param>
        /// <param name="ret">【业务】结果标识</param>
        /// <param name="message">结果信息描述</param>
        [Obsolete("使用 WithResult 方法")]
        public ResultMo(SysResultTypes sysRet, ResultTypes ret, string message)
            : this((int) sysRet, (int) ret, message)
        {
        }

        private int _ret;

        /// <summary>
        /// 【业务结果】
        ///  如果 sys_ret != (int)SysResultTypes.Ok , 且 ret 未设置或设置0 ，则最终 ret = (int) ResultTypes.InnerError
        /// 一般情况下：
        ///  0  成功
        ///  13xx   参数相关错误 
        ///  14xx   用户授权相关错误
        ///  15xx   服务器内部相关错误信息
        ///  16xx   系统级定制错误信息，如升级维护等
        /// 也可依据第三方自行定义数值
        ///
        /// </summary>
        public int ret
        {
            get
            {
                if (sys_ret != 0 && _ret == 0)
                    _ret = (int) ResultTypes.InnerError;

                return _ret;
            }
            set => _ret = value;
        }
        
        /// <summary>
        ///  系统结果
        /// </summary>
        public int sys_ret { get; set; }

        /// <summary>
        /// 状态信息(错误描述等)
        /// </summary>
        public string msg { get; set; }

    }

    /// <inheritdoc />
    [Obsolete("建议使用 LongIdResp 响应实体")]
    public class ResultLongIdMo : ResultIdMo<long>
    {
        /// <inheritdoc />
        public ResultLongIdMo() 
        {
        }

        /// <inheritdoc />
        public ResultLongIdMo(long id) : base(id)
        {
        }
    }

    /// <inheritdoc />
    [Obsolete("建议使用 IdResp 响应实体")]
    public class ResultIdMo: ResultIdMo<string>
    {
        /// <inheritdoc />
        public ResultIdMo()
        {
        }

        /// <inheritdoc />
        public ResultIdMo(string id) : base(id)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultIdMo(int ret, string message) : base(ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultIdMo(ResultTypes ret, string message)
            : base(ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultIdMo(int sysRet, int ret, string message) : base(sysRet, ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultIdMo(SysResultTypes sysRet, ResultTypes ret, string message)
            : base(sysRet, ret, message)
        {
        }


        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultIdMo(SysResultTypes sysRet, string message)
            : base(sysRet, message)
        {
        }

    }

    /// <summary>
    /// 带Id的结果实体
    /// </summary>
    [Obsolete("建议使用 IdResp<IdType> 响应实体")]
    public class ResultIdMo<IdType> : ResultMo
    {
        /// <inheritdoc />
        /// <summary>
        /// 构造结果类
        /// </summary>
        public ResultIdMo()
        {
        }

        /// <inheritdoc />
        public ResultIdMo(IdType id) => this.id = id;


        #region 过时方法

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultIdMo(int ret, string message) : base(ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultIdMo(ResultTypes ret, string message)
            : base(ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultIdMo(int sysRet, int ret, string message) : base(sysRet, ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultIdMo(SysResultTypes sysRet, ResultTypes ret, string message)
            : base(sysRet, ret, message)
        {
        }


        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultIdMo(SysResultTypes sysRet, string message)
            : base(sysRet, message)
        {
        }

        #endregion


        /// <summary>
        /// Id
        /// </summary>
        public IdType id { get; set; }
    }

    /// <inheritdoc />
    [Obsolete("建议使用 Resp<TType> 响应实体")]
    public class ResultMo<TType> : ResultMo
    {
        /// <inheritdoc />
        public ResultMo()
        {
        }

        /// <inheritdoc />
        public ResultMo(TType data)
        {
            this.data = data;
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultMo(int ret, string message = "")
            : base(ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultMo(ResultTypes ret, string message = "")
            : base(ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultMo(int sysRet, int ret, string message) : base(sysRet, ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultMo(SysResultTypes sysRet, ResultTypes ret, string message)
            : base(sysRet, ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultMo(SysResultTypes sysRet, string message)
            : base(sysRet, message)
        {
        }

        /// <summary>
        ///  结果类型数据
        /// </summary>
        public TType data { get; set; }
    }


    /// <inheritdoc />
    [Obsolete("建议使用 ListResp<TType> 响应实体")]
    public class ResultListMo<TType> : ResultMo<IList<TType>>
    {
        /// <inheritdoc />
        public ResultListMo()
        {

        }

        /// <inheritdoc />
        public ResultListMo(IList<TType> data)
        {
            this.data = data;
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultListMo(int ret, string message = "")
            : base(ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultListMo(ResultTypes ret, string message = "")
            : base(ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultListMo(int sysRet, int ret, string message) : base(sysRet, ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultListMo(SysResultTypes sysRet, ResultTypes ret, string message)
            : base(sysRet, ret, message)
        {
        }

        /// <inheritdoc />
        [Obsolete("使用 WithResult 方法")]
        public ResultListMo(SysResultTypes sysRet, string message)
            : base(sysRet, message)
        {
        }

    }
}
