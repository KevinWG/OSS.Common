using System;
using System.Linq;

namespace OSS.Common.ComModels
{

    /// <summary>
    ///  结果实体映射类
    /// </summary>
    public static class ResultMoMap
    {
        #region Old 转化处理

        /// <summary>
        /// 转化到结果实体
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        [Obsolete("请使用 new TResult().WithResult() ")]
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
        [Obsolete("请使用 new TResult().WithResult() ")]
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
        [Obsolete("请使用 new TResult().WithResult() ")]
        public static TResult ConvertToResultInherit<TResult>(this ResultMo res)
            where TResult : ResultMo, new()
        {
            return ConvertToResultInherit<ResultMo, TResult>(res, null);
        }


        /// <summary>
        ///   将结果实体转换成其他结果实体
        /// </summary>
        /// <typeparam name="TResult">输出对象</typeparam>
        /// <typeparam name="TPara"></typeparam>
        /// <returns>输出对象</returns>
        [Obsolete("请使用 new TResult().WithResult() ")]
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
        [Obsolete("请使用 new ResultMo<TResult>().WithResult() ")]
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
        [Obsolete("请使用 new ResultMo<TResult>().WithResult() ")]
        public static ResultMo<TResult> ConvertToResult<TResult>(this ResultMo res)
        {
            return ConvertToResult<ResultMo, TResult>(res, null);
        }


        /// <summary>
        /// 转化到结果列表
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        [Obsolete("请使用 new ResultListMo<TResult>().WithResult() ")]
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
        [Obsolete("请使用 new ResultListMo<TResult>().WithResult() ")]
        public static ResultListMo<TResult> ConvertToResultList<TPara, TResult>(this ResultListMo<TPara> res)
        {
            return new ResultListMo<TResult>(res.sys_ret, res.ret, res.msg);
        }



        #endregion

        #region withresult 基础扩展


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


        #endregion

        #region 实例属性转化



        /// <summary>
        /// 处理结果转化
        /// </summary>
        /// <param name="res"></param>
        /// <param name="tPara"></param>
        /// <typeparam name="TRes"></typeparam>
        /// <returns></returns>
        public static TRes WithResult<TRes>(this TRes res, ResultMo tPara)
            where TRes : ResultMo
        {
            return res.WithResult(tPara.sys_ret, tPara.ret, tPara.msg);
        }

        /// <summary>
        /// 处理结果转化
        /// </summary>
        /// <param name="res"></param>
        /// <param name="tPara"></param>
        /// <param name="func"></param>
        /// <param name="isNullCheck">是否检查参数 data 为空</param>
        /// <typeparam name="TRes"></typeparam>
        /// <typeparam name="TPara"></typeparam>
        /// <returns></returns>
        public static ResultMo<TRes> WithResult<TRes, TPara>(this ResultMo<TRes> res, ResultMo<TPara> tPara,
            Func<TPara, TRes> func, bool isNullCheck = true)

        {
            WithResult(res, tPara.sys_ret, tPara.ret, tPara.msg);

            if (isNullCheck && tPara.data == null)
                return res;

            res.data = func.Invoke(tPara.data);
            return res;
        }

        /// <summary>
        /// 处理结果转化
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <typeparam name="TPara"></typeparam>
        /// <param name="res"></param>
        /// <param name="tPara"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static ResultListMo<TRes> WithResult<TRes, TPara>(this ResultListMo<TRes> res, ResultListMo<TPara> tPara,
            Func<TPara, TRes> func)
        {
            WithResult(res, tPara.sys_ret, tPara.ret, tPara.msg);

            res.data = tPara.data?.Select(func).ToList();

            return res;
        }

        #endregion
    }


}
