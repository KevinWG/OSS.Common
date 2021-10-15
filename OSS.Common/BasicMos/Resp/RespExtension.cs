using System;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Common.BasicMos.Resp
{
    /// <summary>
    ///  响应实体映射类
    /// </summary>
    public static class RespExtension
    {
        #region 判断结果
        
        /// <summary>
        ///  【业务响应】是否是Success
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsSuccess(this IReadonlyResp res) =>
            res.ret == 0;

        /// <summary>
        ///  【业务响应】数据是否为空（即数据不存在
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsDataNull<TType>(this IReadonlyResp<TType> res) => res.IsSysOk() &&  res.ret == (int) RespTypes.OperateObjectNull;
        
        /// <summary>
        /// 【业务响应】是否是对应的类型
        /// </summary>
        /// <param name="res"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsRespType(this IReadonlyResp res, RespTypes type) => res.ret == (int) type;


        /// <summary>
        ///  【系统响应】
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsSysOk(this IReadonlyResp res) => res.sys_ret == 0;

        /// <summary>
        /// 【系统响应】是否是对应的类型
        /// </summary>
        /// <param name="res"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSysRespType(this IReadonlyResp res, SysRespTypes type) => res.sys_ret == (int) type;


        #endregion

        #region WithResp 基础扩展

        /// <summary>
        /// 直接设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysRet"></param>
        /// <param name="ret"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, int sysRet, int ret, string msg)
            where TRes : Resp
        {
            res.msg = msg;
            res.ret = ret;
            res.sys_ret = sysRet;
            return res;
        }
        
        /// <summary>
        /// 直接设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysRet"></param>
        /// <param name="ret"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, SysRespTypes sysRet, RespTypes ret, string msg)
            where TRes : Resp
        {
            return res.WithResp((int) sysRet, (int) ret, msg);
        }

        /// <summary>
        ///  设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysRet"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, SysRespTypes sysRet, string msg)
            where TRes : Resp
        {
            res.sys_ret = (int)sysRet;
            res.msg = msg;
            return res;
        }

        /// <summary>
        /// 直接设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="ret"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, int ret, string msg)
            where TRes : Resp
        {
            res.ret = ret;
            res.msg = msg;
            return res;
        }

        /// <summary>
        /// 设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="ret"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, RespTypes ret, string msg)
            where TRes : Resp
        {
            res.ret = (int)ret;
            res.msg = msg;
            return res;
        }

        /// <summary>
        /// 设置不成功时的响应消息内容
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="msg"> 新的消息内容 </param>
        /// <returns></returns>
        public static TRes WithMsg<TRes>(this TRes res, string msg)
            where TRes : Resp
        {
            res.msg = msg;
            return res;
        }

        /// <summary>
        /// 设置不成功时的响应消息内容
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="errMsg">如果 res.IsSuccess()=false，且errMsg不为空，取errMsg，否则不变 </param>
        /// <returns></returns>
        public static TRes WithErrMsg<TRes>(this TRes res, string errMsg)
            where TRes : Resp
        {
            if (!res.IsSuccess())
            {
                res.msg = errMsg;
            }
            return res;
        }

        /// <summary>
        /// 设置不成功时的响应消息内容
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="tRes"></param>
        /// <param name="errMsg">如果 res.IsSuccess()=false，且errMsg不为空，取errMsg，否则不变 </param>
        /// <returns></returns>
        public static async Task<TRes> WithErrMsg<TRes>(this Task<TRes> tRes, string errMsg)
            where TRes : Resp
        {
            var res = await tRes;
            if (!res.IsSuccess())
            {
                res.msg = errMsg;
            }
            return res;
        }

        #endregion

        #region 实例属性转化

        /// <summary>
        /// 处理响应转化
        /// </summary>
        /// <param name="res"></param>
        /// <param name="tPara"></param>
        /// <param name="errMsg">如果 tPara.IsSuccess()=false，且errMsg不为空，取errMsg，否则取 tPara.msg </param>
        /// <typeparam name="TRes"></typeparam>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, IReadonlyResp tPara, string errMsg = null)
            where TRes : Resp
        {
            res.WithResp(tPara.sys_ret, tPara.ret, tPara.msg);
            return string.IsNullOrEmpty(errMsg) ? res : res.WithErrMsg(errMsg);
        }

        /// <summary>
        /// 处理响应转化
        /// </summary>
        /// <param name="res"></param>
        /// <param name="rExc"></param>
        /// <param name="errMsg">不成功时的消息内容，如果为空,消息内容取 tPara.msg</param>
        /// <typeparam name="TRes"></typeparam>
        /// <returns></returns>
        [Obsolete]
        public static TRes WithException<TRes>(this TRes res, RespException rExc, string errMsg = null)
            where TRes : Resp
        {
            res.WithResp(rExc.sys_ret, rExc.ret, rExc.msg);
            return string.IsNullOrEmpty(errMsg) ? res : res.WithErrMsg(errMsg);
        }

        #region 附带 data 转化处理

        /// <summary>
        /// 处理响应转化
        /// </summary>
        /// <param name="res"></param>
        /// <param name="tPara"></param>
        /// <param name="convertFunc"></param>
        /// <param name="errMsg">如果 tPara.IsSuccess()=false，且errMsg不为空，取errMsg，否则取 tPara.msg </param>
        /// <param name="isNullCheck">调用方法（convertFunc）之前是否检查数据是否为空</param>
        /// <typeparam name="TRes"></typeparam>
        /// <typeparam name="TPara"></typeparam>
        /// <returns></returns>
        public static Resp<TRes> WithResp<TRes, TPara>(this Resp<TRes> res, IReadonlyResp<TPara> tPara,
             Func<TPara, TRes> convertFunc, string errMsg, bool isNullCheck = true)
        {
            if ((isNullCheck && tPara.data != null)
                || !isNullCheck)
            {
                res.data = convertFunc.Invoke(tPara.data);
            }
            return res.WithResp(tPara, errMsg);
        }

        /// <summary>
        /// 处理响应转化
        /// </summary>
        /// <param name="res"></param>
        /// <param name="tPara"></param>
        /// <param name="convertFunc"></param>
        /// <param name="isNullCheck">调用方法（convertFunc）之前是否检查数据是否为空</param>
        /// <typeparam name="TRes"></typeparam>
        /// <typeparam name="TPara"></typeparam>
        /// <returns></returns>
        public static Resp<TRes> WithResp<TRes, TPara>(this Resp<TRes> res, IReadonlyResp<TPara> tPara,
           Func<TPara, TRes> convertFunc, bool isNullCheck=true)
        {
            return res.WithResp(tPara, convertFunc, null, isNullCheck);
        }

        /// <summary>
        /// 处理响应转化(循环List执行 convertFunc )
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <typeparam name="TPara"></typeparam>
        /// <param name="res"></param>
        /// <param name="tPara"></param>
        /// <param name="convertFunc"></param>
        /// <param name="errMsg">不成功时的消息内容，如果为空,消息内容取 tPara.msg</param>
        /// <returns></returns>
        public static ListResp<TRes> WithResp<TRes, TPara>(this ListResp<TRes> res, ListResp<TPara> tPara,
            Func<TPara, TRes> convertFunc, string errMsg = null)
        {
            res.data = tPara.data?.Select(convertFunc).ToList();
            return res.WithResp(tPara, errMsg);
        }

        #endregion

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


        /// <summary>
        ///  结果实体转化成异步任务结果实体
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <returns></returns>
        public static Task<TRes> ToTaskResp<TRes>(this TRes res)
            where TRes : Resp
        {
            return Task.FromResult(res);
        }

        /// <summary>
        ///  转化简单Task结果实体
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="resTask"></param>
        /// <returns></returns>
        public static async Task<Resp> ToTaskResp<TRes>(this Task<TRes> resTask)
            where TRes : Resp
        {
            return await resTask;
        }




        //===== 待作废

        ///// <summary>
        /////  【业务响应】是否是Success
        ///// </summary>
        ///// <param name="res"></param>
        ///// <returns></returns>
        //[Obsolete]
        //public static bool IsSuccess(this Resp res) =>
        //    res.ret == 0;

        ///// <summary>
        /////  【业务响应】数据是否为空
        ///// </summary>
        ///// <param name="res"></param>
        ///// <returns></returns>
        //[Obsolete]
        //public static bool IsDataNull<TType>(this Resp<TType> res) =>
        //    res.data == null || res.ret == (int)RespTypes.OperateObjectNull;

        ///// <summary>
        ///// 【业务响应】是否是对应的类型
        ///// </summary>
        ///// <param name="res"></param>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //[Obsolete]
        //public static bool IsRespType(this Resp res, RespTypes type) =>
        //    res.ret == (int)type;


        ///// <summary>
        /////  【系统响应】
        ///// </summary>
        ///// <param name="res"></param>
        ///// <returns></returns>
        //[Obsolete]
        //public static bool IsSysOk(this Resp res) =>
        //    res.sys_ret == 0;

        ///// <summary>
        ///// 【系统响应】是否是对应的类型
        ///// </summary>
        ///// <param name="res"></param>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //[Obsolete]
        //public static bool IsSysRespType(this Resp res, SysRespTypes type) =>
        //    res.sys_ret == (int)type;

    }
}
