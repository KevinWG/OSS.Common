using System;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Common.Resp
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
        public static bool IsSuccess(this IResp res) =>
            res.code == 0;

        /// <summary>
        ///  【业务响应】是否成功或者数据不存在(但系统状态OK)
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsSuccessOrDataNull<TType>(this IResp<TType> res) =>
            res.code == 0 || (res.IsSysOk() && res.code == (int)RespTypes.OperateObjectNull);

        /// <summary>
        /// 【业务响应】是否是对应的类型
        /// </summary>
        /// <param name="res"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsRespType(this IResp res, RespTypes type) => res.code == (int)type;


        /// <summary>
        ///  【系统响应】
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsSysOk(this IResp res) => res.sys_code == 0;

        /// <summary>
        /// 【系统响应】是否是对应的类型
        /// </summary>
        /// <param name="res"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSysRespType(this IResp res, SysRespTypes type) => res.sys_code == (int)type;

        #endregion

        #region WithResp 基础赋值扩展

        /// <summary>
        /// 直接设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysRet"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, int sysRet, int code, string msg)
            where TRes : Resp
        {
            res.msg     = msg;
            res.code     = code;
            res.sys_code = sysRet;
            return res;
        }

        /// <summary>
        /// 直接设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysCode"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, SysRespTypes sysCode, RespTypes code, string msg)
            where TRes : Resp
        {
            return res.WithResp((int)sysCode, (int)code, msg);
        }

        /// <summary>
        ///  设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="sysCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, SysRespTypes sysCode, string msg)
            where TRes : Resp
        {
            res.sys_code = (int)sysCode;
            res.msg     = msg;
            return res;
        }

        /// <summary>
        /// 直接设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, int code, string msg)
            where TRes : Resp
        {
            res.code = code;
            res.msg = msg;
            return res;
        }

        /// <summary>
        /// 设置泛型响应信息，并返回
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, RespTypes code, string msg)
            where TRes : Resp
        {
            res.code = (int)code;
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

        #region 实体相互转化扩展

        /// <summary>
        /// 处理响应转化
        /// </summary>
        /// <param name="res"></param>
        /// <param name="tPara"></param>
        /// <param name="errMsg">如果 tPara.IsSuccess()=false，且errMsg不为空，取errMsg，否则取 tPara.msg </param>
        /// <typeparam name="TRes"></typeparam>
        /// <returns></returns>
        public static TRes WithResp<TRes>(this TRes res, IResp tPara, string errMsg = null)
            where TRes : Resp
        {
            res.WithResp(tPara.sys_code, tPara.code, tPara.msg);
            return string.IsNullOrEmpty(errMsg) ? res : res.WithErrMsg(errMsg);
        }

        /// <summary>
        /// 处理响应转化
        /// </summary>
        /// <param name="res"></param>
        /// <param name="tPara"></param>
        /// <param name="successFormatAction">tPara.IsSuccess()=true 时 执行的格式化方法</param>
        /// <param name="errMsg">如果 tPara.IsSuccess()=false，且errMsg不为空，取errMsg，否则取 tPara.msg </param>
        /// <typeparam name="TRes"></typeparam>
        /// <typeparam name="TPara"></typeparam>
        /// <returns></returns>
        public static TRes WithResp<TPara, TRes>(this TRes res, TPara tPara, Action<TPara, TRes> successFormatAction,
            string errMsg = null)
            where TRes : Resp
            where TPara : Resp
        {
            res.WithResp(tPara.sys_code, tPara.code, tPara.msg);
            if (tPara.IsSuccess())
            {
                successFormatAction(tPara, res);
            }

            return string.IsNullOrEmpty(errMsg) ? res : res.WithErrMsg(errMsg);
        }

        /// <summary>
        ///  赋值data
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="res"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TRes WithData<TRes, TData>(this TRes res, TData data)
            where TRes : Resp<TData>
        {
            res.data = data;
            return res;
        }


        #region Task 相关扩展

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

        #endregion
        


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
        public static Resp<TRes> WithResp<TRes, TPara>(this Resp<TRes> res, IResp<TPara> tPara,
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
        public static Resp<TRes> WithResp<TRes, TPara>(this Resp<TRes> res, IResp<TPara> tPara,
            Func<TPara, TRes> convertFunc, bool isNullCheck = true)
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

    }

}