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
        public ResultMo(ResultTypes ret, string message)
            : this((int) ret, message)
        {
        }


        /// <summary>
        ///  构造结果类
        /// </summary>
        /// <param name="sysRet">【系统/框架】 结果标识</param>
        /// <param name="message">结果信息描述</param>
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



    /// <summary>
    /// 带Id的结果实体
    /// </summary>
    public class ResultIdMo : ResultMo
    {
        /// <inheritdoc />
        /// <summary>
        /// 构造结果类
        /// </summary>
        public ResultIdMo()
        {
        }

        /// <inheritdoc />
        public ResultIdMo(string id) => this.id = id;


        /// <inheritdoc />
        public ResultIdMo(int ret, string message) : base(ret, message)
        {
        }

        /// <inheritdoc />
        public ResultIdMo(ResultTypes ret, string message)
            : base(ret, message)
        {
        }

        /// <inheritdoc />
        public ResultIdMo(int sysRet, int ret, string message) : base(sysRet, ret, message)
        {
        }

        /// <inheritdoc />
        public ResultIdMo(SysResultTypes sysRet, ResultTypes ret, string message)
            : base(sysRet, ret, message)
        {
        }


        /// <inheritdoc />
        public ResultIdMo(SysResultTypes sysRet, string message)
            : base(sysRet, message)
        {
        }

        /// <summary>
        /// Id
        /// </summary>
        public string id { get; set; }
    }


    /// <inheritdoc />
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
        public ResultMo(int ret, string message = "")
            : base(ret, message)
        {
        }

        /// <inheritdoc />
        public ResultMo(ResultTypes ret, string message = "")
            : base(ret, message)
        {
        }

        /// <inheritdoc />
        public ResultMo(int sysRet, int ret, string message) : base(sysRet, ret, message)
        {
        }

        /// <inheritdoc />
        public ResultMo(SysResultTypes sysRet, ResultTypes ret, string message)
            : base(sysRet, ret, message)
        {
        }

        /// <inheritdoc />
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
        public ResultListMo(int ret, string message = "")
            : base(ret, message)
        {
        }

        /// <inheritdoc />
        public ResultListMo(ResultTypes ret, string message = "")
            : base(ret, message)
        {
        }

        /// <inheritdoc />
        public ResultListMo(int sysRet, int ret, string message) : base(sysRet, ret, message)
        {
        }

        /// <inheritdoc />
        public ResultListMo(SysResultTypes sysRet, ResultTypes ret, string message)
            : base(sysRet, ret, message)
        {
        }

        /// <inheritdoc />
        public ResultListMo(SysResultTypes sysRet, string message)
            : base(sysRet, message)
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

            if (oRes != null)
                return oRes;

            oRes = new TResult
            {
                ret = res.ret,
                msg = res.msg,
                sys_ret = res.sys_ret
            };
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

            if (oRes != null)
                return oRes;

            oRes = new TResult
            {
                ret = res.ret,
                msg = res.msg,
                sys_ret = res.sys_ret
            };
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


        /// <summary>
        /// 直接设置泛型结果信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysRet"></param>
        /// <param name="ret"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResult<TRes>(this TRes res, int sysRet, int ret, string eMsg)
            where TRes : ResultMo
        {
            res.msg = eMsg;
            res.ret = ret;
            res.sys_ret = sysRet;
            return res;
        }

        /// <summary>
        /// 直接设置泛型结果信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="ret"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResult<TRes>(this TRes res, int ret, string eMsg)
            where TRes : ResultMo
        {
            return res.WithResult((int) SysResultTypes.Ok, ret, eMsg);
        }

        /// <summary>
        /// 直接设置泛型结果信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysRet"></param>
        /// <param name="ret"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResult<TRes>(this TRes res, SysResultTypes sysRet, ResultTypes ret, string eMsg)
            where TRes : ResultMo
        {
            return res.WithResult((int) sysRet, (int) ret, eMsg);
        }

        /// <summary>
        ///  设置泛型结果信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysRet"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResult<TRes>(this TRes res, SysResultTypes sysRet, string eMsg)
            where TRes : ResultMo
        {
            return res.WithResult((int) sysRet, (int) ResultTypes.Success, eMsg);
        }

        /// <summary>
        /// 设置泛型结果信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="ret"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResult<TRes>(this TRes res, ResultTypes ret, string eMsg)
            where TRes : ResultMo
        {
            return res.WithResult((int) SysResultTypes.Ok, (int) ret, eMsg);
        }
    }
}
