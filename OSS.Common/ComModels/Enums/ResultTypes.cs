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
        UpdateFail = 1330,

        /// <summary>
        /// 对象不存在
        /// </summary>
        ObjectNull = 1404,

        /// <summary>
        /// 对象已存在
        /// </summary>
        ObjectExsit = 1410,

        /// <summary>
        /// 对象状态不正常
        /// </summary>
        ObjectStateError = 1420,

        /// <summary>
        ///  未知来源
        /// </summary>
        UnKnowSource = 1423,

        /// <summary>
        /// 未授权
        /// </summary>
        UnAuthorize = 1425,

        /// <summary>
        /// 权限不足
        /// </summary>
        NoRight = 1430,

        /// <summary>
        /// 授权冻结
        /// </summary>
        AuthFreezed = 1440,

        /// <summary>
        /// 内部错误（服务器错误）
        /// </summary>
        InnerError = 1500

    }
}
