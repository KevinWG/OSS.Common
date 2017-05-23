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
        [OSDescript("成功")]
        Success = 200,

        /// <summary>
        /// 参数错误
        /// </summary>
        [OSDescript("参数错误")]
        ParaError = 301,

        /// <summary>
        /// 条件不满足
        /// </summary>
        [OSDescript("条件不满足")]
        ParaNotMeet = 310,

        /// <summary>
        /// 添加失败
        /// </summary>
        [OSDescript("添加失败")]
        AddFail = 320,

        /// <summary>
        /// 更新失败
        /// </summary>
        UpdateFail = 330,
   
        /// <summary>
        /// 对象不存在
        /// </summary>
        ObjectNull = 404,

        /// <summary>
        /// 对象已存在
        /// </summary>
        ObjectExsit = 410,

        /// <summary>
        /// 对象状态不正常
        /// </summary>
        ObjectStateError = 420,

        /// <summary>
        ///  未知来源
        /// </summary>
        UnKnowSource=423,

        /// <summary>
        /// 未授权
        /// </summary>
        UnAuthorize = 425,

        /// <summary>
        /// 权限不足
        /// </summary>
        NoRight = 430,

        /// <summary>
        /// 授权冻结
        /// </summary>
        AuthFreezen = 440,

        /// <summary>
        /// 内部错误（服务器错误）
        /// </summary>
        InnerError = 500

    }
}
