namespace OSS.Common.ComModels
{

    /// <summary>
    ///   ResultMo 扩展
    /// </summary>
    public static class ResultExtention
    {
        /// <summary>
        ///  【业务结果】是否是Success
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsSuccess(this ResultMo res) =>
            res.ret == 0;

        /// <summary>
        /// 【业务结果】是否是对应的类型
        /// </summary>
        /// <param name="res"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsResultType(this ResultMo res, ResultTypes type) =>
            res.ret == (int)type;


        /// <summary>
        ///  【系统结果】xi
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsSysOk(this ResultMo res) =>
            res.sys_ret == 0;

        /// <summary>
        /// 【系统结果】是否是对应的类型
        /// </summary>
        /// <param name="res"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSysResultType(this ResultMo res, SysResultTypes type) =>
            res.sys_ret == (int)type;
    }

}
