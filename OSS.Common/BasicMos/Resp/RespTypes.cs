#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用响应枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

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
        [OSDescribe("OK")]
        Ok = 0,
        
        /// <summary>
        /// 无法连接
        /// </summary>
        [OSDescribe("网络异常")] 
        NetError = 100,

        /// <summary>
        ///  超时
        /// </summary>
        [OSDescribe("超时")] 
        TimeOut = 200,
        
        /// <summary>
        /// 应用异常
        /// </summary>
        [OSDescribe("应用异常")] 
        AppError = 500,
    }

    /// <summary>
    ///   业务响应类型
    /// </summary>
    public enum RespTypes
    {
        /// <summary>
        /// 成功
        /// </summary>
        [OSDescribe("成功")] 
        Success = 0,


        /// <summary>
        /// 参数错误
        /// </summary>
        [OSDescribe("参数错误")] 
        ParaError = 1300,

        /// <summary>
        /// 超出范围
        /// </summary>
        [OSDescribe("超出参数范围")]
        ParaOutOfRange = 1302,

        /// <summary>
        ///  签名错误
        /// </summary>
        [OSDescribe("签名错误")]
        ParaSignError = 1310,

        /// <summary>
        ///  签名过期
        /// </summary>
        [OSDescribe("签名过期")]
        ParaSignExpired = 1312,

        

        /// <summary>
        /// 未登录
        /// </summary>
        [OSDescribe("未登录")]
        UserUnLogin = 1420,

        /// <summary>
        /// 已拉黑
        /// </summary>
        [OSDescribe("已拉黑")]
        UserBlocked = 1422,

        /// <summary>
        /// 账号未激活
        /// </summary>
        [OSDescribe("账号未激活")]
        UserUnActive = 1424,

        /// <summary>
        /// 第三方授权待绑定
        /// </summary>
        [OSDescribe("第三方授权待绑定")]
        UserFromSocialUnBind = 1426,

        /// <summary>
        /// 账号信息缺失
        /// </summary>
        [OSDescribe("账号信息缺失")]
        UserIncomplete = 1428,
        
        /// <summary>
        /// 权限不足
        /// </summary>
        [OSDescribe("权限不足")]
        UserNoPermission = 1430,






        /// <summary>
        /// 操作失败
        /// </summary>
        [OSDescribe("操作失败")]
        OperateFailed = 1500,

        /// <summary>
        /// 对象不存在
        /// </summary>
        [OSDescribe("对象为空")]
        OperateObjectNull = 1504,

        /// <summary>
        /// 对象已存在
        /// </summary>
        [OSDescribe("已经存在")]
        OperateObjectExist = 1506,
    }
}
