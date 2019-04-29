#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

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
using System.Linq;
using OSS.Common.ComModels.Enums;

namespace OSS.Common.ComModels
{
    /// <summary>
    /// 结果实体
    /// </summary>
    public class ResultMo
    {
        /// <summary>
        /// 空构造函数
        /// </summary>
        public ResultMo()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultMo(int ret, string message)
        {
            this.ret = ret;
            this.msg = message;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultMo(ResultTypes ret, string message)
            : this((int) ret, message)
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysRet">系统错误编码</param>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultMo(int sysRet, int ret = 0, string message = null)
        {
            this.sys_ret = sysRet;
            this.ret = ret;
            this.msg = message;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysRet">系统错误编码</param>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultMo(SysResultTypes sysRet, ResultTypes ret = 0, string message = null)
            : this((int) sysRet, (int) ret, message)
        {
        }

        private int _ret;
        /// <summary>
        /// 业务结果
        /// 一般情况下：
        ///  0  成功
        ///  13xx   参数相关错误 
        ///  14xx   用户授权相关错误
        ///  15xx   服务器内部相关错误信息
        ///  16xx   系统级定制错误信息，如升级维护等
        /// 也可依据第三方自行定义数值
        /// </summary>
        public int ret {
            get
            {
                if (sys_ret != 0 && _ret == 0)
                    _ret=(int)GetRetFromSysRet((SysResultTypes)sys_ret);

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


        private ResultTypes GetRetFromSysRet(SysResultTypes sysRet)
        {
            switch (sysRet)
            {
                case SysResultTypes.ConfigError:
                    return ResultTypes.ParaError;
                case SysResultTypes.WaitActivate:
                case SysResultTypes.WaitRun:
                case SysResultTypes.RunFailed:
                case SysResultTypes.RunPause:
                    return ResultTypes.ObjectStateError;
                  default: return ResultTypes.InnerError;
                  
            }
        }
    }



    /// <summary>
    /// 带Id的结果实体
    /// </summary>
    public class ResultIdMo : ResultMo
    {
        public ResultIdMo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id</param>
        public ResultIdMo(string id)
        {
            this.id = id;
        }

        /// <summary>
        /// 结果实体
        /// </summary>        
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultIdMo(int ret, string message) : base(ret, message)
        {
        }

        /// <summary>
        /// 结果实体
        /// </summary>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultIdMo(ResultTypes ret, string message)
            : base(ret, message)
        {
        }

        /// <summary>
        /// 结果实体
        /// </summary>
        /// <param name="sysRet">系统错误编码</param>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultIdMo(int sysRet, int ret, string message) : base(sysRet, ret, message)
        {
        }

        /// <summary>
        /// 结果实体
        /// </summary>
        /// <param name="sysRet">系统错误编码</param>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultIdMo(SysResultTypes sysRet, ResultTypes ret, string message)
            : base(sysRet, ret, message)
        {
        }

        /// <summary>
        /// Id
        /// </summary>
        public string id { get; set; }
    }


    /// <summary>
    /// 自定义泛型的结果实体
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class ResultMo<TType> : ResultMo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultMo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data">数据信息</param>
        public ResultMo(TType data)
        {
            this.data = data;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultMo(int ret, string message = "")
            : base(ret, message)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultMo(ResultTypes ret, string message = "")
            : base(ret, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysRet">系统错误编码</param>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultMo(int sysRet, int ret, string message) : base(sysRet, ret, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysRet">系统错误编码</param>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultMo(SysResultTypes sysRet, ResultTypes ret, string message)
            : base(sysRet, ret, message)
        {
        }

        /// <summary>
        ///  结果类型数据
        /// </summary>
        public TType data { get; set; }
    }


    /// <summary>
    /// 自定义泛型的结果实体
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class ResultListMo<TType> : ResultMo<IList<TType>>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultListMo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        public ResultListMo(IList<TType> data)
        {
            this.data = data;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultListMo(int ret, string message = "")
            : base(ret, message)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultListMo(ResultTypes ret, string message = "")
            : base(ret, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysRet">系统错误编码</param>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultListMo(int sysRet, int ret, string message) : base(sysRet, ret, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysRet">系统错误编码</param>
        /// <param name="ret">业务错误编码</param>
        /// <param name="message">错误信息</param>
        public ResultListMo(SysResultTypes sysRet, ResultTypes ret, string message)
            : base(sysRet, ret, message)
        {
        }

    }


    /// <summary>
    ///  结果实体映射类
    /// </summary>
    public static class ResultMoMap
    {
        /// <summary>
        ///   将结果实体转换成其他结果实体
        /// </summary>
        /// <typeparam name="TResult">输出对象</typeparam>
        /// <typeparam name="TPara"></typeparam>
        /// <returns>输出对象</returns>
        public static ResultMo<TResult> ConvertToResult<TPara, TResult>(this TPara res,
            Func<TPara, TResult> func)
            where TPara : ResultMo
        {
            var ot = new ResultMo<TResult>
            {
                ret = res.ret,
                msg = res.msg,
                sys_ret = res.sys_ret
            };

            if (func != null && res.IsSuccess())
                ot.data = func(res);

            return ot;
        }



        /// <summary>
        /// 转化到结果实体
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static ResultMo<TResult> ConvertToResult<TPara, TResult>(this ResultMo<TPara> res,
            Func<TPara, TResult> func)
        {
            var ot = new ResultMo<TResult>
            {
                ret = res.ret,
                msg = res.msg,
                sys_ret = res.sys_ret
            };

            if (func != null && res.IsSuccess())
                ot.data = func(res.data);

            return ot;
        }

        /// <summary>
        /// 转化到结果实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <returns></returns>
        public static ResultMo<TResult> ConvertToResult<TResult>(this ResultMo res)
        {
            return ConvertToResult<ResultMo, TResult>(res, null);
        }


        /// <summary>
        /// 转化到结果实体
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult ConvertToResultInherit<TPara, TResult>(this TPara res,
            Func<TPara, TResult> func)
            where TPara : ResultMo
            where TResult : ResultMo, new()
        {
            var oRes = default(TResult);
            if (func != null)
                oRes = func(res);

            if (oRes == null)
                oRes = new TResult();

            if (oRes.ret > 0)
                return oRes;

            oRes.ret = res.ret;
            oRes.msg = res.msg;
            oRes.sys_ret = res.sys_ret;

            return oRes;
        }

        /// <summary>
        /// 转化到结果实体
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult ConvertToResultInherit<TPara, TResult>(this ResultMo<TPara> res,
            Func<TPara, TResult> func)
            where TResult : ResultMo, new()
        {
            var oRes = default(TResult);
            if (func != null)
                oRes = func(res.data);

            if (oRes == null)
                oRes = new TResult();

            if (oRes.ret > 0)
                return oRes;

            oRes.ret = res.ret;
            oRes.msg = res.msg;
            oRes.sys_ret = res.sys_ret;

            return oRes;
        }

        /// <summary>
        /// 转化到结果实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <returns></returns>
        public static TResult ConvertToResultInherit<TResult>(this ResultMo res)
            where TResult : ResultMo, new()
        {
            return ConvertToResultInherit<ResultMo, TResult>(res, null);
        }


        /// <summary>
        /// 转化到结果列表
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static ResultListMo<TResult> ConvertToResultList<TPara, TResult>(this ResultListMo<TPara> res,
            Func<TPara, TResult> func)
        {
            var listRes = new ResultListMo<TResult>()
            {
                ret = res.ret,
                msg = res.msg,
                sys_ret = res.sys_ret
            };

            if (func != null && res.data != null)
                listRes.data = res.data.Select(func).ToList();

            return listRes;
        }

        /// <summary>
        /// 转化到结果列表
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <returns></returns>
        public static ResultListMo<TResult> ConvertToResultList<TPara, TResult>(this ResultListMo<TPara> res)
        {
            return new ResultListMo<TResult>(res.sys_ret, res.ret, res.msg);
        }

    }
}
