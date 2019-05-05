
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
using OSS.Common.ComModels;
using OSS.Common.ComModels.Enums;

namespace OSS.Common.Extention
{
    /// <summary>
    ///  结果异常类
    /// </summary>
    public class ResultException:Exception
    {
        public int sys_ret { get; set; }
        public int ret { get; set; }
        public string msg { get; set; }


        /// <summary>
        ///  构造异常结果类
        /// </summary>
        /// <param name="sysRet">【系统/框架】 结果标识</param>
        /// <param name="ret">【业务】结果标识</param>
        /// <param name="msg">结果信息描述</param>
        public ResultException(int sysRet, int ret, string msg) : base(msg)
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
        public ResultException(SysResultTypes sysRet, ResultTypes ret, string msg) : base(msg)
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
        public ResultException(SysResultTypes sysRet,  string msg) : base(msg)
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
        public ResultException(ResultTypes ret, string msg) : base(msg)
        {
            sys_ret = 0;
            this.ret = (int)ret;
            this.msg = msg;
        }


        /// <summary>
        ///  转化为结果实例
        /// </summary>
        /// <returns></returns>
        public ResultMo ConvertToReult()
        {
            return new ResultMo(sys_ret,ret,msg);
        }

        /// <summary>
        /// 转化为结果实例
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <returns></returns>
        public ResultMo<TRes> ConvertToReult<TRes>()
        {
            return new ResultMo<TRes>(sys_ret, ret, msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <returns></returns>
        public TRes ConvertToReultInherit<TRes>()
            where TRes:ResultMo,new()

        {
            var res = new TRes
            {
                ret = ret,
                sys_ret = sys_ret,
                msg = msg
            };
            return res;
        }
    }

    /// <summary>
    ///  异常结果扩展类
    /// </summary>
    public static class ResultExceptionExtention
    {
        /// <summary>
        ///  结果类直接转化为 异常结果实例
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static ResultException ConvertToException(this ResultMo res)
        {
            return new ResultException(res.sys_ret,res.ret,res.msg);
        }

    }
}
