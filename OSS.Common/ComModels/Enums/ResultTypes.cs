#region Copyright (C) 2016 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：通用结果枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion
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
        Success = 200,

        /// <summary>
        /// 参数错误
        /// </summary>
        ParaError = 301,

        /// <summary>
        /// 条件不满足
        /// </summary>
        ParaNotMeet = 310,

        /// <summary>
        /// 添加失败
        /// </summary>
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
        /// 当前用户未验证身份信息
        /// </summary>
        UnAuthorize = 425,

        /// <summary>
        /// 没有权限
        /// </summary>
        NoRight = 430,

        /// <summary>
        /// 内部错误（服务器错误）
        /// </summary>
        InnerError = 500

    }
}
