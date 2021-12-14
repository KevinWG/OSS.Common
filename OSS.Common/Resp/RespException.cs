
#region Copyright (C) 2019 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：xml序列化辅助类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com       
*    	创建日期：2019-4-30
*       
*****************************************************************************/

#endregion

using System;

namespace OSS.Common.Resp
{
    /// <summary>
    ///  结果异常类
    /// </summary>
    public class RespException:Exception,IResp
    {
        private int _ret;

        /// <summary>
        ///  业务结果标识
        /// </summary>
        public int ret
        {
            get { return sys_ret != 0 && ret == 0 ? sys_ret : _ret; }
            set => _ret = value;
        }

        /// <summary>
        /// 结果信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        ///  系统标识
        /// </summary>
        public int sys_ret { get; set; }

        /// <summary>
        ///  构造异常结果类
        /// </summary>
        /// <param name="sysRet">【系统/框架】 结果标识</param>
        /// <param name="ret">【业务】结果标识</param>
        /// <param name="msg">结果信息描述</param>
        public RespException(int sysRet, int ret, string msg) : base(msg)
        {
            sys_ret = sysRet;
            this.ret = ret;
            this.msg = msg;
        }
        /// <summary>
        ///  构造异常结果类
        /// </summary>
        /// <param name="ret">【业务】结果标识</param>
        /// <param name="msg">结果信息描述</param>
        public RespException( int ret, string msg) : base(msg)
        {
            this.ret = ret;
            this.msg = msg;
        }

        /// <summary>
        ///  构造异常结果类
        /// </summary>
        /// <param name="msg">结果信息描述</param>
        public RespException(string msg) : base(msg)
        {
            this.sys_ret = (int) SysRespTypes.AppError;
            this.msg = msg;
        }



        /// <summary>
        ///  构造异常结果类
        /// </summary>
        /// <param name="sysRet">【系统/框架】 结果标识</param>
        /// <param name="ret">【业务】结果标识</param>
        /// <param name="msg">结果信息描述</param>
        public RespException(SysRespTypes sysRet, RespTypes ret, string msg) : base(msg)
        {
            sys_ret = (int)sysRet;
            this.ret = (int)ret;
            this.msg = msg;
        }

        /// <summary>
        ///  构造异常结果类
        /// </summary>
        /// <param name="sysRet">【系统/框架】 结果标识</param>
        /// <param name="msg">结果信息描述</param>
        public RespException(SysRespTypes sysRet,  string msg) : base(msg)
        {
            sys_ret = (int)sysRet;
            this.ret = (int)ret;
            this.msg = msg;
        }
        /// <summary>
        ///  构造异常结果类
        /// </summary>
        /// <param name="ret">【业务】结果标识</param>
        /// <param name="msg">结果信息描述</param>
        public RespException(RespTypes ret, string msg) : base(msg)
        {
            sys_ret = 0;
            this.ret = (int)ret;
             this.msg = msg;
        }

        /// <summary>
        ///  构造异常结果类
        /// </summary>
        /// <param name="res"> 结果实体 </param>
        public RespException(Resp res) : base(res.msg)
        {
            sys_ret = res.sys_ret;
            this.ret = res.ret;
            this.msg = res.msg;
        }

    }


}
