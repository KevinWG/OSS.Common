﻿
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

namespace OSS.Common.Resp
{
    /// <summary>
    ///  结果异常类
    /// </summary>
    public class RespException:System.Exception
    {
        /// <summary>
        /// 异常相应实体
        /// </summary>
        public Resp ErrorResp { get; }

        /// <summary>
        ///  异常
        /// </summary>
        /// <param name="ret">【业务】错误码</param>
        /// <param name="message">异常信息描述</param>
        public RespException(int ret, string message):base(message)
        {
            ErrorResp = new Resp(ret, message);
        }


        /// <summary>
        ///  异常
        /// </summary>
        /// <param name="ret">【业务】错误码</param>
        /// <param name="message">异常信息描述</param>
        public RespException(RespTypes ret, string message = null) : base(message)
        {
            ErrorResp = new Resp(ret, message);
        }


        /// <summary>
        ///  异常
        /// </summary>
        /// <param name="sysRet">【系统/框架】 错误码</param>
        /// <param name="message">异常信息描述</param>
        public RespException(SysRespTypes sysRet, string message = null) : base(message)
        {
            ErrorResp = new Resp(sysRet, message);
        }

        /// <summary>
        ///  异常
        /// </summary>
        /// <param name="sysRet">【系统/框架】 错误码</param>
        /// <param name="ret">【业务】错误码</param>
        /// <param name="message">异常信息描述</param>
        public RespException(int sysRet, int ret, string message) : base(message)
        {
            ErrorResp = new Resp(sysRet,ret, message);
        }

    }


}
