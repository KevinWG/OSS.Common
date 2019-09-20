namespace OSS.Common.Resp
{
    /// <summary>
    ///   ResultMo 扩展
    /// </summary>
    public static class RespExtention
    {
        /// <summary>
        ///  【业务响应】是否是Success
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsSuccess(this Resp res) =>
            res.ret == 0;

        /// <summary>
        /// 【业务响应】是否是对应的类型
        /// </summary>
        /// <param name="res"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsResultType(this Resp res, RespTypes type) =>
            res.ret == (int)type;


        /// <summary>
        ///  【系统响应】
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsSysOk(this Resp res) =>
            res.sys_ret == 0;

        /// <summary>
        /// 【系统响应】是否是对应的类型
        /// </summary>
        /// <param name="res"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSysResultType(this Resp res, SysRespTypes type) =>
            res.sys_ret == (int)type;
    }

}
