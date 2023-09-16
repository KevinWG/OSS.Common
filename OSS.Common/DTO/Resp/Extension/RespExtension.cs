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
        /// 【业务响应】是否是 OperateObjectNull
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsObjectNull<TType>(this IResp<TType> res) =>  res.code == (int)RespCodes.OperateObjectNull;

        /// <summary>
        ///  【业务响应】是否成功或者数据不存在(但系统状态OK)
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsSuccessOrDataNull<TType>(this IResp<TType> res) =>
            res.code == 0 || (res.IsSysOk() && res.code == (int)RespCodes.OperateObjectNull);

        /// <summary>
        /// 【业务响应】是否是对应的类型
        /// </summary>
        /// <param name="res"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsRespCode(this IResp res, RespCodes type) => res.code == (int)type;


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
        public static bool IsSysRespCode(this IResp res, SysRespCodes type) => res.sys_code == (int)type;

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
            where TRes : IResp
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
        public static TRes WithResp<TRes>(this TRes res, SysRespCodes sysCode, RespCodes code, string msg)
            where TRes : IResp
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
        public static TRes WithResp<TRes>(this TRes res, SysRespCodes sysCode, string msg)
            where TRes : IResp
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
            where TRes : IResp
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
        public static TRes WithResp<TRes>(this TRes res, RespCodes code, string msg)
            where TRes : IResp
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
            where TRes : IResp
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
            where TRes : IResp
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
            where TRes : IResp
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
            where TRes : IResp
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
            where TRes : IResp
            where TPara : IResp
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
            where TRes : IResp<TData>
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
            where TRes : IResp
        {
            return Task.FromResult(res);
        }

        /// <summary>
        ///  转化简单Task结果实体
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="resTask"></param>
        /// <returns></returns>
        public static async Task<IResp> ToTaskResp<TRes>(this Task<TRes> resTask)
            where TRes : IResp
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
        public static IResp<TRes> WithResp<TRes, TPara>(this IResp<TRes> res, IResp<TPara> tPara,
                                                        Func<TPara, TRes> convertFunc, string errMsg=null, bool isNullCheck = true)
        {
            if ((isNullCheck && tPara.data != null)
                || !isNullCheck)
            {
                res.data = convertFunc.Invoke(tPara.data);
            }

            return res.WithResp(tPara, errMsg);
        }

        #endregion


        #endregion

    }

}