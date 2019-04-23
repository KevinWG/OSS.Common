#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：通用结果枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using OSS.Common.Extention;

namespace OSS.Common.ComModels.Enums
{
    /// <summary>
    ///   系统结果类型
    /// </summary>
    public enum SysResultTypes
    {
        /// <summary>
        /// 成功
        /// </summary>
        [OSDescript("正常")] None = 0,
        
        /// <summary>
        /// 无法连接
        /// </summary>
        [OSDescript("无法连接")] ConnectError = 100,

        /// <summary>
        /// 超时
        /// </summary>
        [OSDescript("超时")] TimeOut = 200,
        
        /// <summary>
        /// 任务失败
        /// </summary>
        [OSDescript("任务失败")] TaskFailed = 300,

        /// <summary>
        /// 内部错误（服务器错误）
        /// </summary>
        [OSDescript("内部错误")] InnerError = 500,
    }

    /// <summary>
    ///   业务结果类型
    /// </summary>
    public enum ResultTypes
    {
        /// <summary>
        /// 成功
        /// </summary>
        [OSDescript("成功")] Success = 0,

        /// <summary>
        ///  签名错误
        /// </summary>
        [OSDescript("签名错误")] SignError = 1300,

        /// <summary>
        /// 参数错误
        /// </summary>
        [OSDescript("参数错误")] ParaError = 1301,

        /// <summary>
        /// 添加失败
        /// </summary>
        [OSDescript("添加失败")] AddFail = 1320,

        /// <summary>
        /// 更新失败
        /// </summary>
        [OSDescript("更新失败")] UpdateFail = 1330,

        /// <summary>
        /// 对象不存在
        /// </summary>
        [OSDescript("对象不存在")] ObjectNull = 1404,

        /// <summary>
        /// 对象已存在
        /// </summary>
        [OSDescript("对象已存在")] ObjectExsit = 1410,

        /// <summary>
        /// 对象状态不正常
        /// </summary>
        [OSDescript("对象状态不正常")] ObjectStateError = 1420,

        /// <summary>
        ///  未知操作
        /// </summary>
        [OSDescript("未知操作")] UnKnowOperate = 1422,

        /// <summary>
        ///  未知来源
        /// </summary>
        [OSDescript("未知来源")] UnKnowSource = 1423,

        /// <summary>
        /// 未登录
        /// </summary>
        [OSDescript("未登录")] UnAuthorize = 1425,

        /// <summary>
        /// 权限不足
        /// </summary>
        [OSDescript("权限不足")] NoPermission = 1430,

        /// <summary>
        /// 账号/权限冻结
        /// </summary>
        [OSDescript("账号/权限冻结")]
        AuthFreezed = 1440,

        /// <summary>
        /// 系统错误
        /// </summary>
        [OSDescript("系统错误")] InnerError = 1500
    }



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
        /// 【系统结果】是否是对应的类型
        /// </summary>
        /// <param name="res"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSysResultType(this ResultMo res, SysResultTypes type) =>
            res.sys_ret == (int)type;
    }

}
