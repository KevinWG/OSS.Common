using System;
using System.Linq;

namespace OSS.Common.BasicMos.Resp
{
    /// <summary>
    ///  响应实体映射类
    /// </summary>
    public static class RespMap
    {
        #region 判断结果
        
        /// <summary>
        ///  【业务响应】是否是Success
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsSuccess(this Resp res) =>
            res.code== 0;

        /// <summary>
        /// 【业务响应】是否是对应的类型
        /// </summary>
        /// <param name="res"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsRespCode(this Resp res, RespCode type) =>
            res.code== (int) type;


        /// <summary>
        ///  【系统响应】
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsSysOk(this Resp res) =>
            res.sys_code== 0;

        /// <summary>
        /// 【系统响应】是否是对应的类型
        /// </summary>
        /// <param name="res"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSysRespCode(this Resp res, SysRespCode type) =>
            res.sys_code== (int) type;

        #endregion

        #region WithResp 基础扩展

        /// <summary>
        /// 直接设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysCode"></param>
        /// <param name="code"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, int sysCode, int code, string eMsg)
            where TRes : Resp
        {
            res.msg = eMsg;
            res.code = code;
            res.sys_code = sysCode;
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
            return res.WithResp((int) SysRespCode.Ok, ret, eMsg);
        }

        /// <summary>
        /// 直接设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysCode"></param>
        /// <param name="code"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, SysRespCode sysCode, RespCode code, string eMsg)
            where TRes : Resp
        {
            return res.WithResp((int) sysCode, (int) code, eMsg);
        }

        /// <summary>
        ///  设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysCode"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, SysRespCode sysCode, string eMsg)
            where TRes : BasicMos.Resp.Resp
        {
            return res.WithResp((int) sysCode, (int) RespCode.Success, eMsg);
        }

        /// <summary>
        /// 设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="ret"></param>
        /// <param name="eMsg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, RespCode ret, string eMsg)
            where TRes : Resp
        {
            return res.WithResp((int) SysRespCode.Ok, (int) ret, eMsg);
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
            return res.WithResp(tPara.sys_code, tPara.code, tPara.msg);
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
            WithResp(res, tPara.sys_code, tPara.code, tPara.msg);

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
            WithResp(res, tPara.sys_code, tPara.code, tPara.msg);

            res.data = tPara.data?.Select(func).ToList();

            return res;
        }


        /// <summary>
        /// 处理响应转化
        /// </summary>
        /// <param name="res"></param>
        /// <param name="rExc"></param>
        /// <typeparam name="TRes"></typeparam>
        /// <returns></returns>
        public static TRes WithException<TRes>(this TRes res, RespException rExc)
            where TRes : BasicMos.Resp.Resp
        {
            return res.WithResp(rExc.sys_code, rExc.code, rExc.msg);
        }
        #endregion

        /// <summary>
        ///  赋值data
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="res"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TRes WithData<TRes, TData>(this TRes res, TData data)
        where TRes:Resp<TData>
        {
            res.data = data;
            return res;
        }
    }


}
