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

namespace OSS.Common.Resp
{

    /// <summary>
    ///   系统响应类型
    /// </summary>
    public enum SysRespCodes
    {
        /// <summary>
        /// 运行正常
        /// </summary>
        [OSDescribe("OK")] Ok = 0,

        /// <summary>
        /// 无法连接
        /// </summary>
        [OSDescribe("网络异常")] NetError = 100,

        /// <summary>
        ///  超时
        /// </summary>
        [OSDescribe("已超时")] TimeOut = 408,
        
        /// <summary>
        /// 系统权限不足
        /// </summary>
        [OSDescribe("拒绝请求")] NotAllowed = 405,

        /// <summary>
        /// 应用异常
        /// </summary>
        [OSDescribe("应用程序异常")] AppError = 500,    

        /// <summary>
        /// 未实现
        /// </summary>
        [OSDescribe("未实现")] NotImplement = 501,
    }

    /// <summary>
    ///   业务响应类型
    /// </summary>
    public enum RespCodes
    {
        /// <summary>
        /// 成功
        /// </summary>
        [OSDescribe("成功")] Success = 0,

        
        /// <summary>
        /// 参数错误
        /// </summary>
        [OSDescribe("参数错误")] ParaError = 1300,

        /// <summary>
        ///  参数过期
        /// </summary>
        [OSDescribe("参数过期")] ParaExpired = 1310,

        /// <summary>
        ///  签名错误
        /// </summary>
        [OSDescribe("签名错误")] ParaSignError = 1312,





        /// <summary>
        /// 未登录
        /// </summary>
        [OSDescribe("未登录")] UserUnLogin = 1401,


        /// <summary>
        /// 已拉黑
        /// </summary>
        [OSDescribe("已拉黑")] UserBlocked = 1403,


        /// <summary>
        /// 权限不足
        /// </summary>
        [OSDescribe("权限不足")] UserNoPermission = 1405,



        /// <summary>
        /// 账号未激活
        /// </summary>
        [OSDescribe("账号未激活")] UserUnActive = 1424,

        /// <summary>
        /// 第三方授权待绑定
        /// </summary>
        [OSDescribe("第三方授权待绑定")] UserFromSocial = 1426,

        /// <summary>
        /// 账号信息缺失
        /// </summary>
        [OSDescribe("账号信息缺失")] UserIncomplete = 1428,








        /// <summary>
        /// 操作失败
        /// </summary>
        [OSDescribe("操作失败")] OperateFailed = 1500,

        /// <summary>
        /// 对象不存在
        /// </summary>
        [OSDescribe("对象为空")] OperateObjectNull = 1504,

        /// <summary>
        /// 对象已存在
        /// </summary>
        [OSDescribe("已经存在")] OperateObjectExisted = 1506,



    }
}
