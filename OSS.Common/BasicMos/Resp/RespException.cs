
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

namespace OSS.Common.BasicMos.Resp
{
    /// <summary>
    ///  结果异常类
    /// </summary>
    public class RespException:Exception
    {
        private int _ret;
        public int ret
        {
            get
            {
                if (sys_ret != 0 && _ret == 0)
                    _ret = (int) RespTypes.InnerError;

                return _ret;
            }
            set => _ret = value;
        }

        public string msg { get; set; }
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
        /// 
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="msg"></param>
        public RespException(RespTypes ret, string msg) : base(msg)
        {
            sys_ret = 0;
            this.ret = (int)ret;
             this.msg = msg;
        }


    }


}
