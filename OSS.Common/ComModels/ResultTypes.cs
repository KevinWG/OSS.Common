#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用结果枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System;
using OSS.Common.Extention;

namespace OSS.Common.ComModels
{
    /// <summary>
    ///   系统结果类型
    /// </summary>
    public enum SysResultTypes
    {
        /// <summary>
        /// 运行正常
        /// </summary>
        [OSDescript("运行正常")] Ok = 0,

        /// <summary>
        /// 无法连接
        /// </summary>
        [OSDescript("无法连接")] NetworkError = 10000,

        /// <summary>
        /// 无响应
        /// </summary>
        [OSDescript("无响应")] NoResponse = 11000,

        /// <summary>
        ///  连接超时
        /// </summary>
        [OSDescript("连接超时")] TimeOut = 12000,

        /// <summary>
        ///  数据库错误
        /// </summary>
        [OSDescript("数据库错误")] DataSourceError = 20000,

        /// <summary>
        /// 应用异常
        /// </summary>
        [OSDescript("应用异常")] ApplicationError = 30000,


        /// <summary>
        /// 配置错误
        /// </summary>
        [OSDescript("配置错误")] AppConfigError = 30000,




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
        [Obsolete("建议使用OperateFailed")] [OSDescript("添加失败")] AddFail = 1320,

        /// <summary>
        /// 更新失败
        /// </summary>
        [Obsolete("建议使用OperateFailed")] [OSDescript("更新失败")] UpdateFail = 1330,


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
        [OSDescript("账号/权限冻结")] AuthFreezed = 1440,

        /// <summary>
        /// 更新失败
        /// </summary>
        [OSDescript("操作失败")] OperateFailed = 1450,

        /// <summary>
        /// 系统错误
        /// </summary>
        [OSDescript("系统错误")] InnerError = 1500
    }



}
