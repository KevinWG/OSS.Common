#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用响应枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion


using System;
using OSS.Common.Extension;

namespace OSS.Common.BasicMos.Resp
{
    /// <summary>
    ///   系统响应类型
    /// </summary>
    public enum SysRespTypes
    {
        /// <summary>
        /// 运行正常
        /// </summary>
        [OSDescribe("运行正常")] Ok = 0,

        /// <summary>
        /// 无法连接
        /// </summary>
        [OSDescribe("无法连接")] NetworkError = 10000,

        /// <summary>
        /// 无响应
        /// </summary>
        [OSDescribe("无响应")] NoResponse = 11000,

        /// <summary>
        ///  连接超时
        /// </summary>
        [OSDescribe("连接超时")] TimeOut = 12000,

        /// <summary>
        ///  数据库错误
        /// </summary>
        [OSDescribe("数据源错误")] DataSourceError = 20000,

        /// <summary>
        /// 应用异常
        /// </summary>
        [OSDescribe("应用异常")] ApplicationError = 30000,


        /// <summary>
        /// 配置错误
        /// </summary>
        [OSDescribe("配置错误")] AppConfigError = 30000,
    }

    /// <summary>
    ///   业务响应类型
    /// </summary>
    public enum RespTypes
    {
        /// <summary>
        /// 成功
        /// </summary>
        [OSDescribe("成功")] Success = 0,

     
        /// <summary>
        /// 参数错误
        /// </summary>
        [OSDescribe("参数错误")] ParaError = 1301,

        /// <summary>
        ///  签名过期
        /// </summary>
        [OSDescribe("签名过期")] SignExpired = 1302,

        /// <summary>
        ///  签名错误
        /// </summary>
        [OSDescribe("签名错误")] SignError = 1303,

        /// <summary>
        /// 超出范围
        /// </summary>
        [OSDescribe("超出范围")] 
        ParaOutOfRange = 1310,

        /// <summary>
        /// 对象不存在
        /// </summary>
        [OSDescribe("对象为空")] ObjectNull = 1404,

     

        /// <summary>
        /// 对象已存在
        /// </summary>
        [OSDescribe("已经存在")] ObjectExist = 1410,

        /// <summary>
        /// 对象状态不正常
        /// </summary>
        [OSDescribe("状态异常")] ObjectStateError = 1420,



        /// <summary>
        /// 未登录
        /// </summary>
        [OSDescribe("未登录")] UnLogin = 1425,

        /// <summary>
        /// 未登录
        /// </summary>
        [OSDescribe("登录失败")] LoginFailed = 1426,
        
        /// <summary>
        /// 权限不足
        /// </summary>
        [OSDescribe("权限不足")] NoPermission = 1430,

        /// <summary>
        /// 账号/权限冻结
        /// </summary>
        [OSDescribe("账号/权限冻结")] AuthFreezed = 1440,

        /// <summary>
        /// 更新失败
        /// </summary>
        [OSDescribe("操作失败")] OperateFailed = 1450,
        
        /// <summary>
        /// 系统错误
        /// </summary>
        [OSDescribe("内部异常")] InnerError = 1500,
        

        /// <summary>
        ///  未知操作
        /// </summary>
        [OSDescribe("未知操作")] UnKnowOperate = 1600,

        /// <summary>
        ///  未知来源
        /// </summary>
        [OSDescribe("未知来源")] UnKnowSource = 1610,



        /// <summary>
        /// 对象不存在
        /// </summary>
        [Obsolete]
        [OSDescribe("列表为空")] ListNull = 1406,
    }
}
