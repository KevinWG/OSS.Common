
#region Copyright (C) 2019 Kevin (OSS开源系列) 公众号：osscoder

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
        
        public ResultException(int sysRet, int ret, string msg) : base(msg)
        {
            sys_ret = sysRet;
            this.ret = ret;
            this.msg = msg;
        }

        public ResultException(SysResultTypes sysRet, ResultTypes ret, string msg) : base(msg)
        {
            sys_ret = (int)sysRet;
            this.ret = (int)ret;
            this.msg = msg;
        }

        /// <summary>
        ///  转化为结果实体
        /// </summary>
        /// <returns></returns>
        public ResultMo ConvertToReult()
        {
            return new ResultMo(sys_ret,ret,msg);
        }
    }


    public static class ResultExceptionExtention
    {
        public static ResultException ConvertToException(this ResultMo res)
        {
            return new ResultException(res.sys_ret,res.ret,res.msg);
        }

    }
}
