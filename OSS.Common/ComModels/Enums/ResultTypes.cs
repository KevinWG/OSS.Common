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
    ///   结果类型
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
        [OSDescript("权限不足")] NoRight = 1430,

        /// <summary>
        /// 账号/权限冻结
        /// </summary>
        [OSDescript("账号/权限冻结")]
        AuthFreezed = 1440,
        
        /// <summary>
        /// 租户下线关闭状态
        /// </summary>
        [OSDescript("租户关闭")] TenantOff = 1450,

        /// <summary>
        /// 租户欠费
        /// </summary>
        [OSDescript("租户欠费")] TenantArrearage = 1452,

        /// <summary>
        /// 内部错误（服务器错误）
        /// </summary>
        [OSDescript("内部错误")] InnerError = 1500,

    }
}
