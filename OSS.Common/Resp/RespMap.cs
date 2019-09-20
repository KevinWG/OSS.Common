using System;
using System.Linq;

namespace OSS.Common.Resp
{

    /// <summary>
    ///  响应实体映射类
    /// </summary>
    public static class RespMap
    {

        #region withresult 基础扩展


        /// <summary>
        /// 直接设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysRet"></param>
        /// <param name="ret"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, int sysRet, int ret, string eMsg)
            where TRes : Resp
        {
            res.msg = eMsg;
            res.ret = ret;
            res.sys_ret = sysRet;
            return res;
        }

        /// <summary>
        /// 直接设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="ret"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, int ret, string eMsg)
            where TRes : Resp
        {
            return res.WithResp((int) SysRespTypes.Ok, ret, eMsg);
        }

        /// <summary>
        /// 直接设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysRet"></param>
        /// <param name="ret"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, SysRespTypes sysRet, RespTypes ret, string eMsg)
            where TRes : Resp
        {
            return res.WithResp((int) sysRet, (int) ret, eMsg);
        }

        /// <summary>
        ///  设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysRet"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, SysRespTypes sysRet, string eMsg)
            where TRes : Resp
        {
            return res.WithResp((int) sysRet, (int) RespTypes.Success, eMsg);
        }

        /// <summary>
        /// 设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="ret"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, RespTypes ret, string eMsg)
            where TRes : Resp
        {
            return res.WithResp((int) SysRespTypes.Ok, (int) ret, eMsg);
        }


        #endregion

        #region 实例属性转化



        /// <summary>
        /// 处理响应转化
        /// </summary>
        /// <param name="res"></param>
        /// <param name="tPara"></param>
        /// <typeparam name="TRes"></typeparam>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, Resp tPara)
            where TRes : Resp
        {
            return res.WithResp(tPara.sys_ret, tPara.ret, tPara.msg);
        }

        /// <summary>
        /// 处理响应转化
        /// </summary>
        /// <param name="res"></param>
        /// <param name="tPara"></param>
        /// <param name="func"></param>
        /// <param name="isNullCheck">是否检查参数 data 为空</param>
        /// <typeparam name="TRes"></typeparam>
        /// <typeparam name="TPara"></typeparam>
        /// <returns></returns>
        public static Resp<TRes> WithResp<TRes, TPara>(this Resp<TRes> res, Resp<TPara> tPara,
            Func<TPara, TRes> func, bool isNullCheck = true)

        {
            WithResp(res, tPara.sys_ret, tPara.ret, tPara.msg);

            if (isNullCheck && tPara.data == null)
                return res;

            res.data = func.Invoke(tPara.data);
            return res;
        }

        /// <summary>
        /// 处理响应转化
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <typeparam name="TPara"></typeparam>
        /// <param name="res"></param>
        /// <param name="tPara"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static ListResp<TRes> WithResp<TRes, TPara>(this ListResp<TRes> res, ListResp<TPara> tPara,
            Func<TPara, TRes> func)
        {
            WithResp(res, tPara.sys_ret, tPara.ret, tPara.msg);

            res.data = tPara.data?.Select(func).ToList();

            return res;
        }

        #endregion
    }


}
