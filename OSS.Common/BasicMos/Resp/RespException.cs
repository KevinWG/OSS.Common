
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
        private int _code = 0;
        /// <summary>
        /// 
        /// </summary>
        public int sys_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { 
            get
            {
                if (sys_code != 0 && _code == 0)
                    _code = (int) RespCode.InnerError;

                return _code;
            }
            set => _code = value;
        }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }


        /// <summary>
        ///  构造异常结果类
        /// </summary>
        /// <param name="sysCode">【系统/框架】 结果标识</param>
        /// <param name="code">【业务】结果标识</param>
        /// <param name="msg">结果信息描述</param>
        public RespException(int sysCode, int code, string msg) : base(msg)
        {
            sys_code = sysCode;
            this.code = code;
            this.msg = msg;
        }

        /// <summary>
        ///  构造异常结果类
        /// </summary>
        /// <param name="sysCode">【系统/框架】 结果标识</param>
        /// <param name="code">【业务】结果标识</param>
        /// <param name="msg">结果信息描述</param>
        public RespException(SysRespCode sysCode, RespCode code, string msg) : base(msg)
        {
            sys_code = (int)sysCode;
            this.code = (int)code;
            this.msg = msg;
        }

        /// <summary>
        ///  构造异常结果类
        /// </summary>
        /// <param name="sysCode">【系统/框架】 结果标识</param>
        /// <param name="msg">结果信息描述</param>
        public RespException(SysRespCode sysCode,  string msg) : base(msg)
        {
            sys_code = (int)sysCode;
            this.msg = msg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public RespException(RespCode code, string msg) : base(msg)
        {
            sys_code = 0;
            this.code = (int) code;
            this.msg = msg;
        }
    }
}
